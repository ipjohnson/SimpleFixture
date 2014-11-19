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
        object CreateType(DataRequest request);
    }

    public class TypeCreator : ITypeCreator
    {
        private readonly IConstraintHelper _constraintHelper;
        private IConstructorSelector _selector;

        public TypeCreator(IConstructorSelector selector, IConstraintHelper constraintHelper)
        {
            _selector = selector;
            _constraintHelper = constraintHelper;
        }

        public object CreateType(DataRequest request)
        {
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
                object parameterValue = _constraintHelper.GetValue<object>(request.Constraints, null, parameterInfo.Name) ??
                                        GetValueForParameter(parameterInfo, request);

                if (parameterValue == null)
                {
                    throw new Exception("Could not create Type " + parameterInfo.ParameterType.FullName);
                }

                parameters.Add(parameterValue);
            }

            return method.Invoke(parameters.ToArray());
        }

        private object GetValueForParameter(ParameterInfo parameterInfo, DataRequest request)
        {
            var newRequest = new DataRequest(request, request.Fixture, parameterInfo.ParameterType, parameterInfo.Name, request.Populate, request.Constraints, parameterInfo);

            return newRequest.Fixture.Generate(newRequest);
        }
    }
}
