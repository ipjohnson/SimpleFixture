using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFixture.Conventions
{
    public class IComparerConvention : ITypedConvention
    {
        public ConventionPriority Priority
        {
            get { return ConventionPriority.Last; }
        }

        public object GenerateData(DataRequest request)
        {
            if (request.RequestedType.IsConstructedGenericType)
            {
                Type closedType = typeof(Comparer<>).MakeGenericType(request.RequestedType.GenericTypeArguments[0]);

                PropertyInfo defaultProperty = closedType.GetRuntimeProperty("Default");

                return defaultProperty.GetValue(null);
            }

            return Convention.NoValue;
        }

        public IEnumerable<Type> SupportedTypes
        {
            get { yield return typeof(IComparer<>); }
        }
    }
}
