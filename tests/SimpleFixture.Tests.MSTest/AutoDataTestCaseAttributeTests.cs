using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleFixture.MSTest;
using FluentAssertions;
using SimpleFixture.Attributes;
using System.Linq;

namespace SimpleFixture.Tests.MSTest
{
    [TestClass]
    public class AutoDataTestCaseAttributeTests
    {
        [AutoDataTestCase]
        public void AutoData_ProvidesFixture(Fixture fixture)
        {
            fixture.Should().NotBeNull();
        }

        [AutoDataTestCase]
        public void AutoData_ProvidesGeneratedData(string firstName, int value)
        {
            firstName.All(char.IsLetter).Should().BeTrue();
        }

        [AutoDataTestCase]
        public void AutoData_Freeze(Fixture fixture, [Freeze]int froozen)
        {
            fixture.Generate<int>().Should().Be(froozen);
        }

        [AutoDataTestCase]
        public void AutoData_FreezeValue(Fixture fixture, [Freeze(Value = 8)]int froozen)
        {
            froozen.Should().Be(8);
            fixture.Generate<int>().Should().Be(froozen);
        }

        [AutoDataTestCase]
        public void AutoData_LocateData(Fixture fixture, [Locate]int locate)
        {
            fixture.Locate<int>().Should().Be(locate);
        }

        [AutoDataTestCase]
        public void AutoData_LocateValue(Fixture fixture, [Locate(Value = 8)]int locate)
        {
            locate.Should().Be(8);
        }

        [AutoDataTestCase(8)]
        public void AutoData_MixInData(SomeClass someClass, int value)
        {
            someClass.Should().NotBeNull();
            value.Should().Be(8);
        }
    }
}
