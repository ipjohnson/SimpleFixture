using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleFixture.Impl;

namespace SimpleFixture.Conventions
{
    public class IntConvention : SimpleTypeConvention<int>
    {
        private readonly IConstraintHelper _constraintHelper;
        private readonly IRandomDataGeneratorService _dataGenerator;

        public static int LocateValue = 5;

        public IntConvention(IRandomDataGeneratorService dataGenerator, IConstraintHelper constraintHelper)
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

            int minValue = _constraintHelper.GetValue(request.Constraints, int.MinValue, "min", "minValue");
            int maxValue = _constraintHelper.GetValue(request.Constraints, int.MaxValue, "max", "maxValue");

            return _dataGenerator.NextInt(minValue, maxValue);
        }
    }
}
