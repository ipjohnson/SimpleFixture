using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleFixture.Impl;

namespace SimpleFixture.Conventions
{
    public class StringConvention : SimpleTypeConvention<string>
    {
        private readonly IConstraintHelper _constraintHelper;
        private readonly IRandomDataGeneratorService _dataGenerator;

        public static string LocateValue = "String";

        public StringConvention(IRandomDataGeneratorService dataGenerator, IConstraintHelper constraintHelper)
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

            int minLength = _constraintHelper.GetValue(request.Constraints, 5, "min", "minlength");
            int maxLength = _constraintHelper.GetValue(request.Constraints, 16, "max", "maxlength");

            string prefix = _constraintHelper.GetValue(request.Constraints, string.Empty, "prefix", "pre", "seed");

            if (!string.IsNullOrEmpty(prefix))
            {
                minLength -= prefix.Length;
                maxLength -= prefix.Length;
            }

            string postfix = _constraintHelper.GetValue(request.Constraints, string.Empty, "postfix", "post");

            if (!string.IsNullOrEmpty(postfix))
            {
                minLength -= postfix.Length;
                maxLength -= postfix.Length;
            }

            StringType stringType = _constraintHelper.GetValue(request.Constraints, StringType.MostCharacter, "stringType", "Type");

            return prefix + _dataGenerator.NextString(stringType, minLength, maxLength) + postfix;
        }

    }
}
