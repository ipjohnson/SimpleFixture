using System;
using System.Collections.Generic;
using System.Text;
using SimpleFixture.Conventions;
using Xunit;

namespace SimpleFixture.Tests.FixtureTests.Primitives
{
    public class DateTimeOffsetTests
    {
        #region Generate Tests
        [Fact]
        public void Fixture_GenerateDateTimeOffset_ReturnsPopulatedValue()
        {
            var fixture = new Fixture();

            var value = fixture.Generate<DateTimeOffset>();
        }

        [Fact]
        public void Fixture_GenerateNullableDateTime_ReturnsPopulatedValue()
        {
            var fixture = new Fixture();

            var nullable = fixture.Generate<DateTimeOffset?>();

            Assert.True(nullable.HasValue);
        }

        [Fact]
        public void Fixture_GenerateDateTimeArray_ReturnsPopulateValue()
        {
            var fixture = new Fixture();

            var array = fixture.Generate<DateTimeOffset[]>();

            Assert.NotNull(array);
            Assert.True(array.Length > 0);
        }
        #endregion

        #region Locate Tests
        [Fact]
        public void Fixture_LocateDateTime_ReturnLocateValue()
        {
            var fixture = new Fixture();

            var value = fixture.Locate<DateTimeOffset>();

            Assert.Equal(DateTimeConvention.LocateValue, value);
        }

        [Fact]
        public void Fixture_LocateNullableDateTime_ReturnsLocateValue()
        {
            var fixture = new Fixture();

            var nullable = fixture.Locate<DateTimeOffset?>();

            Assert.True(nullable.HasValue);
            Assert.Equal(DateTimeConvention.LocateValue, nullable.Value);
        }

        [Fact]
        public void Fixture_LocateDateTimeArray_ReturnsEmptyArray()
        {
            var fixture = new Fixture();

            var array = fixture.Locate<DateTimeOffset[]>();

            Assert.NotNull(array);
            Assert.True(array.Length == 0);
        }

        #endregion

        #region Min/Max Tests

        [Fact]
        public void Fixture_GenerateDateTimeWithMin_ReturnsValueGreaterThanMin()
        {
            var fixture = new Fixture();

            for (var i = 0; i < 100; i++)
            {
                DateTimeOffset min = new DateTime(2050, 1, 1);

                var value = fixture.Generate<DateTimeOffset>(constraints: new { min });

                Assert.True(value >= min);
            }
        }

        [Fact]
        public void Fixture_GenerateDateTimeWithMax_ReturnsValueLessThanMax()
        {
            var fixture = new Fixture();

            for (var i = 0; i < 100; i++)
            {
                DateTimeOffset max = new DateTime(2050, 1, 1);

                var value = fixture.Generate<DateTimeOffset>(constraints: new { max });

                Assert.True(value <= max);
            }
        }

        [Fact]
        public void Fixture_GenerateDateTimeWithMinMax_ReturnsValueInRange()
        {
            var fixture = new Fixture();

            for (var i = 0; i < 100; i++)
            {
                DateTimeOffset min = new DateTime(2050, 1, 1);
                DateTimeOffset max = new DateTime(2052, 1, 1);

                var value = fixture.Generate<DateTimeOffset>(constraints: new { min, max });

                Assert.True(value >= min);
                Assert.True(value <= max);
            }
        }

        [Fact]
        public void Fixture_GenerateDateTime_Min_Greater_Than_Max()
        {
            var fixture = new Fixture();

            DateTimeOffset currentDate = DateTime.Today;

            var value = fixture.Generate<DateTimeOffset>(constraints: new { min = currentDate.AddDays(1), max = currentDate });

            Assert.Equal(currentDate, value);

        }
        #endregion
    }
}
