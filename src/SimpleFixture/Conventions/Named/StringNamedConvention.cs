using System.Linq;
using SimpleFixture.Impl;
using System.Reflection;

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
            AddConvention(DateOfBirthConvention, "DateOfBirth", "DOB","Date");
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

        public override object GenerateData(DataRequest request)
        {
            var returnValue =  base.GenerateData(request);

            if(returnValue == Convention.NoValue && request.ExtraInfo is MemberInfo)
            {
                var memberInfo = (MemberInfo)request.ExtraInfo;

                foreach (var attribute in memberInfo.GetCustomAttributes())
                {
                    var attributeName = attribute.GetType().Name;

                    switch(attributeName)
                    {
                        case "EmailAddressAttribute":
                            returnValue = EmailConvention(request);
                            break;
                        case "PhoneAttribute":
                            returnValue = PhoneNumberConvention(request);
                            break;
                        case "UrlAttribute":
                            var uriConvention = new UriConvention(_helper);

                            returnValue = uriConvention.GenerateData(request).ToString();
                            break;                           
                    }
                }
            }

            if(returnValue is string && request.ExtraInfo is MemberInfo)
            {
                var stringValue = (string)returnValue;
                var memberInfo = (MemberInfo)request.ExtraInfo;

                var stringLengthAttr = memberInfo.GetCustomAttributes().FirstOrDefault(a => a.GetType().Name == "StringLengthAttribute");

                if(stringLengthAttr != null)
                {
                    var minProperty = stringLengthAttr.GetType().GetRuntimeProperty("MinimumLength");
                    var maxProperty = stringLengthAttr.GetType().GetRuntimeProperty("MaximumLength");
                    
                    if (minProperty != null)
                    {
                        var minLength = (int)minProperty.GetValue(stringLengthAttr);

                        if (stringValue.Length < minLength)
                        {
                            returnValue = new string('1', minLength - stringValue.Length) + stringValue;
                        }
                    }
                    if (maxProperty != null)
                    {
                        var maxLength = (int)maxProperty.GetValue(stringLengthAttr);

                        if (stringValue.Length > maxLength)
                        {
                            returnValue = stringValue.Substring(0, maxLength);
                        }
                    }
                }
            }

            return returnValue;
        }
    }
}
