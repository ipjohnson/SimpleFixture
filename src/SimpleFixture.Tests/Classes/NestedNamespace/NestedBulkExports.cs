using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFixture.Tests.Classes.NestedNamespace
{
    public interface INestedBulkExportInterface
    {
        int Compute();
    }

    public class NestedBulkExports : INestedBulkExportInterface
    {
        public int Compute()
        {
            return 10;
        }
    }
}
