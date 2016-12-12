using System.Linq;
using Xunit;

namespace SimpleFixture.Tests.FixtureTests
{
    public class PopulateTests
    {
        public class Person
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }
        }

        [Fact]
        public void Populate_Existing_Object()
        {
            var fixture = new Fixture();
            
            var person = new Person();
            
            fixture.Populate(person); 
            
            Assert.False(string.IsNullOrEmpty(person.FirstName));
            Assert.True(person.FirstName.All(char.IsLetter));

            Assert.False(string.IsNullOrEmpty(person.LastName));
            Assert.True(person.LastName.All(char.IsLetter));
        }
    }
}
