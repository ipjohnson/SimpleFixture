using System;
using System.Reflection;
using SimpleFixture.Impl;

namespace SimpleFixture.Conventions
{
    /// <summary>
    /// Convention for creating an populating objects
    /// </summary>
    public class ComplexConvention : IConvention
    {
        private readonly IFixtureConfiguration _configuration;
        private readonly ITypeCreator _typeCreator;
        private readonly ITypePopulator _typePopulator;
        private readonly ICircularReferenceHandler _circularReferenceHandler;
        private readonly IModelService _modelService;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="configuration"></param>
        public ComplexConvention(IFixtureConfiguration configuration)
        {
            _configuration = configuration;
            _typeCreator = configuration.Locate<ITypeCreator>();
            _typePopulator = configuration.Locate<ITypePopulator>();
            _circularReferenceHandler = configuration.Locate<ICircularReferenceHandler>();
            _modelService = configuration.Locate<IModelService>();
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
            if (_configuration.CircularReferenceHandling == CircularReferenceHandlingAlgorithm.MaxDepth &&
                request.RequestDepth > 100)
            {
                return _circularReferenceHandler.HandleCircularReference(request);
            }

            if (request.RequestedType.GetTypeInfo().IsInterface ||
                request.RequestedType.GetTypeInfo().IsAbstract)
            {
                return Convention.NoValue;
            }

            var model = _modelService.GetModel(request.RequestedType);

            var returnValue = _typeCreator.CreateType(request, model);

            request.Instance = returnValue;

            if (request.Populate)
            {
                _typePopulator.Populate(returnValue, request, model);
            }

            model.Apply(returnValue);

            return returnValue;
        }
    }
}
