using SimpleFixture.Attributes;

namespace SimpleFixture.NSubstitute
{
    /// <summary>
    /// Attribute to initialize a fixture, used by SimpleFixture.xUnit
    /// </summary>
    public class SubFixtureInitializeAttribute : FixtureInitializationAttribute
    {
        public SubFixtureInitializeAttribute()
        {
            DefaultSingleton = true;
        }

        public bool DefaultSingleton { get; set; }

        public override void Initialize(Fixture fixture)
        {
            fixture.Add(new SubstituteConvention(DefaultSingleton));
        }
    }
}
