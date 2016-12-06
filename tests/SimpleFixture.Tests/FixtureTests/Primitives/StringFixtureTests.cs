using FluentAssertions;
using SimpleFixture.Tests.Classes;
using System.Linq;
using Xunit;

namespace SimpleFixture.Tests.FixtureTests.Primitives
{
    public class StringFixtureTests
    {
        [Fact]
        public void Fixture_RangedClass_StringCorrectLength()
        {
            var fixture = new Fixture();

            var ranged = fixture.Generate<RangedClass>();
            
            ranged.TestString.Length.Should().BeInRange(50, 100);
            ranged.FirstName.All(char.IsLetter).Should().BeTrue();
        }
    }
}
