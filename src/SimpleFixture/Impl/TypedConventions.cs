using System;
using System.Collections.Generic;

namespace SimpleFixture.Impl
{
    /// <summary>
    /// Convention that handles ITypeConvention
    /// </summary>
    public class TypedConventions : IConvention
    {
        private Dictionary<Type, IConventionList> _typedConventions;
        private readonly IFixtureConfiguration _configuration;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="priority"></param>
        public TypedConventions(IFixtureConfiguration configuration, ConventionPriority priority = ConventionPriority.High)
        {
            _configuration = configuration;
            Priority = priority;
        }

        /// <summary>
        /// Add typed convention
        /// </summary>
        /// <param name="typedConvention">convention</param>
        public void AddConvention(ITypedConvention typedConvention)
        {
            if (_typedConventions == null)
            {
                _typedConventions = new Dictionary<Type, IConventionList>();
            }
            
            IConventionList conventionList;

            foreach (var supportedType in typedConvention.SupportedTypes)
            {
                if (!_typedConventions.TryGetValue(supportedType, out conventionList))
                {
                    conventionList = _configuration.Locate<IConventionList>();
                    _typedConventions[supportedType] = conventionList;
                }

                conventionList.AddConvention(typedConvention);
            }
        }

        /// <summary>
        /// Priority for convention
        /// </summary>
        public ConventionPriority Priority { get; protected set; }

        /// <summary>
        /// Priorit changed event
        /// </summary>
        public event EventHandler<PriorityChangedEventArgs> PriorityChanged;

        /// <summary>
        /// Generate data for the request
        /// </summary>
        /// <param name="request">data request</param>
        /// <returns></returns>
        public object GenerateData(DataRequest request)
        {
            if (_typedConventions == null)
            {
                return Convention.NoValue;
            }

            object returnValue = null;
            IConventionList conventionList;

            if (_typedConventions.TryGetValue(request.RequestedType, out conventionList))
            {
                conventionList.TryGetValue(request, out returnValue);
            }

            if (returnValue == null && request.RequestedType.IsConstructedGenericType)
            {
                var openType = request.RequestedType.GetGenericTypeDefinition();

                if (_typedConventions.TryGetValue(openType, out conventionList))
                {
                    conventionList.TryGetValue(request, out returnValue);
                }
            }

            return returnValue ?? Convention.NoValue;
        }
    }
}
