using FluentAssertions;
using SimpleFixture.Tests.Classes;
using Xunit;

namespace SimpleFixture.Tests.FixtureTests
{
    public class ConstraintTests
    {
        [Fact]
        public void Fixture_NamedPropertyConstrain_ReturnsCorrectly()
        {
            var fixture = new Fixture();

            var importClass =
                fixture.Generate<ImportSomeClass>(constraints: new { SomeClass = new SomeClass { IntValue = 50, StringValue = "Test" } });

            importClass.Should().NotBeNull();
            importClass.SomeClass.Should().NotBeNull();
            importClass.SomeClass.IntValue.Should().Be(50);
            importClass.SomeClass.StringValue.Should().Be("Test");
        }

        [Fact]
        public void Fixture_TypedValues_ReturnsCorrectly()
        {
            var fixture = new Fixture();

            var importClass = 
                fixture.Generate<ImportSomeClass>(constraints: new { _Values = new[] { new SomeClass { IntValue = 50, StringValue = "Test" } } });

            importClass.Should().NotBeNull();
            importClass.SomeClass.Should().NotBeNull();
            importClass.SomeClass.IntValue.Should().Be(50);
            importClass.SomeClass.StringValue.Should().Be("Test"); 
        }
    }
}
