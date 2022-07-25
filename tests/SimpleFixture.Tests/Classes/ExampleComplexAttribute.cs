using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleFixture.Attributes;

namespace SimpleFixture.Tests.Classes
{
    [ExportValue("testing1",1)]
    [ExportValue("testing2", 2)]
    public class ExampleComplexAttribute : Attribute, IComplexAttribute
    {
    }
}
