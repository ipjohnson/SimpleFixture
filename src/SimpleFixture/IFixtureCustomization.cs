using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFixture
{
    /// <summary>
    /// Objects implementing this interface can be used to customize a fixture
    /// </summary>
    public interface IFixtureCustomization
    {
        /// <summary>
        /// Customize the fixture
        /// </summary>
        /// <param name="fixture">fixture to customize</param>
        void Customize(Fixture fixture);
    }
}
