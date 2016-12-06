using FluentAssertions;
using SimpleFixture.Tests.Classes;
using System.Linq;
using SimpleFixture.Impl;
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

        [Fact]
        public void Generate_String_Alpha()
        {
            var fixture = new Fixture();

            var stringValue = fixture.Generate<string>(constraints: new {stringType = StringType.Alpha});

            Assert.True(stringValue.All(char.IsLetter));
        }

        [Fact]
        public void Generate_String_LoremIpsum()
        {
            var fixture = new Fixture();

            var stringValue = fixture.Generate<string>(constraints: new { stringType = StringType.LoremIpsum });

            Assert.True(RandomDataGeneratorService.LoremIpsum.StartsWith(stringValue));
        }
    }
}
