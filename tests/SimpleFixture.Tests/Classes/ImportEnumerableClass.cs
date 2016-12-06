using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleFixture.Tests.Classes
{
    public class ImportEnumerableClass
    {
        public ImportEnumerableClass(IEnumerable<int> intValues)
        {
            IntValues = intValues;
        }

        public IEnumerable<int> IntValues { get; }
    }
}
