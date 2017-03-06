using SimpleFixture.Tests.Classes;
using SimpleFixture.Tests.Classes.NestedNamespace;
using System;
using SimpleFixture.Attributes;
using SimpleFixture.NSubstitute;
using SimpleFixture.Tests.MockTests;
using Xunit;
using SimpleFixture.xUnit;

namespace SimpleFixture.Tests.FixtureTests
{

    [SubFixtureInitialize]
    public class ExportTests
    {
        #region Export
        [Fact]
        public void Fixture_Export_Type()
        {
            var fixture = new Fixture();

            fixture.Export<ImplementInterfaceClass>().As<IImplementInterfaceClass>();

            var instance = fixture.Locate<IImplementInterfaceClass>();

            Assert.NotNull(instance);
            Assert.IsType<ImplementInterfaceClass>(instance);
        }

        [Fact]
        public void Fixture_ExportAs_Type()
        {
            var fixture = new Fixture();

            fixture.ExportAs<ImplementInterfaceClass, IImplementInterfaceClass>();

            var instance = fixture.Locate<IImplementInterfaceClass>();

            Assert.NotNull(instance);
            Assert.IsType<ImplementInterfaceClass>(instance);
        }

        [Fact]
        public void Fixture_ExportSingleton_Type()
        {
            var fixture = new Fixture();

            fixture.ExportSingleton<ImplementInterfaceClass>().As<IImplementInterfaceClass>();

            var instance = fixture.Locate<IImplementInterfaceClass>();

            Assert.NotNull(instance);
            Assert.IsType<ImplementInterfaceClass>(instance);

            var instance2 = fixture.Locate<IImplementInterfaceClass>();

            Assert.NotNull(instance2);
            Assert.IsType<ImplementInterfaceClass>(instance2);

            Assert.Same(instance, instance2);
        }


        [Fact]
        public void Fixture_ExportSingletonAs_Type()
        {
            var fixture = new Fixture();

            fixture.ExportSingletonAs<ImplementInterfaceClass, IImplementInterfaceClass>();

            var instance = fixture.Locate<IImplementInterfaceClass>();

            Assert.NotNull(instance);
            Assert.IsType<ImplementInterfaceClass>(instance);

            var instance2 = fixture.Locate<IImplementInterfaceClass>();

            Assert.NotNull(instance2);
            Assert.IsType<ImplementInterfaceClass>(instance2);

            Assert.Same(instance, instance2);
        }
        #endregion

        #region Export All By Interface

        [Fact]
        public void Fixture_ExportAllByInterface_ReturnExportBulk()
        {
            var fixture = new Fixture();

            fixture.ExportAllByInterface().FromAssemblyContaining<ExportTests>();

            Assert.NotNull(fixture.Locate<IBulkExportInterface>());
            Assert.NotNull(fixture.Locate<IBulkExportInterface2>());
            Assert.NotNull(fixture.Locate<IBulkExportInterface3>());
        }

        [Fact]
        public void Fixture_ExportAllByInterface_ReturnClosedGeneric()
        {
            var fixture = new Fixture();

            fixture.ExportAllByInterface().FromAssemblyContaining<ExportTests>();

            var openGeneric = fixture.Locate<IGenericBulkInterface<int>>();

            Assert.NotNull(openGeneric);
            Assert.IsType<OpenGeneric<int>>(openGeneric);
        }

        [Fact]
        public void Fixture_ExportAllByInterface_OutOfOrderParameters()
        {
            var fixture = new Fixture();

            fixture.ExportAllByInterface().FromAssemblyContaining<ExportTests>();

            var generic = fixture.Locate<IMultipleTypeGenericBulkInterface<int, string>>();

            Assert.NotNull(generic);
            Assert.IsType<MultipleTypeGenericBulk<string, int>>(generic);
        }

        [Fact]
        public void Fixture_ExportAllByInterface_FilterByNamespace()
        {
            var fixture = new Fixture();

            fixture.ExportAllByInterface().FromAssemblyContaining<ExportTests>().InNamespace("SimpleFixture.Tests.Classes", false);

            Assert.Throws<Exception>(() => fixture.Locate<INestedBulkExportInterface>());
        }

        [Fact]
        public void Fixture_ExportAllByInterface_FilterByInterface()
        {
            var fixture = new Fixture();

            fixture.ExportAllByInterface().FromAssemblyContaining<ExportTests>().InterfaceMatching<INestedBulkExportInterface>();

            Assert.Throws<Exception>(() => fixture.Locate<IBulkExportInterface>());
        }


        [Fact]
        public void Fixture_ExportAllByInterface_AsSingleton()
        {
            var fixture = new Fixture();

            fixture.ExportAllByInterface().FromAssemblyContaining<ExportTests>().AsSingleton();

            var instance1 = fixture.Locate<IBulkExportInterface>();
            var instance2 = fixture.Locate<IBulkExportInterface>();

            Assert.Same(instance1, instance2);
        }
        #endregion

        public class SomeInterface : ISomeInterface
        {
            public int SomeIntMethod()
            {
                return 15;
            }
        }

        public interface IAnotherInterface
        {
            
        }

        [Theory]
        [AutoData]
        [Export(typeof(SomeInterface))]
        public void ExportAttribute_Test(ImportSomeInterface import,IAnotherInterface anotherInterface )
        {
            Assert.NotNull(import);

            Assert.Equal(15, import.SomeValue);

            Assert.NotNull(anotherInterface);
        }
    }
}
