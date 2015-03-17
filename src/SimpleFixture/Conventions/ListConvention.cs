using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SimpleFixture.Impl;

namespace SimpleFixture.Conventions
{
    public class ListConvention : ITypedConvention
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
                    GetType().GetTypeInfo().DeclaredMethods.First(m => m.Name == "GetList");

                MethodInfo closedMethod = methodInfo.MakeGenericMethod(request.RequestedType.GenericTypeArguments);

                return closedMethod.Invoke(this, new object[] { request });
            }

            return Convention.NoValue;
        }

        private object GetList<TValue>(DataRequest request)
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

            return returnValue;
        }

        public IEnumerable<Type> SupportedTypes
        {
            get
            {
                yield return typeof(IEnumerable<>);
                yield return typeof(ICollection<>);
                yield return typeof(IList<>);
                yield return typeof(List<>);
            }
        }
    }
}
