using System;
using System.Linq;
using System.Reflection;
using SimpleFixture.Impl;

namespace SimpleFixture.Conventions
{
    /// <summary>
    /// Convention for creating enum
    /// </summary>
    public class EnumConvention : IConvention
    {
		private readonly IRandomDataGeneratorService _dataGenerator;

        /// <summary>
        /// DEfault constructor
        /// </summary>
        /// <param name="dataGenerator"></param>
        /// <param name="constraintHelper"></param>
		public EnumConvention(IRandomDataGeneratorService dataGenerator, IConstraintHelper constraintHelper)
        {
            _dataGenerator = dataGenerator;
        }

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
			if(request.RequestedType.GetTypeInfo().IsEnum)
			{
				if(!request.Populate)
				{
					return Enum.GetValues(request.RequestedType)
					           .Cast<object>()
					           .First();
				}

				return _dataGenerator.NextEnum(request.RequestedType);
			}

	        return Convention.NoValue;
        }
    }
}
