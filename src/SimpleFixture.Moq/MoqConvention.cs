using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Moq;
using SimpleFixture.Impl;

namespace SimpleFixture.Moq
{
    public class MoqConvention : IConvention
    {
        private bool _defaultSingleton;
        private readonly Dictionary<Type, Mock> _mocks = new Dictionary<Type, Mock>();

        public MoqConvention(bool defaultSingleton)
        {
            _defaultSingleton = defaultSingleton;
        }

        public ConventionPriority Priority => ConventionPriority.Last;

        public event EventHandler<PriorityChangedEventArgs> PriorityChanged;

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
            bool singleton = GetMoqSingleton(request);

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
            Type mockedType = typeof(Mock<>).GetTypeInfo().MakeGenericType(request.RequestedType);

            Mock mockObject;
            bool singleton = GetMoqSingleton(request);

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

            bool? singleton = helper.GetValue<bool?>(request.Constraints, null, "moqSingleton");

            if (!singleton.HasValue)
            {
                singleton = _defaultSingleton;
            }

            return singleton.Value;
        }
    }
}
