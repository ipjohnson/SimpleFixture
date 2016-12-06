using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SimpleFixture.Impl;
using NSub = NSubstitute;

namespace SimpleFixture.NSubstitute
{
    public class SubstituteConvention : IConvention
    {
        private readonly bool _defaultSingleton;
        private readonly Dictionary<Type, object> _substituted = new Dictionary<Type, object>();

        public SubstituteConvention(bool defaultSingleton)
        {
            _defaultSingleton = defaultSingleton;
        }

        public ConventionPriority Priority => ConventionPriority.Last;

        public event EventHandler<PriorityChangedEventArgs> PriorityChanged;

        public object GenerateData(DataRequest request)
        {
            if (!request.RequestedType.GetTypeInfo().IsInterface)
            {
                return Convention.NoValue;
            }

            object returnValue = null;
            var helper = request.Fixture.Configuration.Locate<IConstraintHelper>();

            bool? singleton = helper.GetValue<bool?>(request.Constraints, null, "fakeSingleton");

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
