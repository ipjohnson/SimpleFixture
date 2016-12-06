using System;
using System.Collections.Generic;

namespace SimpleFixture.Conventions
{
    /// <summary>
    /// Abstract simple type convention
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SimpleTypeConvention<T> : ITypedConvention
    {
        /// <summary>
        /// Priority for the convention, last by default
        /// </summary>
        public virtual ConventionPriority Priority => ConventionPriority.Last;

        /// <summary>
        /// Priorit changed event
        /// </summary>
        public event EventHandler<PriorityChangedEventArgs> PriorityChanged;

        /// <summary>
        /// Generate data for the request, return Constrain.NoValue instead of null
        /// </summary>
        /// <param name="request">data request</param>
        /// <returns>generated data</returns>
        public abstract object GenerateData(DataRequest request);

        /// <summary>
        /// Supported types
        /// </summary>
        public virtual IEnumerable<Type> SupportedTypes
        {
            get { yield return typeof(T); }
        }

        /// <summary>
        /// Raise priority changed event
        /// </summary>
        /// <param name="priority"></param>
        protected void RaisePriorityChanged(ConventionPriority priority)
        {
            if (PriorityChanged != null)
            {
                PriorityChanged(this,new PriorityChangedEventArgs{ Priority = priority});
            }
        }
    }
}
