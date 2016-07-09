using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFixture.Impl
{
    public interface ITypeCreator
    {
        object CreateType(DataRequest request, ComplexModel model);
    }

    public class TypeCreator : ITypeCreator
    {
        private IFixtureConfiguration _configuration;
        private readonly IConstraintHelper _constraintHelper;
        private readonly IConstructorSelector _selector;

        public TypeCreator(IFixtureConfiguration configuration, IConstructorSelector selector, IConstraintHelper constraintHelper)
        {
            _configuration = configuration;
            _selector = selector;
            _constraintHelper = constraintHelper;
        }

        public object CreateType(DataRequest request, ComplexModel model)
        {
            if (model.New != null)
            {
                return model.New(request);
            }

            ConstructorInfo constructorInfo = _selector.SelectConstructor(request.RequestedType);

            if (constructorInfo == null)
            {
                if (request.RequestedType.GetTypeInfo().IsValueType)
                {
                    return Activator.CreateInstance(request.RequestedType);
                }

                throw new Exception("Could not find public constructor on type " + request.RequestedType.FullName);
            }

            return InjectConstructor(constructorInfo, request);
        }

        private object InjectConstructor(ConstructorInfo method, DataRequest request)
        {
            List<object> parameters = new List<object>();

            foreach (ParameterInfo parameterInfo in method.GetParameters())
            {
                object parameterValue = null;

                if (_configuration.CircularReferenceHandling == CircularReferenceHandlingAlgorithm.AutoWire)
                {
                    var currentRequest = request;
                    var requestTypeInfo = parameterInfo.ParameterType .GetTypeInfo();

                    while (currentRequest != null)
                    {
                        if (requestTypeInfo.IsAssignableFrom(currentRequest.RequestedType.GetTypeInfo()))
                        {
                            parameterValue = currentRequest.Instance;
                            break;
                        }

                        currentRequest = currentRequest.ParentRequest;
                    }
                }

                bool foundValue = false;
                var newRequest = CreateDataRequestForParameter(parameterInfo, request);

                if (parameterValue == null)
                {
                    foundValue = _constraintHelper.GetUnTypedValue(out parameterValue, parameterInfo.ParameterType, request.Constraints, null, parameterInfo.Name);

                    if (parameterValue != null)
                    {
                        newRequest.Fixture.Behavior.Apply(newRequest, parameterValue);
                    }
                }

                if (!foundValue)
                {
                    parameterValue = parameterValue ?? newRequest.Fixture.Generate(newRequest);

                    if (parameterValue == null)
                    {
                        throw new Exception("Could not create Type " + parameterInfo.ParameterType.FullName);
                    }
                }

                parameters.Add(parameterValue);
            }

            return method.Invoke(parameters.ToArray());
        }

        private DataRequest CreateDataRequestForParameter(ParameterInfo parameterInfo, DataRequest request)
        {
            return new DataRequest(request,
                                   request.Fixture,
                                   parameterInfo.ParameterType,
                                   DependencyType.ConstructorDependency,
                                   parameterInfo.Name,
                                   request.Populate,
                                   request.Constraints,
                                   parameterInfo);

        }
    }
}
