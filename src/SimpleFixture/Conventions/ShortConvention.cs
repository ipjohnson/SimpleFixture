using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleFixture.Impl;

namespace SimpleFixture.Conventions
{
    public class ShortConvention : SimpleTypeConvention<short>
    {
        private readonly IConstraintHelper _constraintHelper;
        private readonly IRandomDataGeneratorService _dataGenerator;

        public static short LocateValue = 5;

		public ShortConvention(IRandomDataGeneratorService dataGenerator, IConstraintHelper constraintHelper)
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

			short minValue = _constraintHelper.GetValue(request.Constraints, short.MinValue, "min", "minValue");
			short maxValue = _constraintHelper.GetValue(request.Constraints, short.MaxValue, "max", "maxValue");

            return _dataGenerator.NextShort(minValue, maxValue);
        }
    }
}
