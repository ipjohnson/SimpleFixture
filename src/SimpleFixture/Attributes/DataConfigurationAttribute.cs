using System;
using System.Reflection;

namespace SimpleFixture.Attributes
{
    public abstract class DataConfigurationAttribute : Attribute
    {
        public abstract DefaultFixtureConfiguration ProvideConfiguration(MethodInfo method);
    }
}
