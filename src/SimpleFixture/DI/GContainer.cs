using System;
using System.Collections.Generic;

namespace SimpleFixture.DI
{
    /// <summary>
    /// Simple dependency injection container
    /// </summary>
    public class GContainer : IGContainer
    {
        private readonly Dictionary<Type, object> _exports = new Dictionary<Type, object>();

        /// <summary>
        /// Export a particular type
        /// </summary>
        /// <typeparam name="T">type being exported</typeparam>
        /// <param name="exportFunc">export function</param>
        public void Export<T>(Func<GContainer, T> exportFunc)
        {
            _exports[typeof(T)] = exportFunc;
        }

        /// <summary>
        /// Export a type as a singleton
        /// </summary>
        /// <typeparam name="T">type to export</typeparam>
        /// <param name="exportFunc">export func</param>
        public void ExportSingleton<T>(Func<GContainer, T> exportFunc)
        {
            T tValue = default (T);

            Func<GContainer, T> singletonFunc = 
                g =>
                {
                    if (Equals(tValue, default(T)))
                    {
                        tValue = exportFunc(g);
                    }

                    return tValue;
                };

            _exports[typeof(T)] = singletonFunc;
        }
        
        /// <summary>
        /// Locate an instance of T
        /// </summary>
        /// <typeparam name="T">type to locate</typeparam>
        /// <returns>new instance of T</returns>
        public T Locate<T>()
        {
            object objectFunc;

            if (!_exports.TryGetValue(typeof(T), out objectFunc))
            {
                throw new Exception("Could not locate type: " + typeof(T).FullName);
            }

            Func<GContainer, T> exportFunc = objectFunc as Func<GContainer, T>;

            if (exportFunc == null)
            {
                throw new Exception("Could not cast func to proper type: " + objectFunc.GetType());
            }

            return exportFunc(this);
        }
    }
}
