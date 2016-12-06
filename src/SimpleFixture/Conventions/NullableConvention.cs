using System;

namespace SimpleFixture.Conventions
{
    public class NullableConvention : IConvention
    {
        public ConventionPriority Priority => ConventionPriority.Low;

        public event EventHandler<PriorityChangedEventArgs> PriorityChanged;

        public object GenerateData(DataRequest request)
        {
            if (request.RequestedType.IsConstructedGenericType &&
                request.RequestedType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                var newRequest = new DataRequest(request, request.RequestedType.GenericTypeArguments[0]);

                return newRequest.Fixture.Generate(newRequest);
            }

            return Convention.NoValue;
        }
    }
}
