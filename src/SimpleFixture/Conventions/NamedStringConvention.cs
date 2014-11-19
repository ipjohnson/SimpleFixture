using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFixture.Conventions
{
    public class NamedStringConvention : SimpleTypeConvention<string>
    {
        public override ConventionPriority Priority
        {
            get { return ConventionPriority.Low; }
        }

        public override object GenerateData(DataRequest request)
        {
            return Convention.NoValue;
        }
    }
}
