using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using SimpleFixture.NSubstitute;
using SimpleFixture.Tests.Classes;
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
        public void SubFixture_SubstituteNonSingleton_ReturnsCorrectImplementations()
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

    }
}
