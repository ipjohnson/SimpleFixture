using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleFixture.Impl;

namespace SimpleFixture.Conventions.Named
{
    public partial class StringNamedConvention : BaseNamedConvention<string>
    {

        public StringNamedConvention(IRandomDataGeneratorService dataGenerator, IConstraintHelper helper) : base(dataGenerator,helper)
        {
        }
        
        protected override void Initialize()
        {
            AddConvention(FirstNameConvention, "FirstName", "MiddleName");
            AddConvention(LastNameConvention, "LastName", "Surname");
            AddConvention(UsernameConvention,"Username");
            AddConvention(PasswordConvention,"Password");
            AddConvention(HeightConvention,"Height");
            AddConvention(WeightConvention,"Weight");
            AddConvention(DateOfBirthConvention, "DateOfBirth", "DOB");
            AddConvention(EmailConvention,"Email","EmailAddress","EmailAddr");
            AddConvention(GovernementIdConvention, "GovernmentId", "SSN", "SocialSecurityNumber");
            AddConvention(PhoneNumberConvention, "Phone", "PhoneNumber", "CellPhone", "CellPhoneNumber", "MobilePhone", "MobilePhoneNumber", "HomePhone", "HomePhoneNumber","WorkPhone","WorkPhoneNumber");

            AddConvention(AddressLineOneConvention,"Address", "AddressLine", "AddressLine1", "AddressLineOne", "AddrLine1", "HomeAddress1", "MailingAddress1");
            AddConvention(AddressLineTwoConvention, "AddressLine2", "AddressLineTwo", "AddrLine2", "HomeAddress2", "MailingAddress2");
            AddConvention(CityConvention, "City", "HomeCity", "MailingCity");
            AddConvention(StateConvention, "State", "StateProvince");
            AddConvention(StateAbbreviation, "StateAbbreviation");
            AddConvention(PostalCodeConvention, "PostalCode", "ZipCode", "Zip", "HomeZip", "MailingZip");
            AddConvention(CountryConvention, "Country", "HomeCountry","MailingCountry");
        }
    }
}
