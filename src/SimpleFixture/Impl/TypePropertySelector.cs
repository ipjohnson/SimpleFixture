using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFixture.Impl
{
    public interface ITypePropertySelector
    {
        IEnumerable<PropertyInfo> SelectProperties(object instance, DataRequest request, ComplexModel model);
    }

    public class TypePropertySelector : ITypePropertySelector
    {
        private IFixtureConfiguration _configuration;
        private IConstraintHelper _helper;

        public TypePropertySelector(IFixtureConfiguration configuration, IConstraintHelper helper)
        {
            _configuration = configuration;
            _helper = helper;
        }

        public virtual IEnumerable<PropertyInfo> SelectProperties(object instance, DataRequest request, ComplexModel model)
        {
            var skipProperties = new List<string>();

            var skipPropertiesEnumerable = _helper.GetValue<IEnumerable<string>>(request.Constraints,
                                                                        null,
                                                                        "_skipProps",
                                                                        "_skipProperties");

            if (skipPropertiesEnumerable != null)
            {
                skipProperties.AddRange(skipPropertiesEnumerable);
            }

            var returnProperties = instance.GetType()
                                    .GetRuntimeProperties()
                                    .Where(p => p.CanWrite &&
                                                p.SetMethod.IsPublic &&
                                               !p.SetMethod.IsStatic &&
                                                p.SetMethod.GetParameters().Count() == 1 &&
                                               !skipProperties.Contains(p.Name));

            if(request.ParentRequest != null &&
                _configuration.CircularReferenceHandling == CircularReferenceHandlingAlgorithm.OmitCircularReferences)
            {
                returnProperties = CheckForCircularProperties(request, returnProperties);
            }

            return returnProperties;
        }

        private IEnumerable<PropertyInfo> CheckForCircularProperties(DataRequest request, IEnumerable<PropertyInfo> returnProperties)
        {
            var requestTypeInfo = request.RequestedType.GetTypeInfo();
            bool circular = false;

            foreach (var propertyInfo in returnProperties)
            {
                var currentRequest = request;

                while (currentRequest != null)
                {
                    if (currentRequest.Instance != null &&
                       requestTypeInfo.IsAssignableFrom(currentRequest.RequestedType.GetTypeInfo()))
                    {
                        circular = true;
                        break;
                    }
                }

                if (!circular)
                {
                    yield return propertyInfo;
                }
            }
        }
    }
}
