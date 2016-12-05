using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleFixture.Conventions;
using Xunit;

namespace SimpleFixture.Tests.FixtureTests.Primitives
{
    public class LongFixtureTests
    {
        #region Generate Tests
        [Fact]
        public void Fixture_GenerateLong_ReturnsPopulatedValue()
        {
            var fixture = new Fixture();

            var value = fixture.Generate<long>();

            Assert.True(value != 0);
        }

        [Fact]
        public void Fixture_GenerateNullableLong_ReturnsPopulatedValue()
        {
            var fixture = new Fixture();

            var nullable = fixture.Generate<long?>();

            Assert.True(nullable.HasValue);
        }

        [Fact]
        public void Fixture_GenerateLongArray_ReturnsPopulateValue()
        {
            var fixture = new Fixture();

            var array = fixture.Generate<long[]>();

            Assert.NotNull(array);
            Assert.True(array.Length > 0);
        }
        #endregion

        #region Locate Tests
        [Fact]
        public void Fixture_LocateLong_ReturnLocateValue()
        {
            var fixture = new Fixture();

            var value = fixture.Locate<long>();

            Assert.Equal(LongConvention.LocateValue, value);
        }

        [Fact]
        public void Fixture_LocateNullableLong_ReturnsLocateValue()
        {
            var fixture = new Fixture();

            var nullable = fixture.Locate<long?>();

            Assert.True(nullable.HasValue);
            Assert.Equal(LongConvention.LocateValue, nullable.Value);
        }

        [Fact]
        public void Fixture_LocateLongArray_ReturnsEmptyArray()
        {
            var fixture = new Fixture();

            var array = fixture.Locate<long[]>();

            Assert.NotNull(array);
            Assert.True(array.Length == 0);
        }
        #endregion

        #region Min/Max Tests

        [Fact]
        public void Fixture_GenerateLongWithMin_ReturnsValueGreaterThanMin()
        {
            var fixture = new Fixture();

            for (int i = 0; i < 100; i++)
            {
                long min = 1000 * i;

                var value = fixture.Generate<long>(constraints: new { min });

                Assert.True(value >= min);
            }
        }

        [Fact]
        public void Fixture_GenerateLongWithMax_ReturnsValueLessThanMax()
        {
            var fixture = new Fixture();

            for (int i = 0; i < 100; i++)
            {
                long max = 1000 * i;

                var value = fixture.Generate<long>(constraints: new { max });

                Assert.True(value <= max);
            }
        }

        [Fact]
        public void Fixture_GenerateLongWithMinMax_ReturnsValueInRange()
        {
            var fixture = new Fixture();

            for (int i = 0; i < 100; i++)
            {
                long min = 1000 * i;
                long max = min + 10;

                var value = fixture.Generate<long>(constraints: new { min, max });

                Assert.True(value >= min);
                Assert.True(value <= max);
            }
        }
        #endregion
    }
}
