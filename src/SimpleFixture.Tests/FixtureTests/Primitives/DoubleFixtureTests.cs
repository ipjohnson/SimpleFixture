using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleFixture.Conventions;
using Xunit;

namespace SimpleFixture.Tests.FixtureTests.Primitives
{
    public class DoubleFixtureTests
    {
        #region Generate Tests
        [Fact]
        public void Fixture_GenerateDouble_ReturnsPopulatedValue()
        {
            var fixture = new Fixture();

            var value = fixture.Generate<double>();
        }

        [Fact]
        public void Fixture_GenerateNullableDouble_ReturnsPopulatedValue()
        {
            var fixture = new Fixture();

            var nullable = fixture.Generate<double?>();

            Assert.True(nullable.HasValue);
        }

        [Fact]
        public void Fixture_GenerateDoubleArray_ReturnsPopulateValue()
        {
            var fixture = new Fixture();

            var array = fixture.Generate<double[]>();

            Assert.NotNull(array);
            Assert.True(array.Length > 0);
        }
        #endregion

        #region Locate Tests
        [Fact]
        public void Fixture_LocateDouble_ReturnLocateValue()
        {
            var fixture = new Fixture();

            var value = fixture.Locate<double>();

            Assert.Equal(DoubleConvention.LocateValue, value);
        }

        [Fact]
        public void Fixture_LocateNullableDouble_ReturnsLocateValue()
        {
            var fixture = new Fixture();

            var nullable = fixture.Locate<double?>();

            Assert.True(nullable.HasValue);
            Assert.Equal(DoubleConvention.LocateValue, nullable.Value);
        }

        [Fact]
        public void Fixture_LocateDoubleArray_ReturnsEmptyArray()
        {
            var fixture = new Fixture();

            var array = fixture.Locate<double[]>();

            Assert.NotNull(array);
            Assert.True(array.Length == 0);
        }
        #endregion

        #region Min/Max Tests

        [Fact]
        public void Fixture_GenerateDoubleWithMin_ReturnsValueGreaterThanMin()
        {
            var fixture = new Fixture();

            for (int i = 0; i < 100; i++)
            {
                double min = i;

                var value = fixture.Generate<double>(constraints: new { min });

                Assert.True(value >= min);
            }
        }

        [Fact]
        public void Fixture_GenerateDoubleWithMax_ReturnsValueLessThanMax()
        {
            var fixture = new Fixture();

            for (int i = 0; i < 100; i++)
            {
                double max = i + 10;

                var value = fixture.Generate<double>(constraints: new { max });

                Assert.True(value <= max);
            }
        }

        [Fact]
        public void Fixture_GenerateDoubleWithMinMax_ReturnsValueInRange()
        {
            var fixture = new Fixture();

            for (int i = 0; i < 100; i++)
            {
                double min = i;
                double max = min + 10;

                var value = fixture.Generate<double>(constraints: new { min, max });

                Assert.True(value >= min);
                Assert.True(value <= max);
            }
        }
        #endregion
    }
}
