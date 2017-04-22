using System;
using FluentAssertions;
using SimpleFixture.Attributes;
using SimpleFixture.Tests.Classes;
//using SimpleFixture.xUnit;
using System.Linq;
using SimpleFixture.Conventions;
using SimpleFixture.xUnit;
using Xunit;

namespace SimpleFixture.Tests.xUnitTests
{
    public class AutoDataAttributeTests
    {
        [Theory]
        [AutoData]
        public void AutoData_ProvidesFixture(Fixture fixture)
        {
            fixture.Should().NotBeNull();
        }

        [Theory]
        [AutoData]
        public void AutoData_ProvidesData(string firstName, int value)
        {
            firstName.Should().Be(StringConvention.LocateValue);
            value.Should().Be(SimpleFixture.Conventions.IntConvention.LocateValue);
        }

        [Theory]
        [AutoData]
        public void AutoData_ProvidesGeneratedData([Generate]string firstName,[Generate] int value)
        {
            firstName.All(Char.IsLetter).Should().BeTrue();
            firstName.Should().NotBe(StringConvention.LocateValue);
        }

        [Theory]
        [AutoData]
        public void AutoData_Freeze(Fixture fixture, [Freeze]int froozen)
        {
            fixture.Generate<int>().Should().Be(froozen);
        }

        [Theory]
        [AutoData]
        public void AutoData_FreezeValue(Fixture fixture, [Freeze(Value = 8)]int froozen)
        {
            froozen.Should().Be(8);
            fixture.Generate<int>().Should().Be(froozen);
        }

        [Theory]
        [AutoData]
        public void AutoData_LocateData(Fixture fixture, [Locate]int locate)
        {
            fixture.Locate<int>().Should().Be(locate);
        }

        [Theory]
        [AutoData]
        public void AutoData_LocateValue(Fixture fixture, [Locate(Value = 8)]int locate)
        {
            locate.Should().Be(8);
        }

        [Theory]
        [AutoData(8)]
        public void AutoData_MixInData(SomeClass someClass, int value)
        {
            someClass.Should().NotBeNull();
            value.Should().Be(8);
        }
    }
}
