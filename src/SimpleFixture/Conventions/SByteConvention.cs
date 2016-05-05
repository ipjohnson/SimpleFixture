using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleFixture.Impl;

namespace SimpleFixture.Conventions
{
    public class SByteConvention : SimpleTypeConvention<sbyte>
    {
        private readonly IRandomDataGeneratorService _dataGenerator;
        private readonly IConstraintHelper _helper;

        public static sbyte LocateValue = 5;

		public SByteConvention(IRandomDataGeneratorService dataGenerator, IConstraintHelper constraintHelper)
        {
            _dataGenerator = dataGenerator;
            _helper = constraintHelper;
        }

        public override object GenerateData(DataRequest request)
        {
            if (!request.Populate)
            {
                return LocateValue;
            }

            sbyte minValue = _helper.GetValue(request.Constraints, sbyte.MinValue, "min", "minvalue");
            sbyte maxValue = _helper.GetValue(request.Constraints, sbyte.MaxValue, "max", "maxvalue");

            MinMaxValue<sbyte> minMax = _helper.GetMinMax(request, minValue, maxValue);

            return _dataGenerator.NextSByte(minMax.Min, minMax.Max);
        }
    }
}
