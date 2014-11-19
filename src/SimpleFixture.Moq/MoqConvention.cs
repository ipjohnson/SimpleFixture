using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace SimpleFixture.Moq
{
    public class MoqConvention : IConvention
    {
        private readonly Dictionary<Type,Mock> _mocks = new Dictionary<Type, Mock>(); 

        public ConventionPriority Priority
        {
            get { return ConventionPriority.Last; }
        }

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
            Mock mockObject;

            if (_mocks.TryGetValue(request.RequestedType, out mockObject))
            {
                return mockObject;
            }

            mockObject = (Mock)Activator.CreateInstance(request.RequestedType);

            _mocks[request.RequestedType] = mockObject;

            return mockObject;
        }

        private object ProcessInterfaceRequest(DataRequest request)
        {
            Type mockedType = typeof(Mock<>).GetTypeInfo().MakeGenericType(request.RequestedType);

            Mock mockObject;

            if (_mocks.TryGetValue(mockedType, out mockObject))
            {
                return mockObject.Object;
            }

            mockObject = (Mock)Activator.CreateInstance(mockedType);

            _mocks[mockedType] = mockObject;

            return mockObject.Object;
        }
    }
}
