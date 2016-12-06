using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SimpleFixture.Conventions
{
    /// <summary>
    /// Convention for creating dictionary
    /// </summary>
    public class DictionaryConvention : ITypedConvention
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
            if (request.RequestedType.IsConstructedGenericType)
            {
                var openType = request.RequestedType.GetGenericTypeDefinition();

                if (openType == typeof(IDictionary<,>) || openType == typeof(Dictionary<,>))
                {
                    var methodInfo =
                        GetType().GetRuntimeMethods().First(m => m.Name == "GetDictionary");

                    var closedMethod = methodInfo.MakeGenericMethod(request.RequestedType.GenericTypeArguments);

                    return closedMethod.Invoke(this, new object[] { request });
                }
            }

            return Convention.NoValue;
        }

        private object GetDictionary<TKey, TValue>(DataRequest request)
        {
            var newRequest = new DataRequest(request, typeof(IEnumerable<KeyValuePair<TKey,TValue>>));

            var values =
                (IEnumerable<KeyValuePair<TKey, TValue>>)request.Fixture.Generate(newRequest);

            var returnValues = new Dictionary<TKey, TValue>();

            foreach (var keyValuePair in values)
            {
                returnValues[keyValuePair.Key] = keyValuePair.Value;
            }

            return returnValues;
        }

        public IEnumerable<Type> SupportedTypes
        {
            get
            {
                yield return typeof(Dictionary<,>);
                yield return typeof(IDictionary<,>);
            }
        }
    }
}
