using System;
using System.Reflection;
using SimpleFixture.Impl;

namespace SimpleFixture.Conventions
{
    public class ComplexConvention : IConvention
    {
        private readonly IFixtureConfiguration _configuration;
        private readonly ITypeCreator _typeCreator;
        private readonly ITypePopulator _typePopulator;
        private readonly ICircularReferenceHandler _circularReferenceHandler;
        private readonly IModelService _modelService;

        public ComplexConvention(IFixtureConfiguration configuration)
        {
            _configuration = configuration;
            _typeCreator = configuration.Locate<ITypeCreator>();
            _typePopulator = configuration.Locate<ITypePopulator>();
            _circularReferenceHandler = configuration.Locate<ICircularReferenceHandler>();
            _modelService = configuration.Locate<IModelService>();
        }

        public ConventionPriority Priority => ConventionPriority.Last;

        public event EventHandler<PriorityChangedEventArgs> PriorityChanged;

        public object GenerateData(DataRequest request)
        {
            if (_configuration.CircularReferenceHandling == CircularReferenceHandlingAlgorithm.MaxDepth &&
                request.RequestDepth > 100)
            {
                return _circularReferenceHandler.HandleCircularReference(request);
            }

            if (request.RequestedType.GetTypeInfo().IsInterface)
            {
                return Convention.NoValue;
            }

            var model = _modelService.GetModel(request.RequestedType);

            object returnValue = _typeCreator.CreateType(request, model);

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
