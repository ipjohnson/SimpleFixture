using System;
using System.Collections.Generic;
using System.Reflection;

namespace SimpleFixture.Conventions
{
    /// <summary>
    /// Convention for  IEqualityComparer
    /// </summary>
    public class IEqualityComparerConvention : ITypedConvention
    {
        /// <summary>
        /// Prioirity the convention should be looked at
        /// </summary>
        public ConventionPriority Priority => ConventionPriority.Last;

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
