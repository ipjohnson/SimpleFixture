using System;
using System.Collections.Generic;
using System.Linq;
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
                                                 "Abbie",
                                                 "Ali",
                                                 "Angela",
                                                 "Annie",
                                                 "Anya",
                                                 "Amber",
                                                 "Bella",
                                                 "Beth",
                                                 "Bianca",
                                                 "Blaire",
                                                 "Brooklyn",
                                                 "Buffy",
                                                 "Camilla",
                                                 "Carol",
                                                 "Cathleen",
                                                 "Chastity",
                                                 "Cheryl",
                                                 "Chloe",
                                                 "Clarie",
                                                 "Dani",
                                                 "Darcy",
                                                 "Dawn",
                                                 "Debbie",
                                                 "Destiny",
                                                 "Devin",
                                                 "Elly",
                                                 "Ember",
                                                 "Emily",
                                                 "Emma",
                                                 "Faith",
                                                 "Felicity",
                                                 "Fiona",
                                                 "Freda",
                                                 "Fred",
                                                 "Gabby",
                                                 "Ginger",
                                                 "Glenda",
                                                 "Glory",
                                                 "Grace",
                                                 "Haliegh",
                                                 "Hannah",
                                                 "Harmony",
                                                 "Hayden",
                                                 "Indigo",
                                                 "Irene",
                                                 "Irina",
                                                 "Iris",
                                                 "Isabella",
                                                 "Jada",
                                                 "Jade",
                                                 "Jennifer",
                                                 "Juilet",
                                                 "Kate",
                                                 "Kathy",
                                                 "Kelsey",
                                                 "Kerry",
                                                 "Kim",
                                                 "Lacy",
                                                 "Lindsey",
                                                 "Lila",
                                                 "Lily",
                                                 "Liz",
                                                 "Lana",
                                                 "Madison",
                                                 "Malory",
                                                 "Melissa",
                                                 "Mia",
                                                 "Olga",
                                                 "Olivia",
                                                 "Pam",
                                                 "Paris",
                                                 "Penny",
                                                 "Quinn",
                                                 "Rachelle",
                                                 "Raven",
                                                 "Rayna",
                                                 "Roxanne",
                                                 "Sade",
                                                 "Sara",
                                                 "Sophia",
                                                 "Sophie",
                                                 "Tanya",
                                                 "Taylor",
                                                 "Teegan",
                                                 "Veronica",
                                                 "Vickie",
                                                 "Victoria",
                                                 "Virginia",
                                                 "Wanda",
                                                 "Wendy",
                                                 "Willow",
                                                 "Xena",
                                                 "Yazmin",
                                                 "Zoey",
                                                 "Zuri"
                                             };

            _boyNames = new List<string>{
                                            "Abel",
                                            "Ace",
                                            "Al",
                                            "Ando",
                                            "Angel",
                                            "Ben",
                                            "Bert",
                                            "Billy",
                                            "Bob",
                                            "Brett",
                                            "Burton",
                                            "Caesar",
                                            "Caleb",
                                            "Carlos",
                                            "Charles",
                                            "Cyril",
                                            "David",
                                            "Dean",
                                            "Dennis",
                                            "Donald",
                                            "Douglas",
                                            "Eddie",
                                            "Elmer",
                                            "Eric",
                                            "Evan",
                                            "Fabio",
                                            "Felix",
                                            "Floyd",
                                            "Fred",
                                            "Gabe",
                                            "Gary",
                                            "Gavin",
                                            "Gerry",
                                            "Greg",
                                            "Hal",
                                            "Hans",
                                            "Henry",
                                            "Hiro",
                                            "Hugo",
                                            "Ian",
                                            "Igor",
                                            "Irvin",
                                            "Isaac",
                                            "Ivan",
                                            "Jake",
                                            "Jim",
                                            "Johnny",
                                            "Jose",
                                            "Kaleb",
                                            "Karl",
                                            "Keenan",
                                            "Kevin",
                                            "Larry",
                                            "Lester",
                                            "Levi",
                                            "Lief",
                                            "Masi",
                                            "Matt",
                                            "Milo",
                                            "Mohinder",
                                            "Nate",
                                            "Neal",
                                            "Ned",
                                            "Nicky",
                                            "Niles",
                                            "Noah",
                                            "Omar",
                                            "Oak",
                                            "Oliver",
                                            "Owen",
                                            "Ozzy",
                                            "Paul",
                                            "Patrick",
                                            "Pedro",
                                            "Peter",
                                            "Phillip",
                                            "Quinn",
                                            "Ralph",
                                            "Ray",
                                            "Raheem",
                                            "Ramon",
                                            "Rupert",
                                            "Sam",
                                            "Sasha",
                                            "Shane",
                                            "Shawn",
                                            "Spike",
                                            "Sterling",
                                            "Steve",
                                            "Tad",
                                            "Tanner",
                                            "Ted",
                                            "Tim",
                                            "Todd",
                                            "Tom",
                                            "Tony",
                                            "Tray",
                                            "Umar",
                                            "Utah",
                                            "Van",
                                            "Vernon",
                                            "Victor",
                                            "Vidal",
                                            "Vin",
                                            "Vlad",
                                            "Xander",
                                            "Xavier",
                                            "Wade",
                                            "Wally",
                                            "Wes",
                                            "William",
                                            "Wolfgang",
                                            "York",
                                            "Zach",
                                            "Zain",
                                            "Zeke",
                                            "Zoltan",
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
                                            "Abbot",
                                            "Aiken",
                                            "Allen",
											"Archer", 
                                            "Ayers",
                                            "Bennet",
                                            "Blount",
                                            "Boyce",
                                            "Brady",
                                            "Brown",
                                            "Browner",
                                            "Butler",
                                            "Cannon",
                                            "Chung",
                                            "Collins",
                                            "Connoly",
											"Despereaux",
                                            "Devlin",
                                            "Devey",
                                            "Ebner",
                                            "Edelman",
                                            "Evans",
											"Figgis",
                                            "Flemming",
											"Freeman",
											"Giles",
											"Gillette",
                                            "Gonzalez",
                                            "Garoppolo",
                                            "Gostkowski",
                                            "Gray",
                                            "Green",
                                            "Gronkowski",
											"Guster",
                                            "Harmon",
											"Harris",
                                            "Hernandez",
                                            "Hightower",
                                            "Hill",
                                            "Hoomanawanui",
											"Jackov",
											"Johnson",
                                            "Jones",
											"Kane",
                                            "Kline",
											"Krieger",
                                            "LaFell",
                                            "Lee",
                                            "Lewis",
                                            "Lu",
                                            "Maneri",
                                            "Martinez",
                                            "Mars",
                                            "Masahashi",
                                            "McCourty",
                                            "Miller",
											"McDonald",
											"McNabb",
											"Meers",
											"Morgan",
                                            "Ninkovich",
											"OHara",
                                            "Panettiere",
                                            "Parkman",
                                            "Phillips",
											"Ray",
                                            "Revis",
											"Rosenberg",
                                            "Ryan",
                                            "Sanchez",
                                            "Siliga",
                                            "Slater",
                                            "Solder",
                                            "Smith",
											"Spencer",
                                            "Stewart",
                                            "Stork",
											"Summers",
                                            "Suresh",
                                            "Taylor",
                                            "Thomas",
                                            "Thompson",
											"Tunt",
                                            "Tyms",
											"Vick",
                                            "Ventimiglia",
                                            "Vereen",
                                            "Vollmer",
                                            "Wendal",
                                            "White",
                                            "Wilfork",
											"Wilkins",
                                            "Williams",
                                            "Wilson",
											"Winchester",
                                            "Wood",
                                            "Wright",
                                            "Young",
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

        #region Height

        protected virtual string HeightConvention(DataRequest request)
        {
            return _dataGenerator.NextInt(min: 106, max: 250).ToString();
        }
        #endregion

        #region Weight

        protected virtual string WeightConvention(DataRequest request)
        {
            return _dataGenerator.NextInt(20, 180).ToString();
        }

        #endregion


        private string DateOfBirthConvention(DataRequest arg)
        {
            return _dataGenerator.NextDateTime(DateTime.Today.AddYears(-100), DateTime.Today).ToString("MM/dd/yyyy");
        }

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
