using System;

namespace SimpleFixture.Attributes
{
    public abstract class FixtureInitializationAttribute : Attribute
    {
        public abstract void Initialize(Fixture fixture);
    }
}
