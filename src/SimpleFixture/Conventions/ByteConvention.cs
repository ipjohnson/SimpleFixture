using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleFixture.Impl;

namespace SimpleFixture.Conventions
{
    public class ByteConvention : SimpleTypeConvention<byte>
    {
        private readonly IRandomDataGeneratorService _dataGenerator;
        private readonly IConstraintHelper _helper;

        public static byte LocateValue = 5;

        public ByteConvention(IRandomDataGeneratorService dataGenerator, IConstraintHelper constraintHelper)
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

            byte minValue = _helper.GetValue(request.Constraints, byte.MinValue, "min", "minvalue");
            byte maxValue = _helper.GetValue(request.Constraints, byte.MaxValue, "max", "maxvalue");

            return _dataGenerator.NextByte(minValue, maxValue);
        }
    }
}
