using System;
using SimpleFixture.Impl;

namespace SimpleFixture.Conventions
{
    /// <summary>
    /// Convention for creating uri
    /// </summary>
    public class UriConvention : SimpleTypeConvention<Uri>
    {
        private readonly IConstraintHelper _constraintHelper;

        /// <summary>
        /// Value returned for locate
        /// </summary>
        public static Uri LocateValue = new Uri("http://google.com");

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="constraintHelper"></param>
        public UriConvention(IConstraintHelper constraintHelper)
        {
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

            var domain = _constraintHelper.GetValue(request.Constraints, "google.com", "uriDomain");
            var scheme = _constraintHelper.GetValue(request.Constraints, "http", "uriScheme");

            return new Uri(string.Format("{0}://{1}", scheme, domain));
        }
    }
}
