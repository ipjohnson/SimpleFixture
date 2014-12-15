using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
