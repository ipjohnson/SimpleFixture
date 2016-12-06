using SimpleFixture.Moq;
using SimpleFixture.Tests.Classes;
using Xunit;

namespace SimpleFixture.Tests.MockTests
{
    public class MoqTests
    {
        [Fact]
        public void MoqFixture_LocateTypeWithInterface_ReturnsInstance()
        {
            var fixture = new MoqFixture();

            var instance = fixture.Locate<ImportSomeInterface>();

            Assert.NotNull(instance);
            Assert.Equal(0, instance.SomeValue);
        }

        [Fact]
        public void MoqFixture_MockAndLocateTypeWithInterface_ReturnsInstance()
        {
            var fixture = new MoqFixture();

            fixture.Mock<ISomeInterface>(m => m.Setup(x => x.SomeIntMethod()).Returns(10));

            var instance = fixture.Locate<ImportSomeInterface>();

            Assert.NotNull(instance);
            Assert.Equal(10, instance.SomeValue);
        }

        [Fact]
        public void MoqFixture_MockSingleton_ReturnsCorrectInstance()
        {
            var fixture = new MoqFixture(defaultSingleton: true);

            var mock1 = fixture.Mock<ISomeInterface>(m => m.Setup(x => x.SomeIntMethod()).Returns(10), singleton: false);

            Assert.NotNull(mock1);

            var mock2 = fixture.Mock<ISomeInterface>(m => m.Setup(x => x.SomeIntMethod()).Returns(15), singleton: false);

            Assert.NotNull(mock2);

            fixture.Mock<ISomeInterface>(m => m.Setup(x => x.SomeIntMethod()).Returns(20));

            Assert.Equal(20, fixture.Locate<ISomeInterface>().SomeIntMethod());

            Assert.Equal(15, mock2.Object.SomeIntMethod());

            Assert.Equal(10, mock1.Object.SomeIntMethod());

            Assert.Equal(20, fixture.Locate<ISomeInterface>().SomeIntMethod());
        }

        [Fact]
        public void MoqFixture_MockDefaultSingletonFalse_ReturnsCorrectInstance()
        {
            var fixture = new MoqFixture(defaultSingleton: false);

            var mock1 = fixture.Mock<ISomeInterface>(m => m.Setup(x => x.SomeIntMethod()).Returns(10));

            Assert.NotNull(mock1);

            var mock2 = fixture.Mock<ISomeInterface>(m => m.Setup(x => x.SomeIntMethod()).Returns(15));

            Assert.NotNull(mock2);
            
            Assert.Equal(0, fixture.Locate<ISomeInterface>().SomeIntMethod());

            Assert.Equal(15, mock2.Object.SomeIntMethod());

            Assert.Equal(10, mock1.Object.SomeIntMethod());

            Assert.Equal(0, fixture.Locate<ISomeInterface>().SomeIntMethod());
        }

        [Fact]
        public void MoqFixture_MockDefaultSingletonFalseAndSingleton_ReturnsSingleton()
        {
            var fixture = new MoqFixture(defaultSingleton: false);

            fixture.Mock<ISomeInterface>(m => m.Setup(x => x.SomeIntMethod()).Returns(20), singleton: true);

            var mock1 = fixture.Mock<ISomeInterface>(m => m.Setup(x => x.SomeIntMethod()).Returns(10));

            Assert.NotNull(mock1);

            Assert.Equal(20, fixture.Mock<ISomeInterface>(singleton: true).Object.SomeIntMethod());

            Assert.Equal(10, mock1.Object.SomeIntMethod());

            Assert.Equal(20, fixture.Mock<ISomeInterface>(singleton: true).Object.SomeIntMethod());
        }
    }
}
