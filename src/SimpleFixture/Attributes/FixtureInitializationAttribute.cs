using System;

namespace SimpleFixture.Attributes
{
    /// <summary>
    /// Attribute for initializing fixture
    /// </summary>
    public abstract class FixtureInitializationAttribute : Attribute
    {
        /// <summary>
        /// Initialize fixture
        /// </summary>
        /// <param name="fixture">fixture</param>
        public abstract void Initialize(Fixture fixture);
    }
}
