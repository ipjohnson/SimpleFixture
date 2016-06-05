using SimpleFixture.Tests.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SimpleFixture.Tests.FixtureTests
{
    public class ExportTests
    {
        [Fact]
        public void Fixture_ExportAllByInterface_ReturnExportBulk()
        {

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
    }
}
