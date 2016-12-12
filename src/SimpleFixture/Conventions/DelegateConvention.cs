using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using SimpleFixture.Impl;

namespace SimpleFixture.Conventions
{
    /// <summary>
    /// Convention for building delegate
    /// </summary>
    public class DelegateConvention : IConvention
    {
        private readonly IConstraintHelper _constraintHelper;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="constraintHelper"></param>
        public DelegateConvention(IConstraintHelper constraintHelper)
        {
            _constraintHelper = constraintHelper;
        }

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

            var parameterNameArray = parameters.Select(p => p.Name).ToArray();

            var parameterValueArray = Expression.NewArrayInit(typeof(object), 
                    parameters.Select(e => e.Type == typeof(object) ? (Expression)e : Expression.Convert(e, typeof(object))));

            return Expression.Lambda<T>(Expression.Call(getMethod.MakeGenericMethod(method.ReturnType),
                                        Expression.Constant(request),
                                        Expression.Constant(parameterNameArray),
                                        parameterValueArray),
                                        parameters.ToArray()).Compile();

        }

        private static void Noop()
        {

        }

        private static TValue GetValueFunc<TValue>(DataRequest request, string[] parameterNames, object[] values)
        {
            IDictionary<string, object> constraintValues = null;

            if (request.Constraints != null)
            {
                constraintValues = request.Constraints as IDictionary<string, object>;

                if (constraintValues == null)
                {
                    constraintValues = new Dictionary<string, object>();

                    foreach (var property in request.Constraints.GetType().GetRuntimeProperties())
                    {
                        if (!property.CanRead ||
                            !property.GetMethod.IsPublic ||
                            property.GetMethod.IsStatic)
                        {
                            continue;
                        }

                        constraintValues[property.Name] = property.GetValue(request.Constraints);
                    }
                }
            }

            if (constraintValues == null)
            {
                constraintValues = new Dictionary<string, object>();
            }
            
            for (int i = 0; i < parameterNames.Length && i < values.Length; i++)
            {
                constraintValues[parameterNames[i]] = values[i];
            }
            
            var newRequest = new DataRequest(request, request.Fixture, typeof(TValue), DependencyType.Unknown, "", request.Populate, constraintValues, null);

            return (TValue)newRequest.Fixture.Generate(newRequest);
        }
    }
}
