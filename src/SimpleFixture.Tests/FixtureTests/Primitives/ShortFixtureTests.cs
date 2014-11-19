using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleFixture.Conventions;
using Xunit;

namespace SimpleFixture.Tests.FixtureTests.Primitives
{
    public class ShortFixtureTests
    {
        #region Generate Tests
        [Fact]
        public void Fixture_GenerateShort_ReturnsPopulatedValue()
        {
            var fixture = new Fixture();

            var value = fixture.Generate<short>();
        }

        [Fact]
        public void Fixture_GenerateNullableShort_ReturnsPopulatedValue()
        {
            var fixture = new Fixture();

            var nullable = fixture.Generate<short?>();

            Assert.True(nullable.HasValue);
        }

        [Fact]
        public void Fixture_GenerateShortArray_ReturnsPopulateValue()
        {
            var fixture = new Fixture();

            var array = fixture.Generate<short[]>();

            Assert.NotNull(array);
            Assert.True(array.Length > 0);
        }
        #endregion

        #region Locate Tests
        [Fact]
        public void Fixture_LocateShort_ReturnLocateValue()
        {
            var fixture = new Fixture();

            var value = fixture.Locate<short>();

            Assert.Equal(ShortConvention.LocateValue, value);
        }

        [Fact]
        public void Fixture_LocateNullableShort_ReturnsLocateValue()
        {
            var fixture = new Fixture();

            var nullable = fixture.Locate<short?>();

            Assert.True(nullable.HasValue);
            Assert.Equal(ShortConvention.LocateValue, nullable.Value);
        }

        [Fact]
        public void Fixture_LocateShortArray_ReturnsEmptyArray()
        {
            var fixture = new Fixture();

            var array = fixture.Locate<short[]>();

            Assert.NotNull(array);
            Assert.True(array.Length == 0);
        }
        #endregion

        #region Min/Max Tests

        [Fact]
        public void Fixture_GenerateShortWithMin_ReturnsValueGreaterThanMin()
        {
            var fixture = new Fixture();

            for (int i = 0; i < 100; i++)
            {
                short min = (short)i;

                var value = fixture.Generate<short>(constraints: new { min });

                Assert.True(value >= min);
            }
        }

        [Fact]
        public void Fixture_GenerateShortWithMax_ReturnsValueLessThanMax()
        {
            var fixture = new Fixture();

            for (int i = 0; i < 100; i++)
            {
                short max = (short)(i + 10);

                var value = fixture.Generate<short>(constraints: new { max });

                Assert.True(value <= max);
            }
        }

        [Fact]
        public void Fixture_GenerateShortWithMinMax_ReturnsValueInRange()
        {
            var fixture = new Fixture();

            for (int i = 0; i < 100; i++)
            {
                short min = (short)i;
                short max = (short)(min + 10);

                var value = fixture.Generate<short>(constraints: new { min, max });

                Assert.True(value >= min);
                Assert.True(value <= max);
            }
        }
        #endregion
    }
}
