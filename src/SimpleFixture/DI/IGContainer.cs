using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFixture.DI
{
    /// <summary>
    /// Simple dependency injection container interface
    /// </summary>
    public interface IGContainer
    {
        /// <summary>
        /// Export a particular type
        /// </summary>
        /// <typeparam name="T">type being exported</typeparam>
        /// <param name="exportFunc">export function</param>
        void Export<T>(Func<GContainer, T> exportFunc);

        /// <summary>
        /// Locate instance of T
        /// </summary>
        /// <typeparam name="T">type to locate</typeparam>
        /// <returns>instance of T</returns>
        T Locate<T>();
    }
}
