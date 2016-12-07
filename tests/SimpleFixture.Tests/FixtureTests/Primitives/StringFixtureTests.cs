using FluentAssertions;
using SimpleFixture.Tests.Classes;
using System.Linq;
using SimpleFixture.Conventions;
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
        public void Fixture_Locate_String()
        {
            var fixture = new Fixture();

            var value = fixture.Locate<string>();

            Assert.Equal(StringConvention.LocateValue, value);
        }

        [Fact]
        public void Generate_String_With_Prefix()
        {
            var fixture = new Fixture();

            var stringValue = fixture.Generate<string>(constraints: new { preFix = "Hello" });

            Assert.True(stringValue.StartsWith("Hello"));
        }

        [Fact]
        public void Generate_String_With_Postfix()
        {
            var fixture = new Fixture();

            var stringValue = fixture.Generate<string>(constraints: new { postFix = "World" });

            Assert.True(stringValue.EndsWith("World"));
        }

        [Fact]
        public void Generate_String_Min_Greater_Than_Max()
        {
            var fixture = new Fixture();

            var stringValue = fixture.Generate<string>(constraints: new { min = 11, max = 10 });

            Assert.Equal(10, stringValue.Length);
        }

        [Fact]
        public void Generate_String_Alpha()
        {
            var fixture = new Fixture();

            var stringValue = fixture.Generate<string>(constraints: new { stringType = StringType.Alpha });

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
