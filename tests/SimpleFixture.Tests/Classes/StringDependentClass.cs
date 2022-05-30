using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleFixture.Tests.Classes
{
    public class StringDependentClass
    {
        public StringDependentClass(string someString)
        {
            SomeString = someString;
        }

        public string SomeString { get; }
    }
}
