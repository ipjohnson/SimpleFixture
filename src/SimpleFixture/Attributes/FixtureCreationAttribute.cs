using System;

namespace SimpleFixture.Attributes
{
    public abstract class FixtureCreationAttribute : Attribute
    {
        public abstract Fixture CreateFixture();
    }
}
