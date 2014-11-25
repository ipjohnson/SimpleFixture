using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public virtual ConventionPriority Priority
        {
            get { return ConventionPriority.Last; }
        }

        /// <summary>
        /// Generate date for the request, return Constrain.NoValue instead of null
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
    }
}
