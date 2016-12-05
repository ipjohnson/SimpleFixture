using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleFixture.Conventions;
using Xunit;

namespace SimpleFixture.Tests.FixtureTests.Primitives
{
    public class DecimalFixtureTests
    {        
        #region Generate Tests
        [Fact]
        public void Fixture_GenerateDecimal_ReturnsPopulatedValue()
        {
            var fixture = new Fixture();

            var value = fixture.Generate<decimal>();
        }

        [Fact]
        public void Fixture_GenerateNullableDecimal_ReturnsPopulatedValue()
        {
            var fixture = new Fixture();

            var nullable = fixture.Generate<decimal?>();

            Assert.True(nullable.HasValue);
        }

        [Fact]
        public void Fixture_GenerateDecimalArray_ReturnsPopulateValue()
        {
            var fixture = new Fixture();

            var array = fixture.Generate<decimal[]>();

            Assert.NotNull(array);
            Assert.True(array.Length > 0);
        }
        #endregion

        #region Locate Tests
        [Fact]
        public void Fixture_LocateDecimal_ReturnLocateValue()
        {
            var fixture = new Fixture();

            var value = fixture.Locate<decimal>();

            Assert.Equal(DecimalConvention.LocateValue, value);
        }

        [Fact]
        public void Fixture_LocateNullableDecimal_ReturnsLocateValue()
        {
            var fixture = new Fixture();

            var nullable = fixture.Locate<decimal?>();

            Assert.True(nullable.HasValue);
            Assert.Equal(DecimalConvention.LocateValue, nullable.Value);
        }

        [Fact]
        public void Fixture_LocateDecimalArray_ReturnsEmptyArray()
        {
            var fixture = new Fixture();

            var array = fixture.Locate<decimal[]>();

            Assert.NotNull(array);
            Assert.True(array.Length == 0);
        }
        #endregion

        #region Min/Max Tests

        [Fact]
        public void Fixture_GenerateDecimalWithMin_ReturnsValueGreaterThanMin()
        {
            var fixture = new Fixture();

            for (int i = 0; i < 100; i++)
            {
                decimal min = i;

                var value = fixture.Generate<decimal>(constraints: new { min });

                Assert.True(value >= min);
            }
        }

        [Fact]
        public void Fixture_GenerateDecimalWithMax_ReturnsValueLessThanMax()
        {
            var fixture = new Fixture();

            for (int i = 0; i < 100; i++)
            {
                decimal max = i + 10;

                var value = fixture.Generate<decimal>(constraints: new { max });

                Assert.True(value <= max);
            }
        }

        [Fact]
        public void Fixture_GenerateDecimalWithMinMax_ReturnsValueInRange()
        {
            var fixture = new Fixture();

            for (int i = 0; i < 100; i++)
            {
                decimal min = i;
                decimal max = min + 10;

                var value = fixture.Generate<decimal>(constraints: new { min, max });

                Assert.True(value >= min);
                Assert.True(value <= max);
            }
        }
        #endregion
    }
}
