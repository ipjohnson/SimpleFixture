using System;
using SimpleFixture.Conventions;
using Xunit;

namespace SimpleFixture.Tests.FixtureTests.Primitives
{
    public class UriFixtureTests
    {
        [Fact]
        public void Fixture_GenerateUri_ReturnsPopulatedValue()
        {
            var fixture = new Fixture();

            var uri = fixture.Generate<Uri>();

            Assert.NotNull(uri);
        }

        [Fact]
        public void Fixture_LocateUri_ReturnsLocateValue()
        {
            var fixture = new Fixture();

            var uri = fixture.Locate<Uri>();

            Assert.NotNull(uri);
            Assert.Equal(UriConvention.LocateValue, uri);
        }

        [Fact]
        public void Fixture_GenerateUriWithConstraints_ReturnsPopulatedValue()
        {
            var fixture = new Fixture();

            var uri = fixture.Generate<Uri>(constraints: new { uriDomain = "apple.com", uriScheme = "ftp" });

            Assert.NotNull(uri);
            Assert.Equal("ftp", uri.Scheme);
            Assert.Equal("apple.com", uri.Host);
        }
    }
}
