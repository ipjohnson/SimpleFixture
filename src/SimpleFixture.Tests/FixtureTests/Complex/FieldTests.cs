using FluentAssertions;
using SimpleFixture.Tests.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SimpleFixture.Tests.FixtureTests.Complex
{
    public class FieldTests
    {
        [Fact]
        public void Fixture_PopulateFields_ReturnsValue()
        {
            var fixture = new Fixture(new DefaultFixtureConfiguration { PopulateFields = true });

            var fieldClass = fixture.Generate<FieldClass>();

            fieldClass.IntField.Should().NotBe(0);
            fieldClass.FirstName.Should().Match(s => s.All(char.IsLetter));
        }
    }
}
