using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleFixture.Impl;

namespace SimpleFixture.Conventions
{
    public class DecimalConvention : SimpleTypeConvention<decimal>
    {
        private readonly IRandomDataGeneratorService _dataGenerator;
        private readonly IConstraintHelper _helper;

        public static decimal LocateValue = 5;

        public DecimalConvention(IRandomDataGeneratorService dataGenerator, IConstraintHelper constraintHelper)
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

            decimal min = _helper.GetValue(request.Constraints, decimal.MinValue, "min", "minValue");
            decimal max = _helper.GetValue(request.Constraints, decimal.MaxValue, "max", "maxValue");

            MinMaxValue<decimal> minMax = _helper.GetMinMax(request, min, max);

            return _dataGenerator.NextDecimal(minMax.Min, minMax.Max);
        }
    }
}
