using System;
using System.Collections.Generic;
using System.Reflection;

namespace SimpleFixture.Conventions
{
    public class IComparerConvention : ITypedConvention
    {
        public ConventionPriority Priority => ConventionPriority.Last;

        public event EventHandler<PriorityChangedEventArgs> PriorityChanged;

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
