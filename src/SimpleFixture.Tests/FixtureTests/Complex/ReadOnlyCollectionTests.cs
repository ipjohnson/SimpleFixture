using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SimpleFixture.Tests.FixtureTests.Complex
{
    public class ReadOnlyCollectionTests
    {
        #region Generate Tests
        [Fact]
        public void Fixture_GenerateIReadOnlyCollectionInt_ReturnsPopulatedValue()
        {
            var fixture = new Fixture();

            var enumerable = fixture.Generate<IReadOnlyCollection<int>>();

            Assert.NotNull(enumerable);
            Assert.True(enumerable.Any());
        }

        [Fact]
        public void Fixture_GenerateIReadOnlyListInt_ReturnsPopulatedValue()
        {
            var fixture = new Fixture();

            var enumerable = fixture.Generate<IReadOnlyList<int>>();

            Assert.NotNull(enumerable);
            Assert.True(enumerable.Any());
        }
        
        [Fact]
        public void Fixture_GenerateReadOnlyCollectionInt_ReturnsPopulatedValue()
        {
            var fixture = new Fixture();

            var list = fixture.Generate<ReadOnlyCollection<int>>();

            Assert.NotNull(list);
            Assert.True(list.Any());
        }
        #endregion

        #region Locate Tests
        [Fact]
        public void Fixture_LocateIReadOnlyCollectionInt_ReturnsPopulatedValue()
        {
            var fixture = new Fixture();

            var enumerable = fixture.Locate<IReadOnlyCollection<int>>();

            Assert.NotNull(enumerable);
            Assert.False(enumerable.Any());
        }

        [Fact]
        public void Fixture_LocateIReadOnlyListInt_ReturnsPopulatedValue()
        {
            var fixture = new Fixture();

            var enumerable = fixture.Locate<IReadOnlyList<int>>();

            Assert.NotNull(enumerable);
            Assert.False(enumerable.Any());
        }

        [Fact]
        public void Fixture_LocateReadOnlyCollectionInt_ReturnsPopulatedValue()
        {
            var fixture = new Fixture();

            var list = fixture.Locate<ReadOnlyCollection<int>>();

            Assert.NotNull(list);
            Assert.False(list.Any());
        }
        #endregion
    }
}
