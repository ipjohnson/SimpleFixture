using System;
using System.Collections.Generic;
using System.Reflection;

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
            var when = new WhenFilter<object>();

            _behaviors.Add((r, o) =>
                           {
                               if (((IBehaviorFilter<object>)when).PassesFilter(r, o))
                               {
                                   return behavior(r, o);
                               }

                               return o;
                           });
        }

        /// <summary>
        /// Add Behavior to be run on specified type
        /// </summary>
        /// <typeparam name="T">type of object to apply the behavior to</typeparam>
        /// <param name="behavior"></param>
        public WhenFilter<T> Add<T>(Func<DataRequest, T, T> behavior)
        {
            var when = new WhenFilter<T>();

            Func<DataRequest, object, object> objectFunc =
                (r, o) =>
                {
                    if (o is T && ((IBehaviorFilter<T>)when).PassesFilter(r,(T)o))
                    {
                        return behavior(r, (T)o);
                    }

                    return o;
                };

            _behaviors.Add(objectFunc);

            return when;
        }

        /// <summary>
        /// Add behavior for specific type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="behavior"></param>
        public WhenFilter<T> Add<T>(Action<T> behavior)
        {
            var whenFilter = new WhenFilter<T>();

            Func<DataRequest, object, object> objectFunc =
                (r, o) =>
                {
                    if (o is T && ((IBehaviorFilter<T>)whenFilter).PassesFilter(r, (T)o))
                    {
                        behavior((T)o);
                    }

                    return o;
                };

            _behaviors.Add(objectFunc);

            return whenFilter;
        }

        /// <summary>
        /// Apply all the behaviors in the collection to an instance
        /// </summary>
        /// <param name="request">data request</param>
        /// <param name="instance">instance</param>
        /// <returns>instance</returns>
        public object Apply(DataRequest request, object instance)
        {
            foreach (var behavior in _behaviors)
            {
                instance = behavior(request, instance);
            }

            return instance;
        }
    }

    internal interface IBehaviorFilter<T>
    {
        bool PassesFilter(DataRequest request, T instance);
    }

    /// <summary>
    /// Filter for behavior
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class WhenFilter<T> : IBehaviorFilter<T>
    {
        private readonly List<Func<DataRequest, T, bool>> _filters = new List<Func<DataRequest, T, bool>>();

        /// <summary>
        /// Execute behavior when func returns true
        /// </summary>
        /// <param name="filter">when filter</param>
        /// <returns></returns>
        public WhenFilter<T> When(Func<DataRequest, T, bool> filter)
        {
            _filters.Add(filter);

            return this;
        }

        /// <summary>
        /// Execute behavior when request name matches
        /// </summary>
        /// <param name="filter">filter by request name</param>
        /// <returns></returns>
        public WhenFilter<T> WhenRequestName(Func<string, bool> filter)
        {
            _filters.Add((r, i) => filter(r.RequestName));

            return this;
        }

        /// <summary>
        /// Execute behavior when requested for a particular parent type
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <returns></returns>
        public WhenFilter<T> WhenRequestedFor<TRequest>()
        {
            _filters.Add((r, i) =>
                         {
                             if (r.ParentRequest != null &&
                                 r.ParentRequest.RequestedType.GetTypeInfo()
                                     .IsAssignableFrom(typeof(TRequest).GetTypeInfo()))
                             {
                                 return true;
                             }

                             return false;
                         });

            return this;
        }

        bool IBehaviorFilter<T>.PassesFilter(DataRequest request, T instance)
        {
            if (_filters.Count == 0)
            {
                return true;
            }

            foreach (var filter in _filters)
            {
                if (!filter(request, instance))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
