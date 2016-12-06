using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleFixture.Tests.Classes
{
    public interface IImplementInterfaceClass
    {
        int SomeValue { get; }

        string SomeString { get; }
    }

    public class ImplementInterfaceClass : IImplementInterfaceClass
    {
        public int SomeValue { get; set; }

        public string SomeString { get; set; }
    }
}
