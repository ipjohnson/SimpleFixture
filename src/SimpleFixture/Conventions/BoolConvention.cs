using SimpleFixture.Impl;

namespace SimpleFixture.Conventions
{
    /// <summary>
    /// convention for creating bool
    /// </summary>
    public class BoolConvention : SimpleTypeConvention<bool>
    {
        private readonly IRandomDataGeneratorService _dataGenerator;

        /// <summary>
        /// Value return for locate
        /// </summary>
        public static bool LocateValue = true;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dataGenerator"></param>
        public BoolConvention(IRandomDataGeneratorService dataGenerator)
        {
            _dataGenerator = dataGenerator;
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

            return _dataGenerator.NextBool();
        }
    }
}
