using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFixture.Tests.Classes
{
    public class BulkExport : IBulkExportInterface
    {
        public int Compute()
        {
            return 5;
        }
    }

    public class BulkExport2 : IBulkExportInterface2
    {
        public int Compute()
        {
            return 10;
        }
    }


    public class BulkExport3 : IBulkExportInterface3
    {
        public int Compute()
        {
            return 10;
        }
    }

    public class OpenGeneric<T> : IGenericBulkInterface<T>
    {
        public T GetT()
        {
            return default(T);
        }
    }

    public class MultipleTypeGenericBulk<T2, T1> : IMultipleTypeGenericBulkInterface<T1, T2>
    {
        public T1 Get()
        {
            return default(T1);
        }
    }
}
