using SimpleFixture.Impl;
using Xunit;

namespace SimpleFixture.Tests.FixtureTests
{
    public class RandomDataGeneratorServiceTests
    {
        public enum RandomEnum
        {
            Kinda,
            Sorta,
            Maybe,
            Yes,
            No
        }

        [Fact]
        public void RandomDataGeneratorService_Generate_Decimal()
        {
            var service = new RandomDataGeneratorService();

            service.NextDecimal();
        }

        [Fact]
        public void RandomDataGeneratorService_Generate_Enum()
        {
            var service = new RandomDataGeneratorService();

            var enumValue = service.NextEnum<RandomEnum>();
        }

        [Fact]
        public void RandomDataGeneratorService_Generate_DateTime()
        {
            var service = new RandomDataGeneratorService();

            var dateTime = service.NextDateTime();
        }

        [Fact]
        public void RandomDataGeneratorService_Generate_Timespan()
        {
            var service = new RandomDataGeneratorService();
    
            var value = service.NextTimeSpan();
        }
    }
    }
