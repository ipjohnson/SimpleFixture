using System;
using SimpleFixture.Tests.Classes;
using Xunit;

namespace SimpleFixture.Tests.FixtureTests
{
    public class ReturnTests
    {
        [Fact]
        public void Fixture_ReturnInt_GenerateCorrectValue()
        {
            var fixture = new Fixture();

            fixture.Return(15);

            var intValue = fixture.Generate<int>();

            Assert.Equal(15, fixture.Generate<int>());
        }

        [Fact]
        public void Fixture_ReturnInt_LocateCorrectValue()
        {
            var fixture = new Fixture();

            fixture.Return(15);

            Assert.Equal(15, fixture.Locate<int>());
        }

        [Fact]
        public void Fixture_ReturnForType_GenerateCorrectValue()
        {
            var fixture = new Fixture();

            fixture.Return(15).For<PropertiesClass>();

            var propClass = fixture.Generate<PropertiesClass>();

            Assert.NotNull(propClass);
            Assert.Equal(15, propClass.IntValue1);
            Assert.Equal(15, propClass.IntValue2);

            Assert.NotEqual(15, fixture.Generate<int>());
        }

        [Fact]
        public void Fixture_ReturnWhenNamed_GenerateCorrectValue()
        {
            var fixture = new Fixture();

            fixture.Return(15).WhenNamed(name => name.EndsWith("1"));

            var propClass = fixture.Generate<PropertiesClass>();

            Assert.NotNull(propClass);
            Assert.Equal(15, propClass.IntValue1);
            Assert.NotEqual(15, propClass.IntValue2);
            Assert.NotEqual(15, fixture.Generate<int>());
        }

        [Fact]
        public void Fixture_ReturnForTypeWhenNamed_GenerateCorrectValue()
        {
            var fixture = new Fixture();

            fixture.Return(15).For<PropertiesClass>().WhenNamed(name => name.EndsWith("1"));

            var propClass = fixture.Generate<PropertiesClass>();

            Assert.NotNull(propClass);
            Assert.Equal(15, propClass.IntValue1);
            Assert.NotEqual(15, propClass.IntValue2);

            var propClass2 = fixture.Generate<PropertiesClass2>();

            Assert.NotNull(propClass2);
            Assert.NotEqual(15, propClass2.IntValue1);
            Assert.NotEqual(15, propClass2.IntValue2);

            Assert.NotEqual(15, fixture.Generate<int>());
        }
        [Fact]
        public void Fixture_ReturnWhenMatching_GenerateCorrectValue()
        {
            var fixture = new Fixture();

            fixture.Return(15).WhenMatching(r => r.RequestName.EndsWith("1") && r.RequestedType == typeof(int));

            var propClass = fixture.Generate<PropertiesClass>();

            Assert.NotNull(propClass);
            Assert.Equal(15, propClass.IntValue1);
            Assert.NotEqual(15, propClass.IntValue2);

            var propClass2 = fixture.Generate<PropertiesClass2>();

            Assert.NotNull(propClass2);
            Assert.Equal(15, propClass2.IntValue1);
            Assert.NotEqual(15, propClass2.IntValue2);

            Assert.NotEqual(15, fixture.Generate<int>());
        }

        [Fact]
        public void Fixture_ReturnForOutOfOrder_GenerateCorrectValue()
        {
            var fixture = new Fixture();

            fixture.Return(10).For<PropertiesClass>();
            fixture.Return(5);

            var propClass = fixture.Generate<PropertiesClass>();

            Assert.NotNull(propClass);
            Assert.Equal(10,propClass.IntValue1);
        }

        [Fact]
        public void Fixture_Return_Null_Throws_Exception()
        {
            var fixture = new Fixture();

            Assert.Throws<ArgumentNullException>(() => fixture.Return((PropertiesClass[])null));
        }

        [Fact]
        public void Fixture_Return_Func_Null_Throws_Exception()
        {
            var fixture = new Fixture();

            Assert.Throws<ArgumentNullException>(() => fixture.Return((Func<PropertiesClass>)null));
        }
    }
}
