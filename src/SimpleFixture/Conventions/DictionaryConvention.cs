using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SimpleFixture.Conventions
{
    public class DictionaryConvention : ITypedConvention
    {
        public ConventionPriority Priority => ConventionPriority.Low;

        public event EventHandler<PriorityChangedEventArgs> PriorityChanged;

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
