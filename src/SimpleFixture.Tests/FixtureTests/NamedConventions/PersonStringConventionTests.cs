using System.Linq;
using SimpleFixture.Conventions.Named;
using SimpleFixture.Impl;
using Xunit;

namespace SimpleFixture.Tests.FixtureTests.NamedConventions
{
    public class PersonStringConventionTests
    {
        #region Name Tests
        [Theory,
        InlineData("FirstName"),
        InlineData("LastName"),
        InlineData("Surname"),
        InlineData("MiddleName")]
        public void Fixture_GenerateFirstName_ReturnsGoodValue(string name)
        {
            var fixture = new Fixture();

            var value = fixture.Generate<string>(name);

            Assert.NotNull(value);
            Assert.True(value.All(c => char.IsLetter(c) || c == '\'' || c == '-'));
        }

        #endregion

        #region Social Security Number

        [Theory, 
        InlineData("SSN"),
        InlineData("SocialSecurityNumber"),
        InlineData("GovernmentId")]
        public void Fixture_GenerateSSN_ReturnsGoodValue(string name)
        {
            var fixture = new Fixture();

            var value = fixture.Generate<string>(name);

            Assert.NotNull(value);
            Assert.Equal(11, value.Length);
            Assert.True(value.All(c => char.IsDigit(c) || c == '-'));
        }

        #endregion
    }
}
