using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SimpleFixture.Impl;

namespace SimpleFixture.Selenium
{
    public class SeleniumTypePropertySelector : TypePropertySelector
    {
        public SeleniumTypePropertySelector(IConstraintHelper helper) : base(helper)
        {

        }

        public override IEnumerable<PropertyInfo> SelectProperties(object instance, DataRequest request, ComplexModel model)
        {
            // Skip populating properties that have selenium attributes
            return base.SelectProperties(instance, request, model).
                        Where(p => !p.GetCustomAttributes(true).Any(o => o.GetType().Namespace.StartsWith("OpenQA.Selenium")));
        }
    }
}
