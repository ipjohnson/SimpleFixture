using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFixture
{
    /// <summary>
    /// class with special values
    /// </summary>
    public class Convention
    {
        /// <summary>
        /// Value to return instead of null
        /// </summary>
        public static object NoValue = new object();
    }

    /// <summary>
    /// Convention priority
    /// </summary>
    public enum ConventionPriority
    {
        /// <summary>
        /// First convention to try
        /// </summary>
        First,
        /// <summary>
        /// High priority
        /// </summary>
        High,

        /// <summary>
        /// Normal priority
        /// </summary>
        Normal,

        /// <summary>
        /// Low priority
        /// </summary>
        Low,

        /// <summary>
        /// Last convention tried
        /// </summary>
        Last
    }
    
    /// <summary>
    /// Convention for satisifying a data request
    /// </summary>
    public interface IConvention
    {
        /// <summary>
        /// Prioirity the convention should be looked at
        /// </summary>
        ConventionPriority Priority { get; }

        /// <summary>
        /// Generate data for the request, return Convention.NoValue if the convention has no value to provide
        /// </summary>
        /// <param name="request">data request</param>
        /// <returns>generated data value</returns>
        object GenerateData(DataRequest request);
    }
}
