using SimpleFixture.Attributes;
using SimpleFixture.Conventions;
using SimpleFixture.xUnit;
using Xunit;

namespace SimpleFixture.Tests.xUnitTests
{
    
    public class FixtureCustomizationAttributeTests
    {
        [Theory]
        [AutoData]
        [FixtureCustomization(typeof(Customization))]
        public void FixtureCustomizationAttribute_Customization_Test(int intValue)
        {
            Assert.Equal(100, intValue);
        }

        [Theory]
        [AutoData]
        [FixtureCustomization(typeof(IntConvention))]
        public void FixtureCustomizationAttribute_Convention_Test(int intValue)
        {
            Assert.Equal(500, intValue);
        }
    }

    public class Customization : IFixtureCustomization
    {
        public void Customize(Fixture fixture)
        {
            fixture.Return(100);
        }
    }

    public class IntConvention : SimpleTypeConvention<int>
    {
        public override object GenerateData(DataRequest request)
        {
            return 500;
        }
    }
}
