using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NSubstitute;
using SimpleFixture.Moq;
using SimpleFixture.NSubstitute;
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
    }
}
