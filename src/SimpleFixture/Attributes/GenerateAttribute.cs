using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFixture.Attributes
{
    public class GenerateAttribute : Attribute
    {
        public object Min { get; set; }

        public object Max { get; set; }

        public string ConstraintName { get; set; }
    }
}
