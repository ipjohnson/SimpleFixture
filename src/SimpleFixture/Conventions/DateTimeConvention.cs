using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleFixture.Impl;

namespace SimpleFixture.Conventions
{
    public class DateTimeConvention : SimpleTypeConvention<DateTime>
    {
        private readonly IRandomDataGeneratorService _dataGenerator;
        private readonly IConstraintHelper _helper;

        public static DateTime LocateValue = new DateTime(1970,1,1);

        public DateTimeConvention(IRandomDataGeneratorService dataGenerator, IConstraintHelper constraintHelper)
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

            DateTime? min = _helper.GetValue<DateTime?>(request.Constraints, null, "min", "minDate");
            DateTime? max = _helper.GetValue<DateTime?>(request.Constraints, null, "max", "maxDate");

            if (!min.HasValue)
            {
                if (!max.HasValue)
                {
                    min = DateTime.Today.AddYears(-5);
                    max = min.Value.AddYears(10);
                }
                else
                {
                    min = max.Value.AddYears(-100);
                }
            }
            else if(!max.HasValue)
            {
                max = min.Value.AddYears(100);
            }
            
            MinMaxValue<DateTime> minMax = _helper.GetMinMax(request, min.Value, max.Value);

            var timeSpan = minMax.Max.Subtract(minMax.Min);

            return minMax.Min.AddSeconds(_dataGenerator.NextDouble(0, timeSpan.TotalSeconds));
        }
    }
}
