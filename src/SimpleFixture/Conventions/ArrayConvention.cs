using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SimpleFixture.Conventions
{
    public class ArrayConvention : IConvention
    {
        public ConventionPriority Priority => ConventionPriority.Low;

        public event EventHandler<PriorityChangedEventArgs> PriorityChanged;

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
            DataRequest newRequest = new DataRequest(request, typeof(IEnumerable<T>));

            IEnumerable<T> enumerable = (IEnumerable<T>) request.Fixture.Generate(newRequest);

            return enumerable.ToArray();
        }
    }
}
