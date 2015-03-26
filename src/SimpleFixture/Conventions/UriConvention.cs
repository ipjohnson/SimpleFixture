using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleFixture.Impl;

namespace SimpleFixture.Conventions
{
    public class UriConvention : SimpleTypeConvention<Uri>
    {
        public static Uri LocateValue = new Uri("http://google.com");

        private readonly IConstraintHelper _constraintHelper;

        public UriConvention(IConstraintHelper constraintHelper)
        {
            _constraintHelper = constraintHelper;
        }

        public override object GenerateData(DataRequest request)
        {
            if (!request.Populate)
            {
                return LocateValue;
            }

            string domain = _constraintHelper.GetValue(request.Constraints, "google.com", "uriDomain");
            string scheme = _constraintHelper.GetValue(request.Constraints, "http", "uriScheme");

            return new Uri(string.Format("{0}://{1}", scheme, domain));
        }
    }
}
