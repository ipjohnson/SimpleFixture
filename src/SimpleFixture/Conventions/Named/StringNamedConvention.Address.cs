using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleFixture.Impl;

namespace SimpleFixture.Conventions.Named
{
    public partial class StringNamedConvention
    {
        protected virtual string PostalCodeConvention(DataRequest request)
        {
            return _dataGenerator.NextString(StringType.Numeric,5,5);
        }

        protected virtual string AddressLineOneConvention(DataRequest request)
        {
            return null;
        }

        protected virtual string AddressLineTwoConvention(DataRequest request)
        {
            return null;
        }

        protected virtual string CityConvention(DataRequest request)
        {
            return null;
        }

        protected virtual string StateConvention(DataRequest request)
        {
            return null;
        }

        protected virtual string CountryConvention(DataRequest request)
        {
            return null;
        }


    }
}
