using SimpleFixture.Impl;

namespace SimpleFixture.Conventions
{
    public class ULongConvention : SimpleTypeConvention<ulong>
    {
		private readonly IConstraintHelper _constraintHelper;
        private readonly IRandomDataGeneratorService _dataGenerator;

        public static ulong LocateValue = 5;

		public ULongConvention(IRandomDataGeneratorService dataGenerator, IConstraintHelper constraintHelper)
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

            var minMax = _constraintHelper.GetMinMax(request, ulong.MinValue, ulong.MaxValue);

            minMax.Min = _constraintHelper.GetValue(request.Constraints, minMax.Min, "min", "minValue");
			minMax.Max = _constraintHelper.GetValue(request.Constraints, minMax.Max, "max", "maxValue");

            if (minMax.Min.CompareTo(minMax.Max) > 0)
            {
                minMax.Min = minMax.Max;
            }

            return _dataGenerator.NextULong(minMax.Min, minMax.Max);
        }
    }
}
