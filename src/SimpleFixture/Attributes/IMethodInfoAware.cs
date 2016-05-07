using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFixture.Attributes
{
    /// <summary>
    /// Attributes that implement this interface are aware of being run in context of a method
    /// </summary>
    public interface IMethodInfoAware
    {
        MethodInfo Method { get; set; }
    }
}
