using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleFixture.DI;

namespace SimpleFixture
{
    /// <summary>
    /// 
    /// </summary>
    public enum CircularReferenceHandlingAlgorithm
    {
        /// <summary>
        /// Throws an exception when a max depth is reached, this is the original algorithm
        /// </summary>
        MaxDepth,

        /// <summary>
        /// Omit circular properties, skips properties that are circular and returns empty collections when circular
        /// </summary>
        OmitCircularReferences,

        /// <summary>
        /// Autowire circular references, if a parent in the object graph can be used it will be
        /// </summary>
        AutoWire,
    }

    /// <summary>
    /// Configuration interface for SimpleFixture. Only implement this interface if you want to change the internal worksings of SimpleFixture
    /// </summary>
    public interface IFixtureConfiguration : IGContainer
    {
        /// <summary>
        /// Use default conventions for primitive types as well as string
        /// </summary>
        bool UseDefaultConventions { get; }

        /// <summary>
        /// Use conventions that try and assign values based on parameter or property names
        /// </summary>
        bool UseNamedConventions { get; }

        /// <summary>
        /// If set this will indicate how many items to generate for IEnumerables and other colletions.
        /// If null then the convention is free to do what it wants
        /// </summary>
        int? ItemCount { get; }

        /// <summary>
        /// Populate public properties
        /// </summary>
        bool PopulateProperties { get; }

        /// <summary>
        /// Populate public fields
        /// </summary>
        bool PopulateFields { get; }

        /// <summary>
        /// How to handle circular references
        /// </summary>
        CircularReferenceHandlingAlgorithm CircularReferenceHandling { get; }
    }
}
