using System;
using System.Collections.Generic;

namespace SimpleFixture
{
    /// <summary>
    /// Conventions implementing this interface support a specific set of types
    /// </summary>
    public interface ITypedConvention : IConvention
    {
        /// <summary>
        /// Types the convention supports
        /// </summary>
        IEnumerable<Type> SupportedTypes { get; }
    }
}
