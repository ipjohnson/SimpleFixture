using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleFixture.Conventions;
using Xunit;

namespace SimpleFixture.Tests.FixtureTests.Primitives
{
    public class DateTimeFixtureTests
    {
        #region Generate Tests
        [Fact]
        public void Fixture_GenerateDateTime_ReturnsPopulatedValue()
        {
            var fixture = new Fixture();

            var value = fixture.Generate<DateTime>();
        }

        [Fact]
        public void Fixture_GenerateNullableDateTime_ReturnsPopulatedValue()
        {
            var fixture = new Fixture();

            var nullable = fixture.Generate<DateTime?>();

            Assert.True(nullable.HasValue);
        }

        [Fact]
        public void Fixture_GenerateDateTimeArray_ReturnsPopulateValue()
        {
            var fixture = new Fixture();

            var array = fixture.Generate<DateTime[]>();

            Assert.NotNull(array);
            Assert.True(array.Length > 0);
        }
        #endregion

        #region Locate Tests
        [Fact]
        public void Fixture_LocateDateTime_ReturnLocateValue()
        {
            var fixture = new Fixture();

            var value = fixture.Locate<DateTime>();

            Assert.Equal(DateTimeConvention.LocateValue, value);
        }

        [Fact]
        public void Fixture_LocateNullableDateTime_ReturnsLocateValue()
        {
            var fixture = new Fixture();

            var nullable = fixture.Locate<DateTime?>();

            Assert.True(nullable.HasValue);
            Assert.Equal(DateTimeConvention.LocateValue, nullable.Value);
        }

        [Fact]
        public void Fixture_LocateDateTimeArray_ReturnsEmptyArray()
        {
            var fixture = new Fixture();

            var array = fixture.Locate<DateTime[]>();

            Assert.NotNull(array);
            Assert.True(array.Length == 0);
        }

        #endregion

        #region Min/Max Tests

        [Fact]
        public void Fixture_GenerateDateTimeWithMin_ReturnsValueGreaterThanMin()
        {
            var fixture = new Fixture();

            for (int i = 0; i < 100; i++)
            {
                DateTime min = new DateTime(2050, 1, 1);

                var value = fixture.Generate<DateTime>(constraints: new { min });

                Assert.True(value >= min);
            }
        }

        [Fact]
        public void Fixture_GenerateDateTimeWithMax_ReturnsValueLessThanMax()
        {
            var fixture = new Fixture();

            for (int i = 0; i < 100; i++)
            {
                DateTime max = new DateTime(2050, 1, 1);

                var value = fixture.Generate<DateTime>(constraints: new { max });

                Assert.True(value <= max);
            }
        }

        [Fact]
        public void Fixture_GenerateDateTimeWithMinMax_ReturnsValueInRange()
        {
            var fixture = new Fixture();

            for (int i = 0; i < 100; i++)
            {
                DateTime min = new DateTime(2050, 1, 1);
                DateTime max = new DateTime(2052, 1, 1);

                var value = fixture.Generate<DateTime>(constraints: new { min, max });

                Assert.True(value >= min);
                Assert.True(value <= max);
            }
        }
        #endregion
    }
}
