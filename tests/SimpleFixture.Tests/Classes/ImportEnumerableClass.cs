using System.Collections.Generic;

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
