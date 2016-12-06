using SimpleFixture.Impl;
using System;

namespace SimpleFixture.Conventions
{
    public class TimeSpanConvention : SimpleTypeConvention<TimeSpan>
    {
        private readonly IRandomDataGeneratorService _dataGenerator;
        private readonly IConstraintHelper _constraintHelper;
        
        public static TimeSpan LocateValue = new TimeSpan(1,1,1,1);

        public TimeSpanConvention(IRandomDataGeneratorService dataGenerator, IConstraintHelper constraintHelper)
        {
            _dataGenerator = dataGenerator;
            _constraintHelper = constraintHelper;
        }

        public override object GenerateData(DataRequest request)
        {
            if (!request.Populate)
            {
                return LocateValue;
            }

            MinMaxValue<TimeSpan> minMax = _constraintHelper.GetMinMax(request, TimeSpan.MinValue, TimeSpan.MaxValue);

            minMax.Min = _constraintHelper.GetValue(request.Constraints, minMax.Min, "min", "minValue");
            minMax.Max = _constraintHelper.GetValue(request.Constraints, minMax.Max, "max", "maxValue");

            if (minMax.Min.CompareTo(minMax.Max) > 0)
            {
                minMax.Min = minMax.Max;
            }

            return _dataGenerator.NextTimeSpan(minMax.Min, minMax.Max);
        }
    }
}
