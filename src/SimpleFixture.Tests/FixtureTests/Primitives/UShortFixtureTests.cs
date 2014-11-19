using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleFixture.Conventions;
using Xunit;

namespace SimpleFixture.Tests.FixtureTests.Primitives
{
    public class UShortFixtureTests
    {
        #region Generate Tests
        [Fact]
        public void Fixture_GenerateUShort_ReturnsPopulatedValue()
        {
            var fixture = new Fixture();

            var value = fixture.Generate<ushort>();
        }

        [Fact]
        public void Fixture_GenerateNullableUShort_ReturnsPopulatedValue()
        {
            var fixture = new Fixture();

            var nullable = fixture.Generate<ushort?>();

            Assert.True(nullable.HasValue);
        }

        [Fact]
        public void Fixture_GenerateUShortArray_ReturnsPopulateValue()
        {
            var fixture = new Fixture();

            var array = fixture.Generate<ushort[]>();

            Assert.NotNull(array);
            Assert.True(array.Length > 0);
        }
        #endregion

        #region Locate Tests
        [Fact]
        public void Fixture_LocateUShort_ReturnLocateValue()
        {
            var fixture = new Fixture();

            var value = fixture.Locate<ushort>();

            Assert.Equal(UShortConvention.LocateValue, value);
        }

        [Fact]
        public void Fixture_LocateNullableUShort_ReturnsLocateValue()
        {
            var fixture = new Fixture();

            var nullable = fixture.Locate<ushort?>();

            Assert.True(nullable.HasValue);
            Assert.Equal(UShortConvention.LocateValue, nullable.Value);
        }

        [Fact]
        public void Fixture_LocateUShortArray_ReturnsEmptyArray()
        {
            var fixture = new Fixture();

            var array = fixture.Locate<ushort[]>();

            Assert.NotNull(array);
            Assert.True(array.Length == 0);
        }
        #endregion

        #region Min/Max Tests

        [Fact]
        public void Fixture_GenerateUShortWithMin_ReturnsValueGreaterThanMin()
        {
            var fixture = new Fixture();

            for (int i = 0; i < 100; i++)
            {
                ushort min = (ushort)i;

                var value = fixture.Generate<ushort>(constraints: new { min });

                Assert.True(value >= min);
            }
        }

        [Fact]
        public void Fixture_GenerateUShortWithMax_ReturnsValueLessThanMax()
        {
            var fixture = new Fixture();

            for (int i = 0; i < 100; i++)
            {
                ushort max = (ushort)(i + 10);

                var value = fixture.Generate<ushort>(constraints: new { max });

                Assert.True(value <= max);
            }
        }

        [Fact]
        public void Fixture_GenerateUShortWithMinMax_ReturnsValueInRange()
        {
            var fixture = new Fixture();

            for (int i = 0; i < 100; i++)
            {
                ushort min = (ushort)i;
                ushort max = (ushort)(min + 10);

                var value = fixture.Generate<ushort>(constraints: new { min, max });

                Assert.True(value >= min);
                Assert.True(value <= max);
            }
        }
        #endregion
    }
}
