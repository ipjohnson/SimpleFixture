namespace SimpleFixture
{
    /// <summary>
    /// This interface allows you to package a set of customization for reuse. 
    /// </summary>
    public interface IFixtureCustomization
    {
        /// <summary>
        /// Customize the fixture
        /// </summary>
        /// <param name="fixture">fixture to customize</param>
        void Customize(Fixture fixture);
    }
}
