using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleFixture.Impl;

namespace SimpleFixture.Conventions
{
    public class UShortConvention : SimpleTypeConvention<ushort>
    {
        private readonly IConstraintHelper _constraintHelper;
        private readonly IRandomDataGeneratorService _dataGenerator;

        public static ushort LocateValue = 5;

		public UShortConvention(IRandomDataGeneratorService dataGenerator, IConstraintHelper constraintHelper)
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

			ushort minValue = _constraintHelper.GetValue(request.Constraints, ushort.MinValue, "min", "minValue");
			ushort maxValue = _constraintHelper.GetValue(request.Constraints, ushort.MaxValue, "max", "maxValue");

            return _dataGenerator.NextUShort(minValue, maxValue);
        }
    }
}
