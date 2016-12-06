using System;
using System.Collections.Generic;
using System.Reflection;

namespace SimpleFixture.Conventions
{
    /// <summary>
    /// Convention for creating IComparer
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class IComparerConvention : ITypedConvention
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
                var closedType = typeof(Comparer<>).MakeGenericType(request.RequestedType.GenericTypeArguments[0]);

                var defaultProperty = closedType.GetRuntimeProperty("Default");

                return defaultProperty.GetValue(null);
            }

            return Convention.NoValue;
        }

        /// <summary>
        /// Types the convention supports
        /// </summary>
        public IEnumerable<Type> SupportedTypes
        {
            get { yield return typeof(IComparer<>); }
        }
    }
}
