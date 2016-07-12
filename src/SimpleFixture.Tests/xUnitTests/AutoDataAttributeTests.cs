using FluentAssertions;
using SimpleFixture.Attributes;
using SimpleFixture.Tests.Classes;
//using SimpleFixture.xUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SimpleFixture.Tests.xUnitTests
{
    //public class AutoDataAttributeTests
    //{
    //    [Theory]
    //    [AutoData]
    //    public void AutoData_ProvidesFixture(Fixture fixture)
    //    {
    //        fixture.Should().NotBeNull();
    //    }

    //    [Theory]
    //    [AutoData]
    //    public void AutoData_ProvidesGeneratedData(string firstName, int value)
    //    {
    //        firstName.All(char.IsLetter).Should().BeTrue();
    //    }

    //    [Theory]
    //    [AutoData]
    //    public void AutoData_Freeze(Fixture fixture, [Freeze]int froozen)
    //    {
    //        fixture.Generate<int>().Should().Be(froozen);
    //    }
        
    //    [Theory]
    //    [AutoData]
    //    public void AutoData_FreezeValue(Fixture fixture, [Freeze(Value = 8)]int froozen)
    //    {
    //        froozen.Should().Be(8);
    //        fixture.Generate<int>().Should().Be(froozen);
    //    }

    //    [Theory]
    //    [AutoData]
    //    public void AutoData_LocateData(Fixture fixture, [Locate]int locate)
    //    {
    //        fixture.Locate<int>().Should().Be(locate);
    //    }

    //    [Theory]
    //    [AutoData]
    //    public void AutoData_LocateValue(Fixture fixture, [Locate(Value = 8)]int locate)
    //    {
    //        locate.Should().Be(8);
    //    }
        
    //    [Theory]
    //    [AutoData(8)]
    //    public void AutoData_MixInData(SomeClass someClass, int value)
    //    {
    //        someClass.Should().NotBeNull();
    //        value.Should().Be(8);
    //    }
    //}
}
