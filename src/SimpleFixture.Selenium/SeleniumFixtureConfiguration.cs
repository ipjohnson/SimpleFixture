using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleFixture.Impl;

namespace SimpleFixture.Selenium
{
    public class SeleniumFixtureConfiguration : DefaultFixtureConfiguration
    {
        public SeleniumFixtureConfiguration()
        {
            Initialize();
        }

        private void Initialize()
        {
            Export<ITypePropertySelector>(g => new SeleniumTypePropertySelector(g.Locate<IConstraintHelper>()));
        }
    }
}
