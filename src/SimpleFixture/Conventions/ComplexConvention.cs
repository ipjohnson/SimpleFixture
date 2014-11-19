using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SimpleFixture.Impl;

namespace SimpleFixture.Conventions
{
    public class ComplexConvention : IConvention
    {
        private readonly ITypeCreator _typeCreator;
        private readonly ITypePopulator _typePopulator;
        private readonly ICircularReferenceHandler _circularReferenceHandler;

        public ComplexConvention(IFixtureConfiguration configuration)
        {

            _typeCreator = configuration.Locate<ITypeCreator>();
            _typePopulator = configuration.Locate<ITypePopulator>();
            _circularReferenceHandler = configuration.Locate<ICircularReferenceHandler>();
        }

        public ConventionPriority Priority { get { return ConventionPriority.Last; } }

        public object GenerateData(DataRequest request)
        {
            if (request.RequestDepth > 100)
            {
                return _circularReferenceHandler.HandleCircularReference(request);
            }

            if (request.RequestedType.GetTypeInfo().IsInterface)
            {
                return Convention.NoValue;
            }

            object returnValue = _typeCreator.CreateType(request);

            if (request.Populate)
            {
                _typePopulator.Populate(returnValue, request);
            }

            return returnValue;
        }
    }
}
