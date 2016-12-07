using System;
using System.Collections.Generic;
using System.Reflection;
using SimpleFixture.Impl;
using NSub = NSubstitute;

namespace SimpleFixture.NSubstitute
{
    /// <summary>
    /// Convention that uses NSubstitute
    /// </summary>
    public class SubstituteConvention : IConvention
    {
        private readonly bool _defaultSingleton;
        private readonly Dictionary<Type, object> _substituted = new Dictionary<Type, object>();

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="defaultSingleton"></param>
        public SubstituteConvention(bool defaultSingleton)
        {
            _defaultSingleton = defaultSingleton;
        }

        /// <summary>
        /// Prioirity the convention should be looked at
        /// </summary>
        public ConventionPriority Priority => ConventionPriority.Last;

        /// <summary>
        /// Priorit changed event
        /// </summary>
        public event EventHandler<PriorityChangedEventArgs> PriorityChanged;

        /// <summary>
        /// Generate data for the request, return Convention.NoValue if the convention has no value to provide
        /// </summary>
        /// <param name="request">data request</param>
        /// <returns>generated data value</returns>
        public object GenerateData(DataRequest request)
        {
            if (!request.RequestedType.GetTypeInfo().IsInterface)
            {
                return Convention.NoValue;
            }

            object returnValue = null;
            var helper = request.Fixture.Configuration.Locate<IConstraintHelper>();

            var singleton = helper.GetValue<bool?>(request.Constraints, null, "fakeSingleton");

            if (!singleton.HasValue)
            {
                singleton = _defaultSingleton;
            }

            if (singleton.Value)
            {
                if (_substituted.TryGetValue(request.RequestedType, out returnValue))
                {
                    return returnValue;
                }
            }
            
            returnValue = NSub.Substitute.For(new[] { request.RequestedType }, new object[0]);

            if(singleton.Value)
            {
                _substituted[request.RequestedType] = returnValue;
            }

            return returnValue;
        }
    }
}
