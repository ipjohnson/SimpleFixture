using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleFixture.DI;

namespace SimpleFixture
{
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
    }

    public interface IFixtureCustomization
    {
        void Customize(Fixture fixture);
    }
}
