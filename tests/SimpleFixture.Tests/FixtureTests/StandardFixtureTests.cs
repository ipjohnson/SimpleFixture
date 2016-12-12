using System;
using System.Collections;
using SimpleFixture.Tests.Classes;
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

        [Fact]
        public void Fixture_Null_Reference_Test()
        {
            var fixture = new Fixture();

            Assert.Throws<ArgumentNullException>(() => fixture.Generate(null));
            Assert.Throws<ArgumentNullException>(() => fixture.Generate((Type)null));
            Assert.Throws<ArgumentNullException>(() => fixture.Locate((Type)null));
            Assert.Throws<ArgumentNullException>(() => fixture.Populate(null));
            Assert.Throws<ArgumentNullException>(() => fixture.Return((Func<DataRequest, ISomeInterface>) null));
            Assert.Throws<ArgumentNullException>(() => fixture.ReturnIEnumerable((ISomeInterface[])null));
            Assert.Throws<ArgumentNullException>(() => fixture.Add((IConvention) null));
            Assert.Throws<ArgumentNullException>(() => fixture.Add((IFixtureCustomization)null));
        }

        [Fact]
        public void Fixture_IEnumerable()
        {
            var fixture = new Fixture();

            foreach (var variable in (IEnumerable) fixture)
            {
                
            }
        }
    }
}
