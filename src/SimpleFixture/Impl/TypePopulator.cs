using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFixture.Impl
{
    public interface ITypePopulator
    {
        void Populate(object instance, DataRequest request, ComplexModel model);
    }

    public class TypePopulator : ITypePopulator
    {
        private readonly ITypePropertySelector _propertySelector;
        private readonly IConstraintHelper _helper;

        public TypePopulator(IConstraintHelper helper, ITypePropertySelector propertySelector)
        {
            _helper = helper;
            _propertySelector = propertySelector;
        }

        public virtual void Populate(object instance, DataRequest request, ComplexModel model)
        {
            if (instance == null)
            {
                return;
            }

            foreach (PropertyInfo propertyInfo in _propertySelector.SelectProperties(instance, request, model))
            {
                object propertyValue = _helper.GetValue<object>(request.Constraints, null, propertyInfo.Name);

                var newRequest = CreateDataRequestForProperty(propertyInfo, request);

                if (propertyValue != null)
                {
                    propertyValue = newRequest.Fixture.Behavior.Apply(newRequest, propertyValue);
                }
                else
                {
                    if (!model.GetPropertyValue(newRequest, propertyInfo, out propertyValue))
                    {
                        if (model.SkipProperty(newRequest, propertyInfo))
                        {
                            continue;
                        }

                        propertyValue = newRequest.Fixture.Generate(newRequest);
                    }
                    else
                    {
                        propertyValue = newRequest.Fixture.Behavior.Apply(newRequest, propertyValue);
                    }
                }

                if (propertyValue == null)
                {
                    throw new Exception("Could not create type " + propertyInfo.PropertyType.FullName);
                }

                propertyInfo.SetValue(instance, propertyValue);
            }
        }

        protected virtual DataRequest CreateDataRequestForProperty(PropertyInfo propertyInfo, DataRequest request)
        {
            return new DataRequest(request,
                                    request.Fixture,
                                    propertyInfo.PropertyType,
                                    propertyInfo.Name,
                                    true,
                                    request.Constraints,
                                    propertyInfo);
        }
    }
}
