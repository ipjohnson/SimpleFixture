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
