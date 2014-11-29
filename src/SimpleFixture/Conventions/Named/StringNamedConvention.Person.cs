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
        #region FirstName
        protected List<string> _boyNames;
        protected List<string> _girlNames;

        protected virtual string FirstNameConvention(DataRequest request)
        {
            if (_boyNames == null)
            {
                InitializeFirstNames();
            }

            string sex = _helper.GetValue(request.Constraints, "sex", "gender");

            bool isGirl = false;

            switch (sex.ToLowerInvariant())
            {
                case "m":
                case "male":
                case "b":
                case "boy":
                    isGirl = false;
                    break;

                case "f":
                case "female":
                case "g":
                case "girl":
                    isGirl = true;
                    break;

                default:
                    isGirl = _dataGenerator.NextBool();
                    break;
            }

            return isGirl ?
                   _dataGenerator.NextInSet(_girlNames) :
                   _dataGenerator.NextInSet(_boyNames);
        }

        protected virtual void InitializeFirstNames()
        {
            _girlNames = new List<string>{
                                                 "Sophia",
                                                 "Emma",
                                                 "Olivia",
                                                 "Isabella",
                                                 "Mia",
                                                 "Zoey",
                                                 "Emily",
                                                 "Chloe",
                                                 "Sara",
                                                 "Tanya",
                                                 "Jennifer",
                                                 "Kate",
                                                 "Kerry",
                                                 "Kim",
                                                 "Kathy",
                                                 "Glenda",
                                                 "Hannah",
                                                 "Madison",
                                                 "Devin",
                                                 "Kelsey",
                                                 "Lila",
                                                 "Lindsey",
                                                 "Sophie",
                                                 "Grace",
                                                 "Brooklyn",
                                                 "Melissa",
                                                 "Buffy",
                                                 "Faith",
                                                 "Glory",
                                                 "Willow",
                                                 "Lana",
                                                 "Pam",
                                                 "Cheryl",
                                                 "Carol",
                                                 "Malory",
                                                 "Anya",
                                                 "Dawn",
                                                 "Ginger",
                                                 "Chastity",
                                                 "Destiny",
                                                 "Amber",
                                                 "Juilet"
                                             };

            _boyNames = new List<string>{
                                            "Jim",
                                            "Jake",
                                            "Johnny",
                                            "Jose",
                                            "Ian",
                                            "Kevin",
                                            "Nate",
                                            "Brett",
                                            "Dennis",
                                            "Donald",
                                            "Douglas",
                                            "David",
                                            "Vernon",
                                            "Shane",
                                            "Shawn",
                                            "Burton",
                                            "Dean",
                                            "Sam",
                                            "Sterling",
                                            "Cyril",
                                            "Xander",
                                            "Rupert",
                                            "Spike",
                                            "Angel"
                                          };

        }
        #endregion

        #region LastName

        protected List<string> _lastNames;

        protected virtual string LastNameConvention(DataRequest request)
        {
            if (_lastNames == null)
            {
                InitalizeLastNames();
            }

            return _dataGenerator.NextInSet(_lastNames);
        }

        protected virtual void InitalizeLastNames()
        {
            _lastNames = new List<string>
										{
											"Johnson",
											"Ray",
											"Archer",
											"Summers",
											"Spencer",
											"Morgan",
											"Freeman",
											"OHara",
											"Guster",
											"Vick",
											"Giles",
											"Archer",
											"Kane",
											"Figgis",
											"McDonald",
											"McNabb",
											"Winchester",
											"Despereaux",
											"Tunt",
											"Krieger",
											"Gillette",
											"Jackov",
											"Haris",
											"Rosenberg",
											"Meers",
											"Wilkins",
											"Wood"
										};
        }

        #endregion

        #region Username

        protected virtual string UsernameConvention(DataRequest request)
        {
            string firstName = _helper.GetValue<string>(request.Constraints, null, "FirstName");
            string lastName = _helper.GetValue<string>(request.Constraints, null, "LastName", "Surname");

            if (string.IsNullOrEmpty(firstName))
            {
                firstName = FirstNameConvention(request);
            }

            if (string.IsNullOrEmpty(lastName))
            {
                lastName = LastNameConvention(request);
            }

            return string.Format("{0}{1}", firstName[0], lastName);
        }

        #endregion

        #region Password

        protected virtual string PasswordConvention(DataRequest request)
        {
            string retunValue = null;

            retunValue += _dataGenerator.NextT('!', '@', '#', '$', '.', '-', '=', '+');

            retunValue += _dataGenerator.NextChar('A', 'Z');

            retunValue += _dataGenerator.NextChar('a', 'z');

            retunValue += _dataGenerator.NextChar('0', '9');

            retunValue += _dataGenerator.NextString(StringType.AlphaNumeric);

            retunValue = new string(_dataGenerator.Randomize(retunValue.ToCharArray()).ToArray());

            return retunValue;
        }
        #endregion

        #region Government Id

        protected virtual string GovernementIdConvention(DataRequest request)
        {
            return string.Format("{0}-{1}-{2}",
                                 _dataGenerator.NextString(StringType.Numeric, 3, 3),
                                 _dataGenerator.NextString(StringType.Numeric, 2, 2),
                                 _dataGenerator.NextString(StringType.Numeric, 4, 4));
        }

        #endregion

        protected virtual string EmailConvention(DataRequest request)
        {
            string firstName = _helper.GetValue<string>(request.Constraints, null, "FirstName");
            string lastName = _helper.GetValue<string>(request.Constraints, null, "LastName", "Surname");
            string domain = _helper.GetValue<string>(request.Constraints, "none.com", "Domain");

            if (string.IsNullOrEmpty(firstName))
            {
                firstName = FirstNameConvention(request);
            }

            if (string.IsNullOrEmpty(lastName))
            {
                lastName = LastNameConvention(request);
            }

            return string.Format("{0}.{1}@{2}", firstName, lastName, domain);
        }

        protected virtual string PhoneNumberConvention(DataRequest arg)
        {
            return string.Format("{0}-{1}-{2}", _dataGenerator.NextString(StringType.Numeric, 3, 3),
                                                _dataGenerator.NextString(StringType.Numeric, 3, 3),
                                                _dataGenerator.NextString(StringType.Numeric, 4, 4));
        }
    }
}
