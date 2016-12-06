using SimpleFixture.Conventions;
using Xunit;

namespace SimpleFixture.Tests.FixtureTests.Primitives
{
    public class CharFixtureTests
    {
        #region Generate Tests
        [Fact]
        public void Fixture_GenerateChar_ReturnsPopulatedValue()
        {
            var fixture = new Fixture();

            var value = fixture.Generate<char>();
        }

        [Fact]
        public void Fixture_GenerateNullableChar_ReturnsPopulatedValue()
        {
            var fixture = new Fixture();

            var nullable = fixture.Generate<char?>();

            Assert.True(nullable.HasValue);
        }

        [Fact]
        public void Fixture_GenerateCharArray_ReturnsPopulateValue()
        {
            var fixture = new Fixture();

            var array = fixture.Generate<char[]>();

            Assert.NotNull(array);
            Assert.True(array.Length > 0);
        }
        #endregion

        #region Locate Tests
        [Fact]
        public void Fixture_LocateChar_ReturnLocateValue()
        {
            var fixture = new Fixture();

            var value = fixture.Locate<char>();

            Assert.Equal(CharConvention.LocateValue, value);
        }

        [Fact]
        public void Fixture_LocateNullableChar_ReturnsLocateValue()
        {
            var fixture = new Fixture();

            var nullable = fixture.Locate<char?>();

            Assert.True(nullable.HasValue);
            Assert.Equal(CharConvention.LocateValue, nullable.Value);
        }

        [Fact]
        public void Fixture_LocateCharArray_ReturnsEmptyArray()
        {
            var fixture = new Fixture();

            var array = fixture.Locate<char[]>();

            Assert.NotNull(array);
            Assert.True(array.Length == 0);
        }
        #endregion

        #region Min/Max Tests

        [Fact]
        public void Fixture_GenerateCharWithMin_ReturnsValueGreaterThanMin()
        {
            var fixture = new Fixture();

            for (var i = 0; i < 100; i++)
            {
                var min = (char)i;

                var value = fixture.Generate<char>(constraints: new { min });

                Assert.True(value >= min);
            }
        }

        [Fact]
        public void Fixture_GenerateCharWithMax_ReturnsValueLessThanMax()
        {
            var fixture = new Fixture();

            for (var i = 0; i < 100; i++)
            {
                var max = (char)(i + 10);

                var value = fixture.Generate<char>(constraints: new { max });

                Assert.True(value <= max);
            }
        }

        [Fact]
        public void Fixture_GenerateCharWithMinMax_ReturnsValueInRange()
        {
            var fixture = new Fixture();

            for (var i = 0; i < 100; i++)
            {
                var min = (char)i;
                var max = (char)(min + 10);

                var value = fixture.Generate<char>(constraints: new { min, max });

                Assert.True(value >= min);
                Assert.True(value <= max);
            }
        }
        #endregion
    }
}
