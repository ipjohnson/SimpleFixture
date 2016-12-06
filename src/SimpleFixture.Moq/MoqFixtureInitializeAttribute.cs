using SimpleFixture.Attributes;

namespace SimpleFixture.Moq
{
    public class MoqFixtureInitializeAttribute : FixtureInitializationAttribute
    {
        public MoqFixtureInitializeAttribute()
        {
            DefaultSingleton = true;
        }

        public bool DefaultSingleton { get; set; }

        public override void Initialize(Fixture fixture)
        {
            fixture.Add(new MoqConvention(DefaultSingleton));
        }
    }
}
