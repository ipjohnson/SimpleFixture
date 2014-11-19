using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFixture
{
    public class Convention
    {
        public static object NoValue = new object();
    }

    public enum ConventionPriority
    {
        High,
        Normal,
        Low,
        Last
    }

    public interface IConvention
    {
        ConventionPriority Priority { get; }

        object GenerateData(DataRequest request);
    }
}
