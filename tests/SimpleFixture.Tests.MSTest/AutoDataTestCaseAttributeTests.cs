using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleFixture.MSTest;
using FluentAssertions;
using SimpleFixture.Attributes;
using System.Linq;
using SimpleFixture.Conventions;

namespace SimpleFixture.Tests.MSTest
{
    [TestClass]
    public class AutoDataTestCaseAttributeTests
    {
        [AutoDataTestCase]
        public void MSTest_AutoData_ProvidesFixture(Fixture fixture)
        {
            fixture.Should().NotBeNull();
        }

        [AutoDataTestCase]
        public void MSTest_AutoData_ProvidesData(string someString, int value)
        {
            someString.Should().Be(StringConvention.LocateValue);
            value.Should().Be(IntConvention.LocateValue);
        }
        
        [AutoDataTestCase]
        public void MSTest_AutoData_ProvidesGeneratedData([Generate]string firstName, [Generate]int value)
        {
            firstName.All(Char.IsLetter).Should().BeTrue();
            firstName.Should().NotBe(StringConvention.LocateValue);
        }

        [AutoDataTestCase]
        public void MSTest_AutoData_Freeze(Fixture fixture, [Freeze]int froozen)
        {
            fixture.Generate<int>().Should().Be(froozen);
        }

        [AutoDataTestCase]
        public void MSTest_AutoData_FreezeValue(Fixture fixture, [Freeze(Value = 8)]int froozen)
        {
            froozen.Should().Be(8);
            fixture.Generate<int>().Should().Be(froozen);
        }

        [AutoDataTestCase]
        public void MSTest_AutoData_LocateData(Fixture fixture, [Locate]int locate)
        {
            fixture.Locate<int>().Should().Be(locate);
        }

        [AutoDataTestCase]
        public void MSTest_AutoData_LocateValue(Fixture fixture, [Locate(Value = 8)]int locate)
        {
            locate.Should().Be(8);
        }

        [AutoDataTestCase(8)]
        public void MSTest_AutoData_MixInData(SomeClass someClass, int value)
        {
            someClass.Should().NotBeNull();
            value.Should().Be(8);
        }
    }
}
