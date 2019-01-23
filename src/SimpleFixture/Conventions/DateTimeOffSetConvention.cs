using System;
using System.Collections.Generic;
using System.Text;
using SimpleFixture.Impl;

namespace SimpleFixture.Conventions
{
    /// <summary>
    /// Convention for creating DateTimeOffset
    /// </summary>
    public class DateTimeOffsetConvention : SimpleTypeConvention<DateTimeOffset>
    {
        private readonly IRandomDataGeneratorService _dataGenerator;
        private readonly IConstraintHelper _helper;

        /// <summary>
        /// Value returned for locate
        /// </summary>
        public static DateTimeOffset LocateValue = new DateTime(1970, 1, 1);

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dataGenerator"></param>
        /// <param name="constraintHelper"></param>
        public DateTimeOffsetConvention(IRandomDataGeneratorService dataGenerator, IConstraintHelper constraintHelper)
        {
            _dataGenerator = dataGenerator;
            _helper = constraintHelper;
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

            var min = _helper.GetValue<DateTimeOffset?>(request.Constraints, null, "min", "minDateTimeOffset");
            var max = _helper.GetValue<DateTimeOffset?>(request.Constraints, null, "max", "maxDateTimeOffset");

            if (!min.HasValue)
            {
                if (!max.HasValue)
                {
                    min = DateTimeOffset.Now.AddYears(-5);
                    max = min.Value.AddYears(10);
                }
                else
                {
                    min = max.Value.AddYears(-100);
                }
            }
            else if (!max.HasValue)
            {
                max = min.Value.AddYears(100);
            }

            if (min.Value.CompareTo(max.Value) > 0)
            {
                min = max;
            }

            var minMax = _helper.GetMinMax(request, min.Value, max.Value);

            var timeSpan = minMax.Max.Subtract(minMax.Min);

            return minMax.Min.AddSeconds(_dataGenerator.NextDouble(0, timeSpan.TotalSeconds));
        }
    }
}
