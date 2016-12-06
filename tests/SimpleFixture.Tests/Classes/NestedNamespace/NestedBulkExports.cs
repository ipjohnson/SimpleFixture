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
