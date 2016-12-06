using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SimpleFixture.Conventions
{
    /// <summary>
    /// Convention for creating array
    /// </summary>
    public class ArrayConvention : IConvention
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
            if (request.RequestedType.IsArray)
            {
                var method = GetType().GetTypeInfo().DeclaredMethods.First(m => m.Name == "GetArray");

                var closedMethod = method.MakeGenericMethod(request.RequestedType.GetElementType());

                return closedMethod.Invoke(this, new object[] { request });
            }

            return Convention.NoValue;
        }

        private T[] GetArray<T>(DataRequest request)
        {
            var newRequest = new DataRequest(request, typeof(IEnumerable<T>));

            var enumerable = (IEnumerable<T>) request.Fixture.Generate(newRequest);

            return enumerable.ToArray();
        }
    }
}
