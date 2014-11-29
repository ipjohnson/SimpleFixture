using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public void Fixture_CustomizeNewFactoryOneParam_GeneratePopulates()
        {
            var fixture = new Fixture();

            fixture.Customize<ImportSomeClass>().NewFactory<SomeClass>(s => new ImportSomeClass(s));

            var instance = fixture.Generate<ImportSomeClass>();

            Assert.NotNull(instance);
            Assert.NotNull(instance.SomeClass);
        }
    }
}
