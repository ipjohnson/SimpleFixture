﻿namespace SimpleFixture.Tests.Classes
{
    public interface IBulkExportInterface
    {
        int Compute();
    }


    public interface IBulkExportInterface2
    {
        int Compute();
    }

    public interface IBulkExportInterface3
    {
        int Compute();
    }

    public interface IGenericBulkInterface<T>
    {
        T GetT();
    }

    public interface IMultipleTypeGenericBulkInterface<T1,T2>
    {
        T1 Get();
    }
}
