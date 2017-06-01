using System;

namespace SimpleFixture.Attributes
{
    /// <summary>
    /// attributes that implement this interface will be called to initialize fixture
    /// </summary>
    public interface IFixtureInitializationAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fixture"></param>
        void Initialize(Fixture fixture);
    }

    /// <summary>
    /// Attribute for initializing fixture
    /// </summary>
    public abstract class FixtureInitializationAttribute : Attribute, IFixtureInitializationAttribute
    {
        /// <summary>
        /// Initialize fixture
        /// </summary>
        /// <param name="fixture">fixture</param>
        public abstract void Initialize(Fixture fixture);
    }
}
