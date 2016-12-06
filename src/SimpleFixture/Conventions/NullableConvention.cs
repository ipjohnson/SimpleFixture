using System;

namespace SimpleFixture.Conventions
{
    /// <summary>
    /// Convention for creating nullable
    /// </summary>
    public class NullableConvention : IConvention
    {
        /// <summary>
        /// Prioirity the convention should be looked at
        /// </summary>
        public ConventionPriority Priority => ConventionPriority.Low;

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
