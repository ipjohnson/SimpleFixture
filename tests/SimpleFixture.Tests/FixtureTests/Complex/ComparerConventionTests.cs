using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SimpleFixture.Tests.FixtureTests.Complex
{
    public class ComparerConventionTests
    {
        [Fact]
        public void Fixture_GenerateIComparerInt_ReturnsWorkingComparer()
        {
            var fixture = new Fixture();

            var comparer = fixture.Generate<IComparer<int>>();

            Assert.NotNull(comparer);
            Assert.True(comparer.Compare(1, 2) < 0);
        }

        [Fact]
        public void Fixture_GenerateIEqualityComparerInt_ReturnsWorkingComparer()
        {
            var fixture = new Fixture();

            var comparer = fixture.Locate<IEqualityComparer<int>>();

            Assert.NotNull(comparer);
            Assert.False(comparer.Equals(1, 2));
        }

        [Fact]
        public void Fixture_LocateIComparerInt_ReturnsWorkingComparer()
        {
            var fixture = new Fixture();

            var comparer = fixture.Locate<IComparer<int>>();

            Assert.NotNull(comparer);
            Assert.True(comparer.Compare(1,2) < 0);
        }
        
        [Fact]
        public void Fixture_LocateIEqualityComparerInt_ReturnsWorkingComparer()
        {
            var fixture = new Fixture();

            var comparer = fixture.Locate<IEqualityComparer<int>>();

            Assert.NotNull(comparer);
            Assert.False(comparer.Equals(1, 2));
        }
    }
}
