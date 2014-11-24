using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFixture
{
    /// <summary>
    /// A collection of behavior functions to be applied to objects
    /// </summary>
    public class BehaviorCollection
    {
        private readonly List<Func<DataRequest, object, object>> _behaviors;

        /// <summary>
        /// Default constructor
        /// </summary>
        public BehaviorCollection()
        {
            _behaviors = new List<Func<DataRequest, object, object>>();
        }

        /// <summary>
        /// Add behavior that will be run against every object returned by the fixture.
        /// </summary>
        /// <param name="behavior"></param>
        public void Add(Func<DataRequest, object, object> behavior)
        {
            _behaviors.Add(behavior);
        }

        /// <summary>
        /// Add Behavior to be run on specified type
        /// </summary>
        /// <typeparam name="T">type of object to apply the behavior to</typeparam>
        /// <param name="behavior"></param>
        public void Add<T>(Func<DataRequest, T, T> behavior)
        {
            Func<DataRequest, object, object> objectFunc =
                (r, o) =>
                {
                    if (o is T)
                    {
                        return behavior(r, (T)o);
                    }

                    return o;
                };

            _behaviors.Add(objectFunc);
        }

        /// <summary>
        /// Apply all the behaviors in the collection to an instance
        /// </summary>
        /// <param name="request">data request</param>
        /// <param name="instance">instance</param>
        /// <returns>instance</returns>
        public object Apply(DataRequest request, object instance)
        {
            foreach (Func<DataRequest, object, object> behavior in _behaviors)
            {
                instance = behavior(request, instance);
            }

            return instance;
        }
    }
}
