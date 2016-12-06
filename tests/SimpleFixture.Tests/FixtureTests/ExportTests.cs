using SimpleFixture.Tests.Classes;
using SimpleFixture.Tests.Classes.NestedNamespace;
using System;
using Xunit;

namespace SimpleFixture.Tests.FixtureTests
{
    public class ExportTests
    {
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

            fixture.ExportAllByInterface().FromAssemblyContaining<ExportTests>().InNamespace("SimpleFixture.Tests.Classes",false);

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
    }
}
