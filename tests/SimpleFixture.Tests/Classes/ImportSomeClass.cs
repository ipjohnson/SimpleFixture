using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFixture.Tests.Classes
{
    public class ImportSomeClass
    {
        private readonly SomeClass _someClass;

        public ImportSomeClass(SomeClass someClass)
        {
            _someClass = someClass;
        }

        public SomeClass SomeClass => _someClass;
    }
}
