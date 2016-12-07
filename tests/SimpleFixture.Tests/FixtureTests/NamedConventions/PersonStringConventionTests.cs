using System;
using System.Linq;
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

        [Fact]
        public void Fixture_GenerateFirstName_Sex_Girl()
        {
            var fixture = new Fixture();

            var value = fixture.Generate<string>("FirstName", constraints: new { sex = "female" });

            Assert.NotNull(value);
            Assert.True(value.All(c => char.IsLetter(c) || c == '\'' || c == '-'));
        }

        [Fact]
        public void Fixture_GenerateFirstName_Sex_Boy()
        {
            var fixture = new Fixture();

            var value = fixture.Generate<string>("FirstName", constraints: new { sex = "male"});

            Assert.NotNull(value);
            Assert.True(value.All(c => char.IsLetter(c) || c == '\'' || c == '-'));
        }

        [Fact]
        public void Fixture_GenerateHeight_ReturnsGoodValue()
        {
            var fixture = new Fixture();

            var value = fixture.Generate<string>("Height");

            int intValue;

            Assert.NotNull(value);
            Assert.True(int.TryParse(value, out intValue));
        }


        [Fact]
        public void Fixture_GenerateWeight_ReturnsGoodValue()
        {
            var fixture = new Fixture();

            var value = fixture.Generate<string>("Weight");

            int intValue;

            Assert.NotNull(value);
            Assert.True(int.TryParse(value, out intValue));
        }

        [Fact]
        public void Fixture_GenerateDob_ReturnsGoodValue()
        {
            var fixture = new Fixture();

            var value = fixture.Generate<string>("DateOfBirth");

            DateTime date;

            Assert.NotNull(value);
            Assert.True(DateTime.TryParse(value, out date));
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

        #region Email Address
        [Theory,
        InlineData("Email"),
        InlineData("EmailAddress"),
        InlineData("EmailAddr")]
        public void Fixture_GenerateEmail_ReturnsGoodValue(string name)
        {
            var fixture = new Fixture();

            var value = fixture.Generate<string>(name);

            AssertValidEmailAddress(value);
        }

        [Fact]
        public void Fixture_GenerateEmailWithFirstName_ReturnsGoodValue()
        {
            var fixture = new Fixture();
            var firstName = "TestName";

            var value = fixture.Generate<string>("Email", new { firstName });

            AssertValidEmailAddress(value);
            Assert.Contains(firstName, value);
        }


        [Fact]
        public void Fixture_GenerateEmailWithLastName_ReturnsGoodValue()
        {
            var fixture = new Fixture();
            var lastName = "TestName";

            var value = fixture.Generate<string>("Email", new { lastName });

            AssertValidEmailAddress(value);
            Assert.Contains(lastName, value);
        }

        [Fact]
        public void Fixture_GenerateEmailWithBothFirstAndLastName_ReturnsGoodValue()
        {
            var fixture = new Fixture();
            var firstName = "TestFirstName";
            var lastName = "TestLastName";

            var value = fixture.Generate<string>("Email", new { firstName, lastName });

            AssertValidEmailAddress(value);

            Assert.Contains(firstName, value);
            Assert.Contains(lastName, value);
        }

        [Fact]
        public void Fixture_GenerateEmailWithBothFirstAndLastNameAndDomain_ReturnsGoodValue()
        {
            var fixture = new Fixture();
            var firstName = "TestFirstName";
            var lastName = "TestLastName";
            var domain = "TestDomain.com";

            var value = fixture.Generate<string>("Email", new { firstName, lastName, domain });

            AssertValidEmailAddress(value);

            Assert.Contains(firstName, value);
            Assert.Contains(lastName, value);
            Assert.EndsWith(domain, value);
        }

        private static void AssertValidEmailAddress(string value)
        {
            Assert.NotNull(value);
            Assert.Equal(1, value.Count(c => c == '@'));
            Assert.True(value.All(c => char.IsLetterOrDigit(c) || c == '@' || c == '.' || c == '-' || c == '_'));
        }

        #endregion

        #region Password

        [Fact]
        public void Fixture_GeneratePassword_ReturnsGoodValue()
        {
            var fixture = new Fixture();

            var value = fixture.Generate<string>("password");

            Assert.NotNull(value);
            Assert.True(value.Length >= 8);
            Assert.True(value.Any(char.IsUpper));
            Assert.True(value.Any(char.IsLower));
            Assert.True(value.Any(char.IsDigit));
            Assert.False(value.All(char.IsLetterOrDigit));
        }

        #endregion

        #region Username

        [Fact]
        public void Fixture_GenerateUsername_ReturnsGoodValue()
        {
            var fixture = new Fixture();

            var value = fixture.Generate<string>("username");

            Assert.NotNull(value);
            Assert.True(value.All(char.IsLetterOrDigit));
        }

        #endregion

        #region Phone
        [Theory,
        InlineData("Phone"),
        InlineData("PhoneNumber"),
        InlineData("CellPhone"),
        InlineData("MobilePhone"),
        InlineData("HomePhone")]
        public void Fixture_GeneratePhoneNumber_ReturnsGoodValue(string name)
        {
            var fixture = new Fixture();

            var value = fixture.Generate<string>(name);

            Assert.NotNull(value);
            Assert.Equal(12, value.Length);
            Assert.True(value.All(c => char.IsDigit(c) || c == '-'));
        }
        #endregion
    }
}
