using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SimpleFixture.Impl;

namespace SimpleFixture.Conventions
{
    public class ReadOnlyCollectionConvention : ITypedConvention
    {
        public ConventionPriority Priority
        {
            get { return ConventionPriority.Last; }
        }

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
            List<TValue> returnValue = new List<TValue>();

            if (request.Populate)
            {
                int count = request.Fixture.Configuration.ItemCount.HasValue
                    ? request.Fixture.Configuration.ItemCount.Value
                    : request.Fixture.Configuration.Locate<IRandomDataGeneratorService>().NextInt(5, 10);

                for (int i = 0; i < count; i++)
                {
                    DataRequest newRequest = new DataRequest(request, typeof(TValue));

                    TValue newValue = (TValue)newRequest.Fixture.Generate(newRequest);

                    returnValue.Add(newValue);
                }
            }

            return new ReadOnlyCollection<TValue>(returnValue);
        }

    }
}
