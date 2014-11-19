using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleFixture.Impl;

namespace SimpleFixture.Conventions
{
    public class CharConvention : SimpleTypeConvention<char>
    {
        private readonly IRandomDataGeneratorService _dataGenerator;
        private readonly IConstraintHelper _helper;

        public static char LocateValue = 'C';

        public CharConvention(IRandomDataGeneratorService dataGenerator, IConstraintHelper constraintHelper)
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

            char minValue = _helper.GetValue(request.Constraints, char.MinValue, "min", "minvalue");
            char maxValue = _helper.GetValue(request.Constraints, char.MaxValue, "max", "maxvalue");

            return _dataGenerator.NextChar(minValue, maxValue);
        }
    }
}
