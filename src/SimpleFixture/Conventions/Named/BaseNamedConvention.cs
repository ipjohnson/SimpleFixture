using SimpleFixture.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFixture.Conventions.Named
{
    public abstract class BaseNamedConvention<T> : SimpleTypeConvention<T>
    {
        private Dictionary<string, Func<DataRequest, T>> _nameConventions;
        protected readonly IRandomDataGeneratorService _dataGenerator;
        protected readonly IConstraintHelper _helper;

        public BaseNamedConvention(IRandomDataGeneratorService dataGenerator, IConstraintHelper helper)
        {
            _dataGenerator = dataGenerator;
            _helper = helper;
        }
        
        public override ConventionPriority Priority => ConventionPriority.Last;

        public override object GenerateData(DataRequest request)
        {
            if (_nameConventions == null)
            {
                _nameConventions = new Dictionary<string, Func<DataRequest, T>>();

                Initialize();
            }

            Func<DataRequest, T> stringFunc;
            string requestName = request.RequestName;

            if (_nameConventions.TryGetValue(request.RequestName.ToLowerInvariant(), out stringFunc))
            {
                return stringFunc(request);
            }

            bool allUpper = true;

            foreach (char c in requestName)
            {
                if (char.IsLower(c))
                {
                    allUpper = false;
                }
            }

            if (allUpper)
            {
                return Convention.NoValue;
            }

            int lastUpper = 0;

            for (int i = 1; i < requestName.Length; i++)
            {
                if (char.IsUpper(requestName[i]))
                {
                    if (lastUpper == i - 1)
                    {
                        lastUpper = i;
                        continue;
                    }

                    string leftString = requestName.Substring(0, i - 1);

                    if (_nameConventions.TryGetValue(leftString.ToLowerInvariant(), out stringFunc))
                    {
                        return stringFunc(request);
                    }

                    string rightString = requestName.Substring(i);

                    if (_nameConventions.TryGetValue(rightString.ToLowerInvariant(), out stringFunc))
                    {
                        return stringFunc(request);
                    }
                }
            }

            return Convention.NoValue;
        }

        protected void AddConvention(Func<DataRequest, T> stringFunc, params string[] names)
        {
            foreach (string name in names)
            {
                _nameConventions[name.ToLowerInvariant()] = stringFunc;
            }
        }

        protected abstract void Initialize();
    }
}
