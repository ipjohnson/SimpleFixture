using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace SimpleFixture.Conventions
{
    public class ReadOnlyCollectionConvention : ITypedConvention
    {
        public ConventionPriority Priority => ConventionPriority.Last;

        public event EventHandler<PriorityChangedEventArgs> PriorityChanged;

        public object GenerateData(DataRequest request)
        {
            if (request.RequestedType.IsConstructedGenericType)
            {
                MethodInfo methodInfo =
                    GetType().GetTypeInfo().DeclaredMethods.First(m => m.Name == "GetReadOnlyList");

                MethodInfo closedMethod = methodInfo.MakeGenericMethod(request.RequestedType.GenericTypeArguments);

                return closedMethod.Invoke(this, new object[] { request });
            }

            return Convention.NoValue;
        }
        
        public IEnumerable<Type> SupportedTypes
        {
            get
            {
                yield return typeof(IReadOnlyCollection<>);
                yield return typeof(IReadOnlyList<>);
                yield return typeof(ReadOnlyCollection<>);
            }
        }

        private object GetReadOnlyList<TValue>(DataRequest request)
        {
            var newRequest = new DataRequest(request, typeof(List<TValue>));

            List<TValue> returnValue = newRequest.Fixture.Generate(newRequest) as List<TValue>;

            return new ReadOnlyCollection<TValue>(returnValue);
        }

    }
}
