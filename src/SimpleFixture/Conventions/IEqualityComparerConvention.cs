using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFixture.Conventions
{
    public class IEqualityComparerConvention : ITypedConvention
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
                Type closedType = typeof(EqualityComparer<>).MakeGenericType(request.RequestedType.GenericTypeArguments[0]);

                PropertyInfo defualtProperty = closedType.GetRuntimeProperty("Default");

                return defualtProperty.GetValue(null);
            }

            return Convention.NoValue;
        }

        public IEnumerable<Type> SupportedTypes
        {
            get { yield return typeof(IEqualityComparer<>); }
        }
    }
}
