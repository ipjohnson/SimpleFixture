using FakeItEasy;
using SimpleFixture.FakeItEasy;
using SimpleFixture.Tests.Classes;
using Xunit;

namespace SimpleFixture.Tests.MockTests
{
    public class FakeTests
    {
        [Fact]
        public void FakeFixture_LocateTypeWithInterface_ReturnsInstance()
        {
            var fixture = new FakeFixture();

            var instance = fixture.Locate<ImportSomeInterface>();

            Assert.NotNull(instance);
            Assert.Equal(0, instance.SomeValue);
        }

        [Fact]
        public void FakeFixture_FakeAndLocateTypeWithInterface_ReturnsInstance()
        {
            var fixture = new FakeFixture();

            fixture.Fake<ISomeInterface>(f => A.CallTo(() => f.SomeIntMethod()).Returns(10));

            var instance = fixture.Locate<ImportSomeInterface>();

            Assert.NotNull(instance);
            Assert.Equal(10, instance.SomeValue);
        }

        [Fact]
        public void FakeFixture_DefaultSingleton_ReturnsCorrectInstance()
        {
            var fixture = new FakeFixture(defaultSingleton: true);

            var instance1 = fixture.Fake<ISomeInterface>(x => A.CallTo(() => x.SomeIntMethod()).Returns(10), singleton: false);

            var instance2 = fixture.Fake<ISomeInterface>(x => A.CallTo(() => x.SomeIntMethod()).Returns(15), singleton: false);

            fixture.Fake<ISomeInterface>(x => A.CallTo(() => x.SomeIntMethod()).Returns(20));

            Assert.Equal(20, fixture.Locate<ISomeInterface>().SomeIntMethod());

            Assert.Equal(10, instance1.SomeIntMethod());

            Assert.Equal(15, instance2.SomeIntMethod());

            Assert.Equal(20, fixture.Locate<ISomeInterface>().SomeIntMethod());
        }

        [Fact]
        public void FakeFixture_DefaultSingletonFalse_ReturnsCorrectInstance()
        {
            var fixture = new FakeFixture(defaultSingleton: false);

            var instance1 = fixture.Fake<ISomeInterface>(x => A.CallTo(() => x.SomeIntMethod()).Returns(10));

            var instance2 = fixture.Fake<ISomeInterface>(x => A.CallTo(() => x.SomeIntMethod()).Returns(15));

            Assert.Equal(0, fixture.Locate<ISomeInterface>().SomeIntMethod());

            Assert.Equal(10, instance1.SomeIntMethod());

            Assert.Equal(15, instance2.SomeIntMethod());

            Assert.Equal(0, fixture.Locate<ISomeInterface>().SomeIntMethod());
        }

        [Fact]
        public void Fakeixture_DefaultSingletonFalseAndSingleton_ReturnsSingleton()
        {
            var fixture = new FakeFixture(defaultSingleton: false);

            fixture.Fake<ISomeInterface>(x => A.CallTo(() => x.SomeIntMethod()).Returns(20), singleton: true);

            var instance1 = fixture.Fake<ISomeInterface>(x => A.CallTo(() => x.SomeIntMethod()).Returns(10));

            Assert.Equal(20, fixture.Fake<ISomeInterface>(singleton: true).SomeIntMethod());

            Assert.Equal(10, instance1.SomeIntMethod());

            Assert.Equal(20, fixture.Fake<ISomeInterface>(singleton: true).SomeIntMethod());

        }
    }
}
