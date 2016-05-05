using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

			long minValue = _constraintHelper.GetValue(request.Constraints, long.MinValue, "min", "minValue");
			long maxValue = _constraintHelper.GetValue(request.Constraints, long.MaxValue, "max", "maxValue");

            MinMaxValue<long> minMax = _constraintHelper.GetMinMax(request, minValue, maxValue);

            return _dataGenerator.NextLong(minMax.Min, minMax.Max);
        }
    }
}
