using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SimpleFixture.Conventions
{
    /// <summary>
    /// Convention for building delegate
    /// </summary>
    public class DelegateConvention : IConvention
    {
        /// <summary>
        /// Prioirity the convention should be looked at
        /// </summary>
        public ConventionPriority Priority => ConventionPriority.Low;

        /// <summary>
        /// Priorit changed event
        /// </summary>
        public event EventHandler<PriorityChangedEventArgs> PriorityChanged;

        /// <summary>
        /// Generate data for the request, return Convention.NoValue if the convention has no value to provide
        /// </summary>
        /// <param name="request">data request</param>
        /// <returns>generated data value</returns>
        public object GenerateData(DataRequest request)
        {
            if (typeof(MulticastDelegate).GetTypeInfo().IsAssignableFrom(request.RequestedType.GetTypeInfo()))
            {
                var buildMethod = GetType().GetRuntimeMethods().FirstOrDefault(m => m.Name == "BuildMethod");

                var closedMethod = buildMethod.MakeGenericMethod(request.RequestedType);

                return closedMethod.Invoke(this, new object[] { request });
            }

            return Convention.NoValue;
        }

        private T BuildMethod<T>(DataRequest request)
        {
            var delegateType = typeof(T);
            var method = delegateType.GetTypeInfo().GetDeclaredMethod("Invoke");

            if (method == null)
            {
                throw new Exception("Wrong type not delegate " + typeof(T).FullName);
            }

            var parameters = new List<ParameterExpression>();

            foreach (var parameterInfo in method.GetParameters())
            {
                parameters.Add(Expression.Parameter(parameterInfo.ParameterType, parameterInfo.Name));
            }

            if (method.ReturnType == typeof(void))
            {
                var noopMethod = GetType().GetRuntimeMethods().First(m => m.Name == "Noop");

                return Expression.Lambda<T>(Expression.Call(noopMethod), parameters.ToArray()).Compile();
            }

            var getMethod = GetType().GetRuntimeMethods().First(m => m.Name == "GetValueFunc");

            return Expression.Lambda<T>(Expression.Call(getMethod.MakeGenericMethod(method.ReturnType),
                                        Expression.Constant(request))).Compile();

        }

        private static void Noop()
        {

        }

        private static TValue GetValueFunc<TValue>(DataRequest request)
        {
            var newRequest = new DataRequest(request, typeof(TValue));

            return (TValue)newRequest.Fixture.Generate(newRequest);
        }
    }
}
