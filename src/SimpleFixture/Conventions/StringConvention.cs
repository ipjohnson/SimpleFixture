using System.Linq;
using SimpleFixture.Impl;
using System.Reflection;

namespace SimpleFixture.Conventions
{
    /// <summary>
    /// Convention for creating string value
    /// </summary>
    public class StringConvention : SimpleTypeConvention<string>
    {
        private readonly IConstraintHelper _constraintHelper;
        private readonly IRandomDataGeneratorService _dataGenerator;

        /// <summary>
        /// value returned for locate
        /// </summary>
        public static string LocateValue = "String";

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dataGenerator"></param>
        /// <param name="constraintHelper"></param>
        public StringConvention(IRandomDataGeneratorService dataGenerator, IConstraintHelper constraintHelper)
        {
            _dataGenerator = dataGenerator;
            _constraintHelper = constraintHelper;
        }

        /// <summary>
        /// Generate data for the request, return Constrain.NoValue instead of null
        /// </summary>
        /// <param name="request">data request</param>
        /// <returns>generated data</returns>
        public override object GenerateData(DataRequest request)
        {
            if (!request.Populate)
            {
                return LocateValue;
            }
            MinMaxValue<int> lengthMinMax;

            if (request.ExtraInfo is MemberInfo)
            {
                lengthMinMax = GetMemberInfoStringLength(request.ExtraInfo as MemberInfo);

                if(lengthMinMax.Min < 0)
                {
                    lengthMinMax.Min = 5;
                }
                if(lengthMinMax.Max == int.MaxValue)
                {
                    lengthMinMax.Max = 16;
                }
            }
            else
            {
                lengthMinMax = new MinMaxValue<int> { Min = 5, Max = 16 };
            }            

            lengthMinMax.Min = _constraintHelper.GetValue(request.Constraints, lengthMinMax.Min, "min", "minlength");
            lengthMinMax.Max = _constraintHelper.GetValue(request.Constraints, lengthMinMax.Max, "max", "maxlength");
            
            var prefix = _constraintHelper.GetValue(request.Constraints, string.Empty, "prefix", "pre", "seed");

            if (!string.IsNullOrEmpty(prefix))
            {
                lengthMinMax.Min -= prefix.Length;
                lengthMinMax.Max -= prefix.Length;
            }

            var postfix = _constraintHelper.GetValue(request.Constraints, string.Empty, "postfix", "post");

            if (!string.IsNullOrEmpty(postfix))
            {
                lengthMinMax.Min -= postfix.Length;
                lengthMinMax.Max -= postfix.Length;
            }

            var stringType = _constraintHelper.GetValue(request.Constraints, StringType.MostCharacter, "stringType", "Type");

            if (lengthMinMax.Min > lengthMinMax.Max)
            {
                lengthMinMax.Min = lengthMinMax.Max;
            }

            return prefix + _dataGenerator.NextString(stringType, lengthMinMax.Min, lengthMinMax.Max) + postfix;
        }

        private MinMaxValue<int> GetMemberInfoStringLength(MemberInfo memberInfo)
        {
            var returnValue = new MinMaxValue<int> { Min = -1, Max = int.MaxValue };

            var attribute = memberInfo.GetCustomAttributes().FirstOrDefault(a => a.GetType().Name == "StringLengthAttribute");

            if(attribute != null)
            {
                var minProperty = attribute.GetType().GetRuntimeProperty("MinimumLength");
                var maxProperty = attribute.GetType().GetRuntimeProperty("MaximumLength");

                if(minProperty != null)
                { 
                    var localMin = (int)minProperty.GetValue(attribute);

                    if (localMin.CompareTo(returnValue.Min) > 0)
                    {
                        returnValue.Min = localMin;
                    }
                }

                if (maxProperty != null)
                {
                    var localMax = (int)maxProperty.GetValue(attribute);

                    if (localMax.CompareTo(returnValue.Max) < 0)
                    {
                        returnValue.Max = localMax;
                    }
                }
            }

            return returnValue;
        }
    }
}
