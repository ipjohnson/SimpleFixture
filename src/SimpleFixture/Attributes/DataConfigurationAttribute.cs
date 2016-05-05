using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFixture.Attributes
{
    public abstract class DataConfigurationAttribute : Attribute
    {
        public abstract DefaultFixtureConfiguration ProvideConfiguration(MethodInfo method);
    }
}
