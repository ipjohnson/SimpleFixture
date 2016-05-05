using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFixture.Attributes
{
    public abstract class FixtureInitializationAttribute : Attribute
    {
        public abstract void Initialize(Fixture fixture);
    }
}
