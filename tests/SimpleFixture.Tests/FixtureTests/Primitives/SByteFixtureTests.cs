using SimpleFixture.Conventions;
using Xunit;

namespace SimpleFixture.Tests.FixtureTests.Primitives
{
    public class SByteFixtureTests
    {
        #region Generate Tests
        [Fact]
        public void Fixture_GenerateSByte_ReturnsPopulatedValue()
        {
            var fixture = new Fixture();

            var value = fixture.Generate<sbyte>();
        }

        [Fact]
        public void Fixture_GenerateNullableSByte_ReturnsPopulatedValue()
        {
            var fixture = new Fixture();

            var nullable = fixture.Generate<sbyte?>();

            Assert.True(nullable.HasValue);
        }

        [Fact]
        public void Fixture_GenerateSByteArray_ReturnsPopulateValue()
        {
            var fixture = new Fixture();

            var array = fixture.Generate<sbyte[]>();

            Assert.NotNull(array);
            Assert.True(array.Length > 0);
        }
        #endregion

        #region Locate Tests
        [Fact]
        public void Fixture_LocateSByte_ReturnLocateValue()
        {
            var fixture = new Fixture();

            var value = fixture.Locate<sbyte>();

            Assert.Equal(SByteConvention.LocateValue, value);
        }

        [Fact]
        public void Fixture_LocateNullableSByte_ReturnsLocateValue()
        {
            var fixture = new Fixture();

            var nullable = fixture.Locate<sbyte?>();

            Assert.True(nullable.HasValue);
            Assert.Equal(SByteConvention.LocateValue, nullable.Value);
        }

        [Fact]
        public void Fixture_LocateSByteArray_ReturnsEmptyArray()
        {
            var fixture = new Fixture();

            var array = fixture.Locate<sbyte[]>();

            Assert.NotNull(array);
            Assert.True(array.Length == 0);
        }
        #endregion

        #region Min/Max Tests

        [Fact]
        public void Fixture_GenerateSByteWithMin_ReturnsValueGreaterThanMin()
        {
            var fixture = new Fixture();

            for (int i = 0; i < 100; i++)
            {
                sbyte min = (sbyte)i;

                var value = fixture.Generate<sbyte>(constraints: new { min });

                Assert.True(value >= min);
            }
        }

        [Fact]
        public void Fixture_GenerateSByteWithMax_ReturnsValueLessThanMax()
        {
            var fixture = new Fixture();

            for (int i = 0; i < 100; i++)
            {
                sbyte max = (sbyte)(i + 10);

                var value = fixture.Generate<sbyte>(constraints: new { max });

                Assert.True(value <= max);
            }
        }

        [Fact]
        public void Fixture_GenerateSByteWithMinMax_ReturnsValueInRange()
        {
            var fixture = new Fixture();

            for (int i = 0; i < 100; i++)
            {
                sbyte min = (sbyte)i;
                sbyte max = (sbyte)(min + 10);

                var value = fixture.Generate<sbyte>(constraints: new { min, max });

                Assert.True(value >= min);
                Assert.True(value <= max);
            }
        }
        #endregion
    }
}
