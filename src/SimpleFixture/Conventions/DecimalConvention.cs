using SimpleFixture.Impl;

namespace SimpleFixture.Conventions
{
    public class DecimalConvention : SimpleTypeConvention<decimal>
    {
        private readonly IRandomDataGeneratorService _dataGenerator;
        private readonly IConstraintHelper _constraintHelper;

        public static decimal LocateValue = 5;

        public DecimalConvention(IRandomDataGeneratorService dataGenerator, IConstraintHelper constraintHelper)
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

            MinMaxValue<decimal> minMax = _constraintHelper.GetMinMax(request, decimal.MinValue, decimal.MaxValue);

            minMax.Min = _constraintHelper.GetValue(request.Constraints, minMax.Min, "min", "minValue");
            minMax.Max = _constraintHelper.GetValue(request.Constraints, minMax.Max, "max", "maxValue");

            if (minMax.Min.CompareTo(minMax.Max) > 0)
            {
                minMax.Min = minMax.Max;
            }

            return _dataGenerator.NextDecimal(minMax.Min, minMax.Max);
        }
    }
}
