using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleFixture.Conventions;
using SimpleFixture.Tests.Classes;
using Xunit;

namespace SimpleFixture.Tests.FixtureTests.Conventions
{
    // ReSharper disable once InconsistentNaming
    public class IEqualityComparerConventionTests
    {
        [Fact]
        public void IEqualityComparerConvention_Priority()
        {
            var instance = new IEqualityComparerConvention();

            Assert.Equal(ConventionPriority.Last, instance.Priority);
        }

        [Fact]
        public void IEqualityComparerConvention_GenerateType()
        {
            var instance = new IEqualityComparerConvention();
            var fixture = new Fixture();

            var request = new DataRequest(null, fixture, typeof(ISomeInterface), DependencyType.Unknown, null, true, null, null);

            Assert.Equal(Convention.NoValue, instance.GenerateData(request));
        }
    }
}
