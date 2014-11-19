using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFixture.DI
{
    public interface IGContainer
    {
        void Export<T>(Func<GContainer, T> exportFunc);

        T Locate<T>();
    }
}
