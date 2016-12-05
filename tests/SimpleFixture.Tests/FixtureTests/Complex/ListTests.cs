using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SimpleFixture.Tests.FixtureTests
{
    public class ListTests
    {
        #region Generate Tests
        [Fact]
        public void Fixture_GenerateIEnumerableInt_ReturnsPopulatedValue()
        {
            var fixture = new Fixture();

            var enumerable = fixture.Generate<IEnumerable<int>>();

            Assert.NotNull(enumerable);
            Assert.True(enumerable.Any());
        }

        [Fact]
        public void Fixture_GenerateICollectionInt_ReturnsPopulatedValue()
        {
            var fixture = new Fixture();

            var collection = fixture.Generate<ICollection<int>>();

            Assert.NotNull(collection);
            Assert.True(collection.Any());
        }

        [Fact]
        public void Fixture_GenerateIListInt_ReturnsPopulatedValue()
        {
            var fixture = new Fixture();

            var list = fixture.Generate<IList<int>>();

            Assert.NotNull(list);
            Assert.True(list.Any());
        }

        [Fact]
        public void Fixture_GenerateListInt_ReturnsPopulatedValue()
        {
            var fixture = new Fixture();

            var list = fixture.Generate<List<int>>();

            Assert.NotNull(list);
            Assert.True(list.Any());
        }
        #endregion

        #region Locate Tests
        [Fact]
        public void Fixture_LocateIEnumerableInt_ReturnsPopulatedValue()
        {
            var fixture = new Fixture();

            var enumerable = fixture.Locate<IEnumerable<int>>();

            Assert.NotNull(enumerable);
            Assert.False(enumerable.Any());
        }

        [Fact]
        public void Fixture_LocateICollectionInt_ReturnsPopulatedValue()
        {
            var fixture = new Fixture();

            var collection = fixture.Locate<ICollection<int>>();

            Assert.NotNull(collection);
            Assert.False(collection.Any());
        }

        [Fact]
        public void Fixture_LocateIListInt_ReturnsPopulatedValue()
        {
            var fixture = new Fixture();

            var list = fixture.Locate<IList<int>>();

            Assert.NotNull(list);
            Assert.False(list.Any());
        }

        [Fact]
        public void Fixture_LocateListInt_ReturnsPopulatedValue()
        {
            var fixture = new Fixture();

            var list = fixture.Locate<List<int>>();

            Assert.NotNull(list);
            Assert.False(list.Any());
        }
        #endregion

    }
}
