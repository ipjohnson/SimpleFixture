﻿using SimpleFixture.Impl;

namespace SimpleFixture.Conventions
{
    /// <summary>
    /// Convention for creating int
    /// </summary>
    public class IntConvention : SimpleTypeConvention<int>
    {
        private readonly IConstraintHelper _constraintHelper;
        private readonly IRandomDataGeneratorService _dataGenerator;

        /// <summary>
        /// Locate value for int
        /// </summary>
        public static int LocateValue = 5;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dataGenerator"></param>
        /// <param name="constraintHelper"></param>
        public IntConvention(IRandomDataGeneratorService dataGenerator, IConstraintHelper constraintHelper)
        {
            _dataGenerator = dataGenerator;
            _constraintHelper = constraintHelper;
        }

        /// <summary>
        /// Generate data for the request, return Constrain.NoValue instead of null
        /// </summary>
        /// <param name="request">data request</param>
        /// <returns>generated data</returns>
        public override object GenerateData(DataRequest request)
        {
            if (!request.Populate)
            {
                return LocateValue;
            }

            var minMax = _constraintHelper.GetMinMax(request, int.MinValue, int.MaxValue);

            minMax.Min = _constraintHelper.GetValue(request.Constraints, minMax.Min, "min", "minValue");
            minMax.Max = _constraintHelper.GetValue(request.Constraints, minMax.Max, "max", "maxValue");

            if (minMax.Min.CompareTo(minMax.Max) > 0)
            {
                minMax.Min = minMax.Max;
            }

            return _dataGenerator.NextInt(minMax.Min, minMax.Max);
        }
    }
}
