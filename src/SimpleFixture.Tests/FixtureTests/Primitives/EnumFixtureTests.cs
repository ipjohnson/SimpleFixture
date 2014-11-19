using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleFixture.Conventions;
using Xunit;

namespace SimpleFixture.Tests.FixtureTests.Primitives
{
    public class EnumFixtureTests
    {
        public enum TestEnum
        {
            Value1,
            Value2,
            Value3
        }

        #region Generate Tests
        [Fact]
        public void Fixture_GenerateEnum_ReturnsPopulatedValue()
        {
            var fixture = new Fixture();

            var value = fixture.Generate<TestEnum>();
        }

        [Fact]
        public void Fixture_GenerateNullableEnum_ReturnsPopulatedValue()
        {
            var fixture = new Fixture();

            var nullable = fixture.Generate<TestEnum?>();

            Assert.True(nullable.HasValue);
        }

        [Fact]
        public void Fixture_GenerateEnumArray_ReturnsPopulateValue()
        {
            var fixture = new Fixture();

            var array = fixture.Generate<TestEnum[]>();

            Assert.NotNull(array);
            Assert.True(array.Length > 0);
        }
        #endregion

        #region Locate Tests
        [Fact]
        public void Fixture_LocateEnum_ReturnLocateValue()
        {
            var fixture = new Fixture();

            var value = fixture.Locate<TestEnum>();
        }

        [Fact]
        public void Fixture_LocateNullableEnum_ReturnsLocateValue()
        {
            var fixture = new Fixture();

            var nullable = fixture.Locate<TestEnum?>();

            Assert.True(nullable.HasValue);
        }

        [Fact]
        public void Fixture_LocateBoolArray_ReturnsEmptyArray()
        {
            var fixture = new Fixture();

            var array = fixture.Locate<TestEnum[]>();

            Assert.NotNull(array);
            Assert.True(array.Length == 0);
        }
        #endregion
    }
}
