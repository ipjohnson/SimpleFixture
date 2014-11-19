using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFixture.Impl
{
    public interface IConventionProvider
    {
        IEnumerable<IConvention> ProvideConventions(IFixtureConfiguration configuration);
    }
}
