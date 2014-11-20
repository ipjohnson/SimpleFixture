using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleFixture.Impl;

namespace SimpleFixture.Conventions.Named
{
    public partial class StringNamedConvention : SimpleTypeConvention<string>
    {
        private Dictionary<string, Func<DataRequest, string>> _nameConventions;
        private readonly IRandomDataGeneratorService _dataGenerator;
        private readonly IConstraintHelper _helper;

        public StringNamedConvention(IRandomDataGeneratorService dataGenerator, IConstraintHelper helper)
        {
            _dataGenerator = dataGenerator;
            _helper = helper;
        }

        public override ConventionPriority Priority
        {
            get { return ConventionPriority.Low; }
        }

        public override object GenerateData(DataRequest request)
        {
            if (_nameConventions == null)
            {
                Initalize();
            }

            Func<DataRequest, string> stringFunc;

            if (_nameConventions.TryGetValue(request.RequestName.ToLowerInvariant(), out stringFunc))
            {
                return stringFunc(request);
            }

            return Convention.NoValue;
        }

        private void Initalize()
        {
            _nameConventions = new Dictionary<string, Func<DataRequest, string>>();

            AddConvention(FirstNameConvention, "FirstName", "MiddleName");
            AddConvention(LastNameConvention, "LastName", "Surname");
            AddConvention(GovernementIdConvention, "GovernmentId", "SSN", "SocialSecurityNumber");

            AddConvention(AddressLineOneConvention, "AddressLine", "AddressLine1", "AddressLineOne", "AddrLine1");
            AddConvention(AddressLineTwoConvention, "AddressLine2", "AddressLineTwo", "AddrLine2");
            AddConvention(PostalCodeConvention, "PostalCode", "ZipCode", "Zip");
        }
        
        private void AddConvention(Func<DataRequest, string> stringFunc, params string[] names)
        {
            foreach (string name in names)
            {
                _nameConventions[name.ToLowerInvariant()] = stringFunc;
            }
        }

    }
}
