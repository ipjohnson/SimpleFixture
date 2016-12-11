using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleFixture.Conventions;
using Xunit;

namespace SimpleFixture.Tests.FixtureTests.Primitives
{
    public class TypeFixtureTests
    {
        [Fact]
        public void Fixture_Locate_Type()
        {
            var fixture = new Fixture();

            var type = fixture.Locate<Type>();

            Assert.NotNull(type);
            Assert.Equal(TypeConvention.LocateType, type);
        }

        [Fact]
        public void Fixture_Generate_Type()
        {
            var fixture = new Fixture();

            var type = fixture.Generate<Type>();

            Assert.NotNull(type);
        }
    }
}
