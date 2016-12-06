using System;

namespace SimpleFixture.Attributes
{
    public class LocateAttribute : Attribute
    {
        public virtual object Value { get; set; }
    }
}
