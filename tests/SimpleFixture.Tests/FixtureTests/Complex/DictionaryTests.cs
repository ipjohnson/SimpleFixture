using System.Collections.Generic;
using Xunit;

namespace SimpleFixture.Tests.FixtureTests
{
    public class DictionaryTests
    {
        [Fact]
        public void Fixture_GenerateDictionaryIntString_ReturnsPopulatedDictionary()
        {
            var fixture = new Fixture();

            var dictionary = fixture.Generate<Dictionary<int, string>>();

            Assert.NotNull(dictionary);
            Assert.True(dictionary.Count > 0);
        }

        [Fact]
        public void Fixture_LocateDictionaryIntString_ReturnsPopulatedDictionary()
        {
            var fixture = new Fixture();

            var dictionary = fixture.Locate<Dictionary<int, string>>();

            Assert.NotNull(dictionary);
            Assert.True(dictionary.Count == 0);
        }
    }
}
