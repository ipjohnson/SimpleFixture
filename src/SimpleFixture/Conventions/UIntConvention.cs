using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleFixture.Impl;

namespace SimpleFixture.Conventions
{
    public class UIntConvention : SimpleTypeConvention<uint>
    {
        private readonly IConstraintHelper _constraintHelper;
        private readonly IRandomDataGeneratorService _dataGenerator;

        public static uint LocateValue = 5;

		public UIntConvention(IRandomDataGeneratorService dataGenerator, IConstraintHelper constraintHelper)
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

            uint minValue = _constraintHelper.GetValue(request.Constraints, uint.MinValue, "min", "minValue");
            uint maxValue = _constraintHelper.GetValue(request.Constraints, uint.MaxValue, "max", "maxValue");

            MinMaxValue<uint> minMax = _constraintHelper.GetMinMax(request, minValue, maxValue);

            return _dataGenerator.NextUInt(minMax.Min, minMax.Max);
        }
    }
}
