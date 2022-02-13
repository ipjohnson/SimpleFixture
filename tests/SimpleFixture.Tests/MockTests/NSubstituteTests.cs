using NSubstitute;
using SimpleFixture.NSubstitute;
using SimpleFixture.Tests.Classes;
using SimpleFixture.xUnit;
using Xunit;

namespace SimpleFixture.Tests.MockTests
{
    public class NSubstituteTests
    {
        [Fact]
        public void SubFixture_LocateTypeWithInterface_ReturnsInstance()
        {
            var fixture = new SubFixture();

            var instance = fixture.Locate<ImportSomeInterface>();

            Assert.NotNull(instance);
            Assert.Equal(0, instance.SomeValue);
        }

        [Fact]
        public void SubFixture_SubstituteAndLocateTypeWithInterface_ReturnsInstance()
        {
            var fixture = new SubFixture();

            fixture.Locate<ISomeInterface>().SomeIntMethod().Returns(10);

            var instance = fixture.Locate<ImportSomeInterface>();

            Assert.NotNull(instance);
            Assert.Equal(10, instance.SomeValue);
        }

        [Fact]
        public void SubFixture_SubstituteDefaultSingleton_ReturnsCorrectImplementations()
        {
            var fixture = new SubFixture(defaultSingleton: true);

            var instance = fixture.Substitute<ISomeInterface>(s => s.SomeIntMethod().Returns(10), singleton: false);

            Assert.NotNull(instance);

            var instance2 = fixture.Substitute<ISomeInterface>(s => s.SomeIntMethod().Returns(15), singleton: false);

            Assert.NotNull(instance2);

            fixture.Locate<ISomeInterface>().SomeIntMethod().Returns(20);

            Assert.Equal(20, fixture.Locate<ISomeInterface>().SomeIntMethod());

            Assert.Equal(15, instance2.SomeIntMethod());

            Assert.Equal(10, instance.SomeIntMethod());

            Assert.Equal(20, fixture.Locate<ISomeInterface>().SomeIntMethod());
        }

        [Fact]
        public void SubFixture_SubstituteDefaultSingletonFalse_ReturnsCorrectImplementations()
        {
            var fixture = new SubFixture(defaultSingleton: false);

            var instance = fixture.Substitute<ISomeInterface>(s => s.SomeIntMethod().Returns(10));

            Assert.NotNull(instance);

            var instance2 = fixture.Substitute<ISomeInterface>(s => s.SomeIntMethod().Returns(15));

            Assert.NotNull(instance2);

            Assert.Equal(0, fixture.Locate<ISomeInterface>().SomeIntMethod());

            Assert.Equal(15, instance2.SomeIntMethod());

            Assert.Equal(10, instance.SomeIntMethod());

            Assert.Equal(0, fixture.Locate<ISomeInterface>().SomeIntMethod());
        }

        [Fact]
        public void SubFixture_SubstituteDefaultSingletonFalseAndSingleton_ReturnsSingleton()
        {
            var fixture = new SubFixture(defaultSingleton: false);

            fixture.Substitute<ISomeInterface>(s => s.SomeIntMethod().Returns(20), singleton: true);

            var instance = fixture.Substitute<ISomeInterface>(s => s.SomeIntMethod().Returns(10));

            Assert.NotNull(instance);

            Assert.Equal(10, instance.SomeIntMethod());

            Assert.Equal(20, fixture.Substitute<ISomeInterface>(singleton: true).SomeIntMethod());
        }

        [Theory]
        [AutoData]
        [SubFixtureInitialize]
        public void SubFixtureInitializeAttribute(ISomeInterface someInterface)
        {
            someInterface.SomeIntMethod().Returns(15);

            Assert.Equal(15, someInterface.SomeIntMethod());
        }


        public interface ISomeOtherInterface
        {

        }

        public delegate bool FilterDelegate(ISomeOtherInterface otherInterface);

        public class SomeImportingClass
        {
            public FilterDelegate DelegateValue { get; set; }
        }

        [Theory]
        [AutoData]
        [SubFixtureInitialize]
        public void SubFixtureInitializeAttributeConcrete(SomeImportingClass someImportingClass)
        {
            Assert.NotNull(someImportingClass);
            Assert.NotNull(someImportingClass.DelegateValue);
        }
    }
}
