using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFixture.Conventions
{
    public class DictionaryConvention : ITypedConvention
    {
        public ConventionPriority Priority
        {
            get { return ConventionPriority.Low; }
        }

        public event EventHandler<PriorityChangedEventArgs> PriorityChanged;

        public object GenerateData(DataRequest request)
        {
            if (request.RequestedType.IsConstructedGenericType)
            {
                var openType = request.RequestedType.GetGenericTypeDefinition();

                if (openType == typeof(IDictionary<,>) || openType == typeof(Dictionary<,>))
                {
                    MethodInfo methodInfo =
                        GetType().GetRuntimeMethods().First(m => m.Name == "GetDictionary");

                    MethodInfo closedMethod = methodInfo.MakeGenericMethod(request.RequestedType.GenericTypeArguments);

                    return closedMethod.Invoke(this, new object[] { request });
                }
            }

            return Convention.NoValue;
        }

        private object GetDictionary<TKey, TValue>(DataRequest request)
        {
            DataRequest newRequest = new DataRequest(request, typeof(IEnumerable<KeyValuePair<TKey,TValue>>));

            IEnumerable<KeyValuePair<TKey, TValue>> values =
                (IEnumerable<KeyValuePair<TKey, TValue>>)request.Fixture.Generate(newRequest);

            Dictionary<TKey,TValue> returnValues = new Dictionary<TKey, TValue>();

            foreach (KeyValuePair<TKey, TValue> keyValuePair in values)
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
