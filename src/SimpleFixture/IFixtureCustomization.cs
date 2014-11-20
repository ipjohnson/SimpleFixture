using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFixture
{
    public interface IFixtureCustomization
    {
        void Customize(Fixture fixture);
    }
}
