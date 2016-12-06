using SimpleFixture.Tests.Classes;
using Xunit;

namespace SimpleFixture.Tests.FixtureTests
{
    public class FreezeTests
    {
        [Fact]
        public void Fixture_FreezeInt_ReturnsInt()
        {
            var fixture = new Fixture();

            var froozen = fixture.Freeze<int>();

            Assert.Equal(froozen, fixture.Generate<int>());
        }

        [Fact]
        public void Fixture_FreezeIntForType_ReturnsFroozenInt()
        {
            var fixture = new Fixture();

            var froozen = fixture.Freeze<int>(value: i => i.For<SomeClass>());

            var instance = fixture.Generate<SomeClass>();

            Assert.NotEqual(froozen, fixture.Generate<int>());

            Assert.Equal(froozen, instance.IntValue);
        }

        [Fact]
        public void Fixture_FreezeIntWhenNamed_ReturnsFroozenInt()
        {
            var fixture = new Fixture();

            var froozen = fixture.Freeze<int>(value: i => i.WhenNamed(name => name.EndsWith("1")));

            var instance1 = fixture.Generate<PropertiesClass>();
            var instance2 = fixture.Generate<PropertiesClass2>();

            Assert.Equal(froozen, instance1.IntValue1);
            Assert.Equal(froozen, instance2.IntValue1);

            Assert.NotEqual(froozen,instance1.IntValue2);
            Assert.NotEqual(froozen,instance2.IntValue2);
        }
    }
}
