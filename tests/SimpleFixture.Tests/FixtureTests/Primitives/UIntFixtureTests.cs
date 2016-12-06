using SimpleFixture.Conventions;
using Xunit;

namespace SimpleFixture.Tests.FixtureTests.Primitives
{
    public class UIntFixtureTests
    {
        #region Generate Tests
        [Fact]
        public void Fixture_GenerateUInt_ReturnsPopulatedValue()
        {
            var fixture = new Fixture();

            var value = fixture.Generate<uint>();

            Assert.True(value != 0);
        }

        [Fact]
        public void Fixture_GenerateNullableUInt_ReturnsPopulatedValue()
        {
            var fixture = new Fixture();

            var nullable = fixture.Generate<uint?>();

            Assert.True(nullable.HasValue);
        }

        [Fact]
        public void Fixture_GenerateUIntArray_ReturnsPopulateValue()
        {
            var fixture = new Fixture();

            var array = fixture.Generate<uint[]>();

            Assert.NotNull(array);
            Assert.True(array.Length > 0);
        }
        #endregion

        #region Locate Tests
        [Fact]
        public void Fixture_LocateUInt_ReturnLocateValue()
        {
            var fixture = new Fixture();

            var value = fixture.Locate<uint>();

            Assert.Equal(UIntConvention.LocateValue, value);
        }

        [Fact]
        public void Fixture_LocateNullableUInt_ReturnsLocateValue()
        {
            var fixture = new Fixture();

            var nullable = fixture.Locate<uint?>();

            Assert.True(nullable.HasValue);
            Assert.Equal(UIntConvention.LocateValue, nullable.Value);
        }

        [Fact]
        public void Fixture_LocateUIntArray_ReturnsEmptyArray()
        {
            var fixture = new Fixture();

            var array = fixture.Locate<uint[]>();

            Assert.NotNull(array);
            Assert.True(array.Length == 0);
        }
        #endregion

        #region Min/Max Tests

        [Fact]
        public void Fixture_GenerateUIntWithMin_ReturnsValueGreaterThanMin()
        {
            var fixture = new Fixture();

            for (var i = 0; i < 100; i++)
            {
                var min = (uint)(1000 * i);

                var value = fixture.Generate<uint>(constraints: new { min });

                Assert.True(value >= min);
            }
        }

        [Fact]
        public void Fixture_GenerateUIntWithMax_ReturnsValueLessThanMax()
        {
            var fixture = new Fixture();

            for (var i = 0; i < 100; i++)
            {
                var max = (uint)(1000 * i);

                var value = fixture.Generate<uint>(constraints: new { max });

                Assert.True(value <= max);
            }
        }

        [Fact]
        public void Fixture_GenerateUIntWithMinMax_ReturnsValueInRange()
        {
            var fixture = new Fixture();

            for (var i = 0; i < 100; i++)
            {
                var min = (uint)(1000 * i);
                var max = (uint)(min + 10);

                var value = fixture.Generate<uint>(constraints: new { min, max });

                Assert.True(value >= min);
                Assert.True(value <= max);
            }
        }
        #endregion
    }
}
