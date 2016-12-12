using System;
using SimpleFixture.Impl;

namespace SimpleFixture.Conventions
{
    /// <summary>
    /// Generate random type
    /// </summary>
    public class TypeConvention : SimpleTypeConvention<Type>
    {
        private readonly IRandomDataGeneratorService _dataGenerator;
        private readonly Type[] _types = { typeof(bool), typeof(short), typeof(int), typeof(double), typeof(string), typeof(Fixture) };

        /// <summary>
        /// Type returned for locate
        /// </summary>
        public static readonly Type LocateType = typeof(int);

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dataGenerator"></param>
        public TypeConvention(IRandomDataGeneratorService dataGenerator)
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
                return LocateType;
            }

            return _dataGenerator.NextInSet(_types);
        }
    }
}
