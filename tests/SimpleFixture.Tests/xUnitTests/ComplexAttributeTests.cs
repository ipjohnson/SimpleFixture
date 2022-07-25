using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleFixture.Tests.Classes;
using SimpleFixture.xUnit;
using Xunit;

namespace SimpleFixture.Tests.xUnitTests
{
    public class ComplexAttributeTests
    {
        [Theory]
        [AutoData]
        [ExampleComplex]
        public void ComplexAttributeTest(int testing1, int testing2)
        {
            Assert.Equal(1, testing1);
            Assert.Equal(2, testing2);
        }
    }
}
