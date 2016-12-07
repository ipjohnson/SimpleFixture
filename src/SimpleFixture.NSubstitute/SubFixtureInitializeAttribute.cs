using SimpleFixture.Attributes;

namespace SimpleFixture.NSubstitute
{
    /// <summary>
    /// Attribute to initialize a fixture, used by SimpleFixture.xUnit
    /// </summary>
    public class SubFixtureInitializeAttribute : FixtureInitializationAttribute
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public SubFixtureInitializeAttribute()
        {
            DefaultSingleton = true;
        }

        /// <summary>
        /// Default interfaces to singleton
        /// </summary>
        public bool DefaultSingleton { get; set; }

        /// <summary>
        /// Initialize fixture
        /// </summary>
        /// <param name="fixture"></param>
        public override void Initialize(Fixture fixture)
        {
            fixture.Add(new SubstituteConvention(DefaultSingleton));
        }
    }
}
