using SimpleFixture.Attributes;

namespace SimpleFixture.Moq
{
    /// <summary>
    /// Initialize attribute for Moq
    /// </summary>
    public class MoqFixtureInitializeAttribute : FixtureInitializationAttribute
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public MoqFixtureInitializeAttribute()
        {
            DefaultSingleton = true;
        }

        /// <summary>
        /// Default singleton
        /// </summary>
        public bool DefaultSingleton { get; set; }


        /// <summary>
        /// Initialize fixture
        /// </summary>
        /// <param name="fixture">fixture</param>
        public override void Initialize(Fixture fixture)
        {
            fixture.Add(new MoqConvention(DefaultSingleton));
        }
    }
}
