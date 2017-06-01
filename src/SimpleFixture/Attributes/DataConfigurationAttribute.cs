using System;
using System.Reflection;

namespace SimpleFixture.Attributes
{
    public interface IFixtureConfigurationAttribute
    {
        IFixtureConfiguration ProvideConfiguration(MethodInfo method);
    }

    public abstract class DataConfigurationAttribute : Attribute, IFixtureConfigurationAttribute
    {
        public abstract IFixtureConfiguration ProvideConfiguration(MethodInfo method);
    }
}
