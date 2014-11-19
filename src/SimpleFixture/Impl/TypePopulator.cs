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
        void Populate(object instance, DataRequest request);
    }

    public class TypePopulator : ITypePopulator
    {
        private readonly IConstraintHelper _helper;

        public TypePopulator(IConstraintHelper helper)
        {
            _helper = helper;
        }

        public void Populate(object instance, DataRequest request)
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
                object propertyValue = _helper.GetValue<object>(request.Constraints, null, propertyInfo.Name) ??
                                       GetPropertyValue(propertyInfo, request);

                if (propertyValue == null)
                {
                    throw new Exception("Could not create type " + propertyInfo.PropertyType.FullName);
                }

                propertyInfo.SetValue(instance, propertyValue);
            }
        }

        private object GetPropertyValue(PropertyInfo propertyInfo, DataRequest request)
        {
            DataRequest newRequest = new DataRequest(request, 
                                                     request.Fixture, 
                                                     propertyInfo.PropertyType, 
                                                     propertyInfo.Name, 
                                                     true, 
                                                     request.Constraints, 
                                                     propertyInfo);

            return newRequest.Fixture.Generate(newRequest);
        }
    }
}
