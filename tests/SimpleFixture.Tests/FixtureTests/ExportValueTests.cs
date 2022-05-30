using System;
using System.Collections.Generic;
using System.Text;
using SimpleFixture.Attributes;
using SimpleFixture.Tests.Classes;
using SimpleFixture.xUnit;
using Xunit;

namespace SimpleFixture.Tests.FixtureTests
{
    public class ExportValueTests
    {
        [Theory]
        [AutoData]
        [ExportValue("someString", "123")]
        public void ExportStringValueTest(StringDependentClass stringDependentClass)
        {
            Assert.Equal("123", stringDependentClass.SomeString);
        }

        [Theory]
        [AutoData]
        [ExportValue("otherString", "123")]
        public void ExportStringValueNotMatchTest(StringDependentClass stringDependentClass)
        {
            Assert.Equal("String", stringDependentClass.SomeString);
        }
    }
}
