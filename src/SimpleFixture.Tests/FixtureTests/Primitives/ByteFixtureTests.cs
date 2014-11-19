using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleFixture.Conventions;
using Xunit;

namespace SimpleFixture.Tests.FixtureTests.Primitives
{
    public class ByteFixtureTests
    {
        #region Generate Tests
        [Fact]
        public void Fixture_GenerateByte_ReturnsPopulatedValue()
        {
            var fixture = new Fixture();

            var value = fixture.Generate<byte>();
        }

        [Fact]
        public void Fixture_GenerateNullableByte_ReturnsPopulatedValue()
        {
            var fixture = new Fixture();

            var nullable = fixture.Generate<byte?>();

            Assert.True(nullable.HasValue);
        }

        [Fact]
        public void Fixture_GenerateByteArray_ReturnsPopulateValue()
        {
            var fixture = new Fixture();

            var array = fixture.Generate<byte[]>();

            Assert.NotNull(array);
            Assert.True(array.Length > 0);
        }
        #endregion

        #region Locate Tests
        [Fact]
        public void Fixture_LocateByte_ReturnLocateValue()
        {
            var fixture = new Fixture();

            var value = fixture.Locate<byte>();

            Assert.Equal(ByteConvention.LocateValue, value);
        }

        [Fact]
        public void Fixture_LocateNullableByte_ReturnsLocateValue()
        {
            var fixture = new Fixture();

            var nullable = fixture.Locate<byte?>();

            Assert.True(nullable.HasValue);
            Assert.Equal(ByteConvention.LocateValue, nullable.Value);
        }

        [Fact]
        public void Fixture_LocateByteArray_ReturnsEmptyArray()
        {
            var fixture = new Fixture();

            var array = fixture.Locate<byte[]>();

            Assert.NotNull(array);
            Assert.True(array.Length == 0);
        }
        #endregion

        #region Min/Max Tests

        [Fact]
        public void Fixture_GenerateByteWithMin_ReturnsValueGreaterThanMin()
        {
            var fixture = new Fixture();

            for (int i = 0; i < 100; i++)
            {
                byte min = (byte)i;

                var value = fixture.Generate<byte>(constraints: new { min });

                Assert.True(value >= min);
            }
        }

        [Fact]
        public void Fixture_GenerateByteWithMax_ReturnsValueLessThanMax()
        {
            var fixture = new Fixture();

            for (int i = 0; i < 100; i++)
            {
                byte max = (byte)(i + 10);

                var value = fixture.Generate<byte>(constraints: new { max });

                Assert.True(value <= max);
            }
        }

        [Fact]
        public void Fixture_GenerateByteWithMinMax_ReturnsValueInRange()
        {
            var fixture = new Fixture();

            for (int i = 0; i < 100; i++)
            {
                byte min = (byte)i;
                byte max = (byte)(min + 10);

                var value = fixture.Generate<byte>(constraints: new { min, max });

                Assert.True(value >= min);
                Assert.True(value <= max);
            }
        }
        #endregion
    }
}
