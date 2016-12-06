using SimpleFixture.Tests.Classes;
using Xunit;

namespace SimpleFixture.Tests.FixtureTests.Complex
{
    public class ComplexConstraintTests
    {
        [Fact]
        public void Fixture_Generate_IntConstraintSeed()
        {
            var fixture = new Fixture();

            var someClass = fixture.Generate<SomeClass>(constraints: new { IntValue = 8 });

            Assert.NotNull(someClass);
            Assert.Equal(8, someClass.IntValue);
        }
    }
}
