using SimpleFixture.Conventions;
using Xunit;

namespace SimpleFixture.Tests.FixtureTests.Primitives
{
    public class ULongFixtureTests
    {
        #region Generate Tests
        [Fact]
        public void Fixture_GenerateULong_ReturnsPopulatedValue()
        {
            var fixture = new Fixture();

            var value = fixture.Generate<ulong>();

            Assert.True(value != 0);
        }

        [Fact]
        public void Fixture_GenerateNullableULong_ReturnsPopulatedValue()
        {
            var fixture = new Fixture();

            var nullable = fixture.Generate<ulong?>();

            Assert.True(nullable.HasValue);
        }

        [Fact]
        public void Fixture_GenerateULongArray_ReturnsPopulateValue()
        {
            var fixture = new Fixture();

            var array = fixture.Generate<ulong[]>();

            Assert.NotNull(array);
            Assert.True(array.Length > 0);
        }
        #endregion

        #region Locate Tests
        [Fact]
        public void Fixture_LocateULong_ReturnLocateValue()
        {
            var fixture = new Fixture();

            var value = fixture.Locate<ulong>();

            Assert.Equal(ULongConvention.LocateValue, value);
        }

        [Fact]
        public void Fixture_LocateNullableULong_ReturnsLocateValue()
        {
            var fixture = new Fixture();

            var nullable = fixture.Locate<ulong?>();

            Assert.True(nullable.HasValue);
            Assert.Equal(ULongConvention.LocateValue, nullable.Value);
        }

        [Fact]
        public void Fixture_LocateULongArray_ReturnsEmptyArray()
        {
            var fixture = new Fixture();

            var array = fixture.Locate<ulong[]>();

            Assert.NotNull(array);
            Assert.True(array.Length == 0);
        }
        #endregion

        #region Min/Max Tests

        [Fact]
        public void Fixture_GenerateULongWithMin_ReturnsValueGreaterThanMin()
        {
            var fixture = new Fixture();

            for (var i = 0; i < 100; i++)
            {
                var min = (ulong)(1000 * i);

                var value = fixture.Generate<ulong>(constraints: new { min });

                Assert.True(value >= min);
            }
        }

        [Fact]
        public void Fixture_GenerateULongWithMax_ReturnsValueLessThanMax()
        {
            var fixture = new Fixture();

            for (var i = 0; i < 100; i++)
            {
                var max = (ulong)(1000 * i);

                var value = fixture.Generate<ulong>(constraints: new { max });

                Assert.True(value <= max);
            }
        }

        [Fact]
        public void Fixture_GenerateULongWithMinMax_ReturnsValueInRange()
        {
            var fixture = new Fixture();

            for (var i = 0; i < 100; i++)
            {
                var min = (ulong)(1000 * i);
                var max = (ulong)(min + 10);

                var value = fixture.Generate<ulong>(constraints: new { min, max });

                Assert.True(value >= min);
                Assert.True(value <= max);
            }
        }
        #endregion
    }
}
