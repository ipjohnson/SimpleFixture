using System;

namespace SimpleFixture.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    public interface IFixtureCreationAttribute
    {
        /// <summary>
        /// Create fixture
        /// </summary>
        /// <returns></returns>
        Fixture CreateFixture();
    }


    /// <summary>
    /// Attribute for creating fixture
    /// </summary>
    public abstract class FixtureCreationAttribute : Attribute, IFixtureCreationAttribute
    {
        /// <summary>
        /// Create fixture
        /// </summary>
        /// <returns></returns>
        public abstract Fixture CreateFixture();
    }
}
