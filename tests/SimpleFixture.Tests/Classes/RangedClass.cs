using System.ComponentModel.DataAnnotations;

namespace SimpleFixture.Tests.Classes
{
    public class RangedClass
    {
        [Range(100, 200)]
        public int IntValue { get; set; }

        [StringLength(100, MinimumLength = 50)]
        public string TestString { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }
    }
}
