using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleFixture.Impl;

namespace SimpleFixture.Conventions
{
    public class BoolConvention : SimpleTypeConvention<bool>
    {
        private readonly IRandomDataGeneratorService _dataGenerator;

        public static bool LocateValue = true;

        public BoolConvention(IRandomDataGeneratorService dataGenerator)
        {
            _dataGenerator = dataGenerator;
        }
        
        public override object GenerateData(DataRequest request)
        {
            if (!request.Populate)
            {
                return LocateValue;
            }

            return _dataGenerator.NextBool();
        }
    }
}
