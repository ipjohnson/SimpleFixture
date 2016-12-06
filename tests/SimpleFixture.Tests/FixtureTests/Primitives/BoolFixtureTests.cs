using SimpleFixture.Conventions;
using Xunit;

namespace SimpleFixture.Tests.FixtureTests.Primitives
{
    public class BoolFixtureTests
    {
        #region Generate Tests
        [Fact]
        public void Fixture_GenerateBool_ReturnsPopulatedValue()
        {
            var fixture = new Fixture();

            var value = fixture.Generate<bool>();
        }

        [Fact]
        public void Fixture_GenerateNullableBool_ReturnsPopulatedValue()
        {
            var fixture = new Fixture();

            var nullable = fixture.Generate<bool?>();

            Assert.True(nullable.HasValue);
        }

        [Fact]
        public void Fixture_GenerateBoolArray_ReturnsPopulateValue()
        {
            var fixture = new Fixture();

            var array = fixture.Generate<bool[]>();

            Assert.NotNull(array);
            Assert.True(array.Length > 0);
        }
        #endregion

        #region Locate Tests
        [Fact]
        public void Fixture_LocateBool_ReturnLocateValue()
        {
            var fixture = new Fixture();

            var value = fixture.Locate<bool>();

            Assert.Equal(BoolConvention.LocateValue, value);
        }

        [Fact]
        public void Fixture_LocateNullableBool_ReturnsLocateValue()
        {
            var fixture = new Fixture();

            var nullable = fixture.Locate<bool?>();

            Assert.True(nullable.HasValue);
            Assert.Equal(BoolConvention.LocateValue, nullable.Value);
        }

        [Fact]
        public void Fixture_LocateBoolArray_ReturnsEmptyArray()
        {
            var fixture = new Fixture();

            var array = fixture.Locate<bool[]>();

            Assert.NotNull(array);
            Assert.True(array.Length == 0);
        }
        #endregion
    }
}

