using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFixture
{
    public interface ITypedConvention : IConvention
    {
        IEnumerable<Type> SupportedTypes { get; }
    }
}
