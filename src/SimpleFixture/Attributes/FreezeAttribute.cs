using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFixture.Attributes
{
    public class FreezeAttribute : Attribute

    {
        public Type For { get; set; }

        public object Value { get; set; }
    }
}
