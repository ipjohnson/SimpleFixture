using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace SimpleFixture.Conventions
{
    /// <summary>
    /// Convention for creating readonly collection
    /// </summary>
    public class ReadOnlyCollectionConvention : ITypedConvention
    {
        /// <summary>
        /// Prioirity the convention should be looked at
        /// </summary>
        public ConventionPriority Priority => ConventionPriority.Last;

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
                var methodInfo =
                    GetType().GetTypeInfo().DeclaredMethods.First(m => m.Name == "GetReadOnlyList");

                var closedMethod = methodInfo.MakeGenericMethod(request.RequestedType.GenericTypeArguments);

                return closedMethod.Invoke(this, new object[] { request });
            }

            return Convention.NoValue;
        }

        /// <summary>
        /// Types the convention supports
        /// </summary>
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

            var returnValue = newRequest.Fixture.Generate(newRequest) as List<TValue>;

            return new ReadOnlyCollection<TValue>(returnValue);
        }

    }
}
