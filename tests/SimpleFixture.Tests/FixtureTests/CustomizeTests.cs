using System;
using SimpleFixture.Tests.Classes;
using Xunit;

namespace SimpleFixture.Tests.FixtureTests
{
    public class CustomizeTests
    {
        [Fact]
        public void Fixture_CustomizeSetProperty_ReturnsSetValue()
        {
            var fixture = new Fixture();

            fixture.Customize<SomeClass>().Set(x => x.IntValue, 19);

            var instance = fixture.Generate<SomeClass>();

            Assert.NotNull(instance);
            Assert.Equal(19, instance.IntValue);
        }

        [Fact]
        public void Fixture_CustomizeSetProperty_ReturnsSetValue_Func()
        {
            var fixture = new Fixture();

            fixture.Customize<SomeClass>().Set(x => x.IntValue,() => 19);

            var instance = fixture.Generate<SomeClass>();

            Assert.NotNull(instance);
            Assert.Equal(19, instance.IntValue);
        }


        [Fact]
        public void Fixture_CustomizeSetProperty_Non_Proper_Throws_Exception()
        {
            var fixture = new Fixture();

            Assert.Throws<ArgumentException>(() => fixture.Customize<SomeClass>().Set(x => x.IntValue.GetHashCode(), () => 19));
        }

        [Fact]
        public void Fixture_CustomizeSetProperties_ReturnsSetValue()
        {
            var fixture = new Fixture();

            fixture.Customize<PropertiesClass>().SetProperties(p => p.PropertyType == typeof(int) && p.Name.EndsWith("1"), 50);

            var instance = fixture.Generate<PropertiesClass>();

            Assert.NotNull(instance);
            Assert.Equal(50, instance.IntValue1);
            Assert.NotEqual(50, instance.IntValue2);
        }

        [Fact]
        public void Fixture_CustomizeSkipProperty_ReturnsValueWithSkippedProperty()
        {
            var fixture = new Fixture();

            fixture.Customize<PropertiesClass>().Skip(x => x.StringValue2);

            var instance = fixture.Generate<PropertiesClass>();

            Assert.NotNull(instance);
            Assert.NotNull(instance.StringValue1);
            Assert.Null(instance.StringValue2);
        }

        [Fact]
        public void Fixture_CustomizeSkipProperties_ReturnsValueWithSkippedProperties()
        {
            var fixture = new Fixture();

            fixture.Customize<PropertiesClass>().SkipProperties(p => p.PropertyType == typeof(string));

            var instance = fixture.Generate<PropertiesClass>();

            Assert.NotNull(instance);
            Assert.Null(instance.StringValue1);
            Assert.Null(instance.StringValue2);
        }

        [Fact]
        public void Fixture_CustomizeApply_ReturnsValueWithApply()
        {
            var fixture = new Fixture();

            PropertiesClass propertiesClass = null;

            fixture.Customize<PropertiesClass>().Apply(x => propertiesClass = x);

            var instance = fixture.Generate<PropertiesClass>();

            Assert.NotNull(instance);
            Assert.NotNull(propertiesClass);
            Assert.Same(propertiesClass, instance);
        }


        [Fact]
        public void Fixture_CustomizeNewFactory_GeneratePopulates()
        {
            var fixture = new Fixture();

            fixture.Customize<PropertiesClass>().New(() => new PropertiesClass());

            var instance = fixture.Generate<PropertiesClass>();

            Assert.NotNull(instance);
            Assert.False(string.IsNullOrEmpty(instance.StringValue1));
            Assert.False(string.IsNullOrEmpty(instance.StringValue2));
        }


        [Fact]
        public void Fixture_CustomizeNewFactoryOneParam_GeneratePopulates()
        {
            var fixture = new Fixture();

            fixture.Customize<ImportSomeClass>().NewFactory<SomeClass>(s => new ImportSomeClass(s));

            var instance = fixture.Generate<ImportSomeClass>();

            Assert.NotNull(instance);
            Assert.NotNull(instance.SomeClass);
        }

        [Fact]
        public void Fixture_CustomizeNewFactoryTwoArg_GeneratePopulates()
        {
            var fixture = new Fixture();

            fixture.Customize<PropertiesClass>().NewFactory<int, int>((x, y) => new PropertiesClass { IntValue1 = x, IntValue2 = y }).SkipProperties(p => true);

            var instance = fixture.Generate<PropertiesClass>();

            Assert.NotNull(instance);
            Assert.True(instance.IntValue1 != 0);
            Assert.True(instance.IntValue2 != 0);
            Assert.True(string.IsNullOrEmpty(instance.StringValue1));
            Assert.True(string.IsNullOrEmpty(instance.StringValue2));
        }


        [Fact]
        public void Fixture_CustomizeNewFactoryThreeArg_GeneratePopulates()
        {
            var fixture = new Fixture();

            fixture.Customize<PropertiesClass>().NewFactory<int, int, string>((int1, int2, string1) => new PropertiesClass { IntValue1 = int1, IntValue2 = int2, StringValue1 = string1 }).SkipProperties();

            var instance = fixture.Generate<PropertiesClass>();

            Assert.NotNull(instance);
            Assert.True(instance.IntValue1 != 0);
            Assert.True(instance.IntValue2 != 0);
            Assert.False(string.IsNullOrEmpty(instance.StringValue1));
            Assert.True(string.IsNullOrEmpty(instance.StringValue2));
        }

        [Fact]
        public void Fixture_CustomizeNewFactoryFourArg_GeneratePopulates()
        {
            var fixture = new Fixture();

            fixture.Customize<PropertiesClass>().NewFactory<int, int, string, string>(
                (int1, int2, string1, string2) => new PropertiesClass { IntValue1 = int1, IntValue2 = int2, StringValue1 = string1, StringValue2 = string2 }).SkipProperties();

            var instance = fixture.Generate<PropertiesClass>();

            Assert.NotNull(instance);
            Assert.True(instance.IntValue1 != 0);
            Assert.True(instance.IntValue2 != 0);
            Assert.False(string.IsNullOrEmpty(instance.StringValue1));
            Assert.False(string.IsNullOrEmpty(instance.StringValue2));
        }
    }
}
