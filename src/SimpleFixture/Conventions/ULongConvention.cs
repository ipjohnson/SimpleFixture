using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

			ulong minValue = _constraintHelper.GetValue(request.Constraints, ulong.MinValue, "min", "minValue");
			ulong maxValue = _constraintHelper.GetValue(request.Constraints, ulong.MaxValue, "max", "maxValue");

			return _dataGenerator.NextULong(minValue, maxValue);
        }
    }
}
