using System;
using System.Collections.Generic;
using System.Reflection;
using Moq;
using SimpleFixture.Impl;

namespace SimpleFixture.Moq
{
    /// <summary>
    /// Convention for Moq
    /// </summary>
    public class MoqConvention : IConvention
    {
        private readonly bool _defaultSingleton;
        private readonly Dictionary<Type, Mock> _mocks = new Dictionary<Type, Mock>();

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="defaultSingleton"></param>
        public MoqConvention(bool defaultSingleton)
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
            if (request.RequestedType.GetTypeInfo().IsInterface ||
                request.RequestedType.GetTypeInfo().IsAbstract)
            {
                return ProcessInterfaceRequest(request);
            }

            if (request.RequestedType.IsConstructedGenericType &&
                request.RequestedType.GetGenericTypeDefinition() == typeof(Mock<>))
            {
                return ProcessMockRequest(request);
            }

            return Convention.NoValue;
        }

        private object ProcessMockRequest(DataRequest request)
        {
            var singleton = GetMoqSingleton(request);

            Mock mockObject;

            if (singleton && _mocks.TryGetValue(request.RequestedType, out mockObject))
            {
                return mockObject;
            }

            mockObject = (Mock)Activator.CreateInstance(request.RequestedType);

            if (singleton)
            {
                _mocks[request.RequestedType] = mockObject;
            }

            return mockObject;
        }

        private object ProcessInterfaceRequest(DataRequest request)
        {
            var mockedType = typeof(Mock<>).GetTypeInfo().MakeGenericType(request.RequestedType);

            Mock mockObject;
            var singleton = GetMoqSingleton(request);

            if (singleton && _mocks.TryGetValue(mockedType, out mockObject))
            {
                return mockObject.Object;
            }

            mockObject = (Mock)Activator.CreateInstance(mockedType);

            if (singleton)
            {
                _mocks[mockedType] = mockObject;
            }

            return mockObject.Object;
        }

        private bool GetMoqSingleton(DataRequest request)
        {
            var helper = request.Fixture.Configuration.Locate<IConstraintHelper>();

            var singleton = helper.GetValue<bool?>(request.Constraints, null, "moqSingleton");

            if (!singleton.HasValue)
            {
                singleton = _defaultSingleton;
            }

            return singleton.Value;
        }
    }
}
