using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFixture.Conventions
{
    public class NullableConvention : IConvention
    {
        public ConventionPriority Priority
        {
            get { return ConventionPriority.Low; }
        }

        public object GenerateData(DataRequest request)
        {
            if (request.RequestedType.IsConstructedGenericType &&
                request.RequestedType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                DataRequest newRequest = new DataRequest(request, request.RequestedType.GenericTypeArguments[0]);

                return newRequest.Fixture.Generate(newRequest);
            }

            return Convention.NoValue;
        }
    }
}
