using System.Collections.Generic;

namespace SimpleFixture.Impl
{
    public interface IConventionProvider
    {
        IEnumerable<IConvention> ProvideConventions(IFixtureConfiguration configuration);
    }
}
