using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SimpleFixture.Tests.FixtureTests
{
    public class StandardFixtureTests
    {
        public class PrivateClass
        {
            private PrivateClass()
            {

            }
        }
        
        [Fact]
        public void Fixture_Throws_Exception_When_No_Constructor_Found()
        {
            var fixture = new Fixture();

            Assert.Throws<Exception>(() => fixture.Generate<PrivateClass>());
        }

        public struct MyStruct
        {
            public int IntValue { get; set; }
        }

        [Fact]
        public void Fixture_Creates_Value_Type()
        {
            var fixture = new Fixture();

            var instance = fixture.Generate<MyStruct>();

            Assert.NotNull(instance);
            Assert.NotEqual(0, instance.IntValue);
        }
    }
}
