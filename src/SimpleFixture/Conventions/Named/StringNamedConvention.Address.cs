using System;
using System.Collections.Generic;
using SimpleFixture.Impl;

namespace SimpleFixture.Conventions.Named
{
    public partial class StringNamedConvention
    {
        private List<string> _streetNames;
        private List<string> _streetPrefix;
        private List<string> _streetPostfix;
        private List<string> _cityNames;
        private List<Tuple<string, string>> _states;

        protected virtual string PostalCodeConvention(DataRequest request)
        {
            return _dataGenerator.NextString(StringType.Numeric, 5, 5);
        }

        protected virtual string AddressLineOneConvention(DataRequest request)
        {
            return string.Format("{0} {1} {2} {3}", _dataGenerator.NextInt(1, 10000),
                                                    StreetPrefix(request),
                                                    StreetName(request),
                                                    StreetPostfix(request));
        }

        protected virtual string AddressLineTwoConvention(DataRequest request)
        {
            string typeOfLocation = _dataGenerator.NextBool() ? "Unit" : "Apt";

            return string.Format("{0} {1}", typeOfLocation, _dataGenerator.NextInt(1, 10000));
        }

        protected virtual string StreetName(DataRequest request)
        {
            if (_streetNames == null)
            {
                InitalizeStreetNames();
            }

            return _dataGenerator.NextInSet(_streetNames);
        }

        private void InitalizeStreetNames()
        {
            _streetNames = new List<string>
                           {
                                "First",
                                "Second",
                                "Third",
                                "Fourth",
                                "Fifth",
                                "Sixth",
                                "Seventh",
                                "Eighth",
                                "Nineth",
                                "Tenth",
                                "Oak",
                                "Pine",
                                "Maple",
                                "Cedar",
                                "Spruce",
                                "Lake",
                                "Hill",
                                "Pond",
                                "Court",
                                "View",
                                "Church",
                                "Washington",
                                "Main",
                                "Broad",
                                "Center",
                                "Union",
                                "Prospect",
                                "Highland"
                           };
        }

        protected virtual string StreetPrefix(DataRequest request)
        {
            if (_streetPrefix == null)
            {
                _streetPrefix = new List<string>
                                {
                                    "N",
                                    "S",
                                    "E",
                                    "W",
                                    "NE",
                                    "NW",
                                    "SE",
                                    "SW"
                                };
            }

            return _dataGenerator.NextInSet(_streetPrefix);
        }

        protected virtual string StreetPostfix(DataRequest request)
        {
            if (_streetPostfix == null)
            {
                InitalizeStreetPostfixs();
            }
            return _dataGenerator.NextInSet(_streetPostfix);
        }

        private void InitalizeStreetPostfixs()
        {
            _streetPostfix = new List<string>
                             {
                                "ALY",
                                "AVE",
                                "BRK",
                                "BYP",
                                "DR",
                                "GRV",
                                "LN",
                                "PKY",
                                "RD",
                                "ST",
                                "TPKE"
                             };
        }

        protected virtual string CityConvention(DataRequest request)
        {
            if (_cityNames == null)
            {
                InitalizeCityNames();
            }

            return _dataGenerator.NextInSet(_cityNames);
        }

        private void InitalizeCityNames()
        {
            _cityNames = new List<string>
                         {
                             "Washington",
                             "Franklin",
                             "Greenville",
                             "Bristol",
                             "Clinton",
                             "Springfield",
                             "Fairview",
                             "Salem",
                             "Madison",
                             "Georgetown",
                             "Ashland",
                             "Oxford",
                             "Arlington",
                             "Jackson",
                             "Burlington",
                             "Manchester",
                             "Centerville",
                             "Clayton",
                             "SunnyDale",
                             "Los Angeles",
                             "Santa Barbara",
                             "New York",
                             "Miami",
                             "Lidsville",
                             "Boston",
                             "Cleveland",
                             "Potsdam",
                             "Denver",
                             "Hampton",
                             "Lebanon",
                             "Shrewsberry"
                         };
        }

        protected virtual string StateConvention(DataRequest request)
        {
            if (_states == null)
            {
                InitalizeStates();
            }

            return _dataGenerator.NextInSet(_states).Item1;
        }

        protected virtual string StateAbbreviation(DataRequest request)
        {
            if (_states == null)
            {
                InitalizeStates();
            }

            return _dataGenerator.NextInSet(_states).Item2;
        }

        private void InitalizeStates()
        {
            _states = new List<Tuple<string, string>>
                      {
                            new Tuple<string, string>("ALABAMA","AL"),
                            new Tuple<string, string>("ALASKA","AK"),
                            new Tuple<string, string>("ARIZONA","AZ"),
                            new Tuple<string, string>("ARKANSAS","AR"),
                            new Tuple<string, string>("CALIFORNIA","CA"),
                            new Tuple<string, string>("COLORADO","CO"),
                            new Tuple<string, string>("CONNECTICUT","CT"),
                            new Tuple<string, string>("DELAWARE","DE"),
                            new Tuple<string, string>("FLORIDA","FL"),
                            new Tuple<string, string>("GEORGIA","GA"),
                            new Tuple<string, string>("HAWAII","HI"),
                            new Tuple<string, string>("IDAHO","ID"),
                            new Tuple<string, string>("ILLINOIS","IL"),
                            new Tuple<string, string>("INDIANA","IN"),
                            new Tuple<string, string>("IOWA","IA"),
                            new Tuple<string, string>("KANSAS","KS"),
                            new Tuple<string, string>("KENTUCKY","KY"),
                            new Tuple<string, string>("LOUISIANA","LA"),
                            new Tuple<string, string>("MAINE","ME"),
                            new Tuple<string, string>("MARYLAND","MD"),
                            new Tuple<string, string>("MASSACHUSETTS","MA"),
                            new Tuple<string, string>("MICHIGAN","MI"),
                            new Tuple<string, string>("MINNESOTA","MN"),
                            new Tuple<string, string>("MISSISSIPPI","MS"),
                            new Tuple<string, string>("MISSOURI","MO"),
                            new Tuple<string, string>("MONTANA","MT"),
                            new Tuple<string, string>("NEBRASKA","NE"),
                            new Tuple<string, string>("NEVADA","NV"),
                            new Tuple<string, string>("NEW HAMPSHIRE","NH"),
                            new Tuple<string, string>("NEW JERSEY","NJ"),
                            new Tuple<string, string>("NEW MEXICO","NM"),
                            new Tuple<string, string>("NEW YORK","NY"),
                            new Tuple<string, string>("NORTH CAROLINA","NC"),
                            new Tuple<string, string>("NORTH DAKOTA","ND"),
                            new Tuple<string, string>("OHIO","OH"),
                            new Tuple<string, string>("OKLAHOMA","OK"),
                            new Tuple<string, string>("OREGON","OR"),
                            new Tuple<string, string>("PENNSYLVANIA","PA"),
                            new Tuple<string, string>("RHODE ISLAND","RI"),
                            new Tuple<string, string>("SOUTH CAROLINA","SC"),
                            new Tuple<string, string>("SOUTH DAKOTA","SD"),
                            new Tuple<string, string>("TENNESSEE","TN"),
                            new Tuple<string, string>("TEXAS","TX"),
                            new Tuple<string, string>("UTAH","UT"),
                            new Tuple<string, string>("VERMONT","VT"),
                            new Tuple<string, string>("VIRGINIA","VA"),
                            new Tuple<string, string>("WASHINGTON","WA"),
                            new Tuple<string, string>("WEST VIRGINIA","WV"),
                            new Tuple<string, string>("WISCONSIN","WI"),
                            new Tuple<string, string>("WYOMING","WY")
                                          
                      };
        }

        protected virtual string CountryConvention(DataRequest request)
        {
            return "United States";
        }
    }
}
