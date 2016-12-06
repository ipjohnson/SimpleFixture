using SimpleFixture.Conventions;
using Xunit;
using SimpleFixture.Tests.Classes;

namespace SimpleFixture.Tests.FixtureTests.Primitives
{
    public class IntFixtureTests
    {
        #region Generate Tests
        [Fact]
        public void Fixture_GenerateInt_ReturnsPopulatedValue()
        {
            var fixture = new Fixture();

            var value = fixture.Generate<int>();

            Assert.True(value != 0);
        }

        [Fact]
        public void Fixture_GenerateNullableInt_ReturnsPopulatedValue()
        {
            var fixture = new Fixture();

            var nullable = fixture.Generate<int?>();

            Assert.True(nullable.HasValue);
        }

        [Fact]
        public void Fixture_GenerateIntArray_ReturnsPopulateValue()
        {
            var fixture = new Fixture();

            var array = fixture.Generate<int[]>();

            Assert.NotNull(array);
            Assert.True(array.Length > 0);
        }
        #endregion

        #region Locate Tests
        [Fact]
        public void Fixture_LocateInt_ReturnLocateValue()
        {
            var fixture = new Fixture();

            var value = fixture.Locate<int>();

            Assert.Equal(IntConvention.LocateValue, value);
        }

        [Fact]
        public void Fixture_LocateNullableInt_ReturnsLocateValue()
        {
            var fixture = new Fixture();

            var nullable = fixture.Locate<int?>();

            Assert.True(nullable.HasValue);
            Assert.Equal(IntConvention.LocateValue, nullable.Value);
        }

        [Fact]
        public void Fixture_LocateIntArray_ReturnsEmptyArray()
        {
            var fixture = new Fixture();

            var array = fixture.Locate<int[]>();

            Assert.NotNull(array);
            Assert.True(array.Length == 0);
        }
        #endregion

        #region Min/Max Tests

        [Fact]
        public void Fixture_GenerateIntWithMin_ReturnsValueGreaterThanMin()
        {
            var fixture = new Fixture();

            for (var i = 0; i < 100; i++)
            {
                var min = 1000 * i;

                var value = fixture.Generate<int>(constraints: new { min });

                Assert.True(value >= min);
            }
        }

        [Fact]
        public void Fixture_GenerateIntWithMax_ReturnsValueLessThanMax()
        {
            var fixture = new Fixture();

            for (var i = 0; i < 100; i++)
            {
                var max = 1000 * i;

                var value = fixture.Generate<int>(constraints: new { max });

                Assert.True(value <= max);
            }
        }

        [Fact]
        public void Fixture_GenerateIntWithMinMax_ReturnsValueInRange()
        {
            var fixture = new Fixture();

            for (var i = 0; i < 100; i++)
            {
                var min = 1000 * i;
                var max = min + 10;

                var value = fixture.Generate<int>(constraints: new { min, max });

                Assert.True(value >= min);
                Assert.True(value <= max);
            }
        }

        [Fact]
        public void Fixture_GenerateIntWithRangeAttribute_ReturnsValueInRange()
        {
            var fixture = new Fixture();

            var rangedClass = fixture.Generate<RangedClass>();

            Assert.NotNull(rangedClass);
            Assert.InRange(rangedClass.IntValue, 100, 200);
        }
        #endregion
    }
}
