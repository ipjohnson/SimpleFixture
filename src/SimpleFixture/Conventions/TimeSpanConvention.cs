using SimpleFixture.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFixture.Conventions
{
    public class TimeSpanConvention : SimpleTypeConvention<TimeSpan>
    {
        private readonly IRandomDataGeneratorService _dataGenerator;
        private readonly IConstraintHelper _helper;
        
        public static TimeSpan LocateValue = new TimeSpan(1,1,1,1);

        public TimeSpanConvention(IRandomDataGeneratorService dataGenerator, IConstraintHelper constraintHelper)
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

            TimeSpan min = _helper.GetValue(request.Constraints, TimeSpan.MinValue, "min", "minValue");
            TimeSpan max = _helper.GetValue(request.Constraints, TimeSpan.MaxValue, "max", "maxValue");

            return _dataGenerator.NextTimeSpan(min, max);
        }
    }
}
