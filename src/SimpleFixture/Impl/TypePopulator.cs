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
        private readonly IConstraintHelper _helper;

        public TypePopulator(IConstraintHelper helper)
        {
            _helper = helper;
        }

        public void Populate(object instance, DataRequest request, ComplexModel model)
        {
            if (instance == null)
            {
                return;
            }

            var skipProperties = new List<string>();

            var skipPropertiesEnumerable = _helper.GetValue<IEnumerable<string>>(request.Constraints,
                                                                        null,
                                                                        "_skipProps",
                                                                        "_skipProperties");

            if (skipPropertiesEnumerable != null)
            {
                skipProperties.AddRange(skipPropertiesEnumerable);
            }

            foreach (PropertyInfo propertyInfo in instance.GetType()
                                                    .GetRuntimeProperties()
                                                    .Where(p => p.CanWrite &&
                                                                p.SetMethod.IsPublic &&
                                                               !p.SetMethod.IsStatic &&
                                                                p.SetMethod.GetParameters().Count() == 1 &&
                                                               !skipProperties.Contains(p.Name)))
            {
                object propertyValue = _helper.GetValue<object>(request.Constraints, null, propertyInfo.Name);

                var newRequest = CreateDataRequestForProperty(propertyInfo, request);

                if (model.SkipProperty(newRequest, propertyInfo))
                {
                    continue;
                }

                if (propertyValue != null)
                {
                    propertyValue = newRequest.Fixture.Behavior.Apply(newRequest, propertyValue);
                }
                else
                {
                    if (!model.GetPropertyValue(newRequest, propertyInfo, out propertyValue))
                    {
                        propertyValue = newRequest.Fixture.Generate(newRequest);
                    }
                }

                if (propertyValue == null)
                {
                    throw new Exception("Could not create type " + propertyInfo.PropertyType.FullName);
                }

                propertyInfo.SetValue(instance, propertyValue);
            }
        }

        private DataRequest CreateDataRequestForProperty(PropertyInfo propertyInfo, DataRequest request)
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
