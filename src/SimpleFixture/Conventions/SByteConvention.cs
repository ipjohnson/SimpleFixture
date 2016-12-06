using SimpleFixture.Impl;

namespace SimpleFixture.Conventions
{
    public class SByteConvention : SimpleTypeConvention<sbyte>
    {
        private readonly IRandomDataGeneratorService _dataGenerator;
        private readonly IConstraintHelper _constraintHelper;

        public static sbyte LocateValue = 5;

		public SByteConvention(IRandomDataGeneratorService dataGenerator, IConstraintHelper constraintHelper)
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

            MinMaxValue<sbyte> minMax = _constraintHelper.GetMinMax(request, sbyte.MinValue, sbyte.MaxValue);

            minMax.Min = _constraintHelper.GetValue(request.Constraints, minMax.Min, "min", "minValue");
            minMax.Max = _constraintHelper.GetValue(request.Constraints, minMax.Max, "max", "maxValue");

            if (minMax.Min.CompareTo(minMax.Max) > 0)
            {
                minMax.Min = minMax.Max;
            }

            return _dataGenerator.NextSByte(minMax.Min, minMax.Max);
        }
    }
}
