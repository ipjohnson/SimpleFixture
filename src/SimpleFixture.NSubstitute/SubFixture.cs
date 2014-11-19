using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFixture.NSubstitute
{
    public class SubFixture : Fixture
    {
        public SubFixture(IFixtureConfiguration configuration = null) : base(configuration)
        {
            Add(new SubstituteConvention());
        }

        public void Substitute<T>(Action<T> substituteAction)
        {
            substituteAction(Locate<T>());
        }
    }
}
