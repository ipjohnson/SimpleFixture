using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFixture.Impl
{
    public interface ICircularReferenceHandler
    {
        object HandleCircularReference(DataRequest request);
    }

    public class CircularReferenceHandler : ICircularReferenceHandler
    {
        public object HandleCircularReference(DataRequest request)
        {
            throw new Exception("Circular reference detected creating " + request.RequestedType.FullName);
        }
    }
}
