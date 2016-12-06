using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFixture.Tests.Classes
{
    public class ImportSomeInterface
    {
        private ISomeInterface _someInterface;

        public ImportSomeInterface(ISomeInterface someInterface)
        {
            _someInterface = someInterface;
        }

        public int SomeValue => _someInterface.SomeIntMethod();
    }
}
