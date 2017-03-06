using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleFixture.Tests.Classes
{
    public class DependentClass<T>
    {
        public DependentClass(T value)
        {
            Value = value;
        }

        public T Value { get; }
    }
}
