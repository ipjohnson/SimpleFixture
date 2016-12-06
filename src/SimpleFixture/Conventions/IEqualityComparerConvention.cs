using System;
using System.Collections.Generic;
using System.Reflection;

namespace SimpleFixture.Conventions
{
    /// <summary>
    /// Convention for  IEqualityComparer
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class IEqualityComparerConvention : ITypedConvention
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
                var closedType = typeof(EqualityComparer<>).MakeGenericType(request.RequestedType.GenericTypeArguments[0]);

                var defualtProperty = closedType.GetRuntimeProperty("Default");

                return defualtProperty.GetValue(null);
            }

            return Convention.NoValue;
        }

        /// <summary>
        /// Types the convention supports
        /// </summary>
        public IEnumerable<Type> SupportedTypes
        {
            get { yield return typeof(IEqualityComparer<>); }
        }
    }
}
