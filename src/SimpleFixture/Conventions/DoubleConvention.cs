using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleFixture.Impl;

namespace SimpleFixture.Conventions
{
    public class DoubleConvention : SimpleTypeConvention<double>
    {
        private readonly IRandomDataGeneratorService _dataGenerator;
        private readonly IConstraintHelper _helper;

        public static double LocateValue = 5;

        public DoubleConvention(IRandomDataGeneratorService dataGenerator, IConstraintHelper constraintHelper)
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

            double min = _helper.GetValue(request.Constraints, double.MinValue, "min", "minValue");
            double max = _helper.GetValue(request.Constraints, double.MaxValue, "max", "maxValue");

            MinMaxValue<double> minMax = _helper.GetMinMax(request, min, max);

            return _dataGenerator.NextDouble(minMax.Min, minMax.Max);
        }
    }
}
