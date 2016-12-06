using System;
using Xunit;

namespace SimpleFixture.Tests.FixtureTests.Primitives
{
    public class TimeSpanTests
    {
        [Fact]
        public void Fixture_LocateTimeSpan_ReturnsRandomTimeSpan()
        {
            var fixture = new Fixture();

            var timestamp = fixture.Generate<TimeSpan>();

            Assert.NotNull(timestamp);

            var timestamp2 = fixture.Generate<TimeSpan>();

            Assert.NotEqual(timestamp, timestamp2);
        }

        [Fact]
        public void Fixture_LocateTimeSpan_ConstraintValue()
        {
            var fixture = new Fixture();
            var constraint = new { min = new TimeSpan(10000), max = new TimeSpan(11000) };
            var timestamp = fixture.Generate<TimeSpan>(constraints: constraint);

            Assert.NotNull(timestamp);
            Assert.InRange(timestamp, constraint.min, constraint.max);
        }
    }
}
