using System;

namespace SimpleFixture.Attributes
{
    public class GenerateAttribute : Attribute
    {
        public virtual object Min { get; set; }

        public virtual object Max { get; set; }

        public virtual string ConstraintName { get; set; }
    }
}
