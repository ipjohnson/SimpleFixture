using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFixture.DI
{
    public class GContainer : IGContainer
    {
        private readonly Dictionary<Type, object> _exports = new Dictionary<Type, object>();
 
        public void Export<T>(Func<GContainer, T> exportFunc)
        {
            _exports[typeof(T)] = exportFunc;
        }

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
