using System;
using System.Linq;
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
        public void Fixture_Return_Enumerable_Int_LocateCorrectValue()
        {
            var fixture = new Fixture();

            fixture.ReturnIEnumerable(1, 2, 3, 4, 5);

            var instance = fixture.Locate<ImportEnumerableClass>();

            Assert.NotNull(instance);

            var array = instance.IntValues.ToArray();

            Assert.Equal(5, array.Length);
            Assert.Equal(1, array[0]);
            Assert.Equal(2, array[1]);
            Assert.Equal(3, array[2]);
            Assert.Equal(4, array[3]);
            Assert.Equal(5, array[4]);
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
            Assert.Equal(10, propClass.IntValue1);
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

        [Fact]
        public void Fixture_Return_When_String_Null_Throws_Exception()
        {
            var fixture = new Fixture();

            Assert.Throws<ArgumentNullException>(() => fixture.Return(10).WhenNamed((string)null));
        }

        [Fact]
        public void Fixture_Return_When_Func_Null_Throws_Exception()
        {
            var fixture = new Fixture();

            Assert.Throws<ArgumentNullException>(() => fixture.Return(10).WhenNamed((Func<string, bool>)null));
        }

        [Fact]
        public void Fixture_Return_Coustomize()
        {
            var fixture = new Fixture();

            fixture.Return(() => new SomeClass { StringValue = "HelloWorld"})
                .For(typeof(ImportSomeClass));

            var instance = fixture.Generate<ImportSomeClass>();

            Assert.NotNull(instance);
            Assert.NotNull(instance.SomeClass);
            Assert.Equal("HelloWorld", instance.SomeClass.StringValue);
        }
    }
}
