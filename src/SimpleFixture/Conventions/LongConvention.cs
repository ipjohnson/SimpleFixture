using SimpleFixture.Impl;

namespace SimpleFixture.Conventions
{
    public class LongConvention : SimpleTypeConvention<long>
    {
		private readonly IConstraintHelper _constraintHelper;
        private readonly IRandomDataGeneratorService _dataGenerator;

        public static long LocateValue = 5;

		public LongConvention(IRandomDataGeneratorService dataGenerator, IConstraintHelper constraintHelper)
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

            var minMax = _constraintHelper.GetMinMax(request, long.MinValue, long.MaxValue);

            minMax.Min = _constraintHelper.GetValue(request.Constraints, minMax.Min, "min", "minValue");
            minMax.Max = _constraintHelper.GetValue(request.Constraints, minMax.Max, "max", "maxValue");

            if (minMax.Min.CompareTo(minMax.Max) > 0)
            {
                minMax.Min = minMax.Max;
            }

            return _dataGenerator.NextLong(minMax.Min, minMax.Max);
        }
    }
}
