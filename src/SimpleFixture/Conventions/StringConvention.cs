using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleFixture.Impl;
using System.Reflection;

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
            MinMaxValue<int> lengthMinMax = new MinMaxValue<int> { Max = maxLength, Min = minLength };

            if (request.ExtraInfo is MemberInfo)
            {
                lengthMinMax = GetMemberInfoStringLength(request.ExtraInfo as MemberInfo,minLength, maxLength );
            }

            string prefix = _constraintHelper.GetValue(request.Constraints, string.Empty, "prefix", "pre", "seed");

            if (!string.IsNullOrEmpty(prefix))
            {
                lengthMinMax.Min -= prefix.Length;
                lengthMinMax.Max -= prefix.Length;
            }

            string postfix = _constraintHelper.GetValue(request.Constraints, string.Empty, "postfix", "post");

            if (!string.IsNullOrEmpty(postfix))
            {
                lengthMinMax.Min -= postfix.Length;
                lengthMinMax.Max -= postfix.Length;
            }

            StringType stringType = _constraintHelper.GetValue(request.Constraints, StringType.MostCharacter, "stringType", "Type");

            return prefix + _dataGenerator.NextString(stringType, minLength, maxLength) + postfix;
        }

        private MinMaxValue<int> GetMemberInfoStringLength(MemberInfo memberInfo, int minLength, int maxLength)
        {
            MinMaxValue<int> returnValue = new MinMaxValue<int> { Min = minLength, Max = maxLength };

            var attribute = memberInfo.GetCustomAttributes().FirstOrDefault(a => a.GetType().Name == "StringLengthAttribute");

            if(attribute != null)
            {
                var minProperty = attribute.GetType().GetRuntimeProperty("MinimumLength");
                var maxProperty = attribute.GetType().GetRuntimeProperty("MaximumLength");

                int localMin = (int)minProperty.GetValue(attribute);
                int localMax = (int)maxProperty.GetValue(attribute);

                if (localMax.CompareTo(returnValue.Max) < 0)
                {
                    returnValue.Max = localMax;
                }

                if (localMin.CompareTo(returnValue.Min) > 0)
                {
                    returnValue.Min = localMin;
                }
            }

            return returnValue;
        }
    }
}
