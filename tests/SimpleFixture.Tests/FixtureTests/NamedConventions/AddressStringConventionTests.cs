using System.Linq;
using Xunit;

namespace SimpleFixture.Tests.FixtureTests.NamedConventions
{
    public class AddressStringConventionTests
    {
        [Theory, InlineData("PostalCode"), InlineData("ZipCode"),InlineData("Zip")]
        public void Fixture_GeneratePostalCode_ReturnsGoodValue(string name)
        {
            var fixture = new Fixture();

            var value = fixture.Generate<string>(name);

            Assert.NotNull(value);
            Assert.True(value.All(char.IsDigit));
        }

        [Theory]
        [InlineData("AddressLine")]
        [InlineData("AddressLine1")]
        [InlineData("AddressLineOne")]
        public void Fixture_GenerateAddressLineOne_ReturnsGoodValue(string name)
        {
            var fixture = new Fixture();

            var value = fixture.Generate<string>(name);

            Assert.NotNull(value);
            Assert.True(value.All(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c)));
        }

        [Theory]
        [InlineData("AddressLineTwo")]
        [InlineData("AddressLine2")]
        public void Fixture_GenerateAddressLineTwo_ReturnsGoodValue(string name)
        {
            var fixture = new Fixture();

            var value = fixture.Generate<string>(name);

            Assert.NotNull(value);
            Assert.True(value.All(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c)));
        }

        [Fact]
        public void Fixture_GenerateState_ReturnsGoodValue()
        {
            var fixture = new Fixture();

            var value = fixture.Generate<string>("State");

            Assert.NotNull(value);
            Assert.True(value.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)));
        }

        [Fact]
        public void Fixture_GenerateStateAbbreviation_ReturnsGoodValue()
        {
            var fixture = new Fixture();

            var value = fixture.Generate<string>("StateAbbreviation");

            Assert.NotNull(value);
            Assert.Equal(2, value.Length);
            Assert.True(value.All(char.IsLetter));
        }

        [Fact]
        public void Fixture_GenerateCountry_ReturnsGoodValue()
        {
            var fixture = new Fixture();

            var value = fixture.Generate<string>("Country");

            Assert.NotNull(value);
            Assert.True(value.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)));
        }
        [Fact]
        public void Fixture_GenerateCity_ReturnsGoodValue()
        {
            var fixture = new Fixture();

            var value = fixture.Generate<string>("City");

            Assert.NotNull(value);
            Assert.True(value.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)));
        }
    }
}
