using SimpleFixture.Impl;
using System;
using System.Collections.Generic;

namespace SimpleFixture.Conventions.Named
{
    /// <summary>
    /// Base convention for type populating using names
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseNamedConvention<T> : SimpleTypeConvention<T>
    {
        private Dictionary<string, Func<DataRequest, T>> _nameConventions;
        protected readonly IRandomDataGeneratorService _dataGenerator;
        protected readonly IConstraintHelper _helper;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dataGenerator"></param>
        /// <param name="helper"></param>
        protected BaseNamedConvention(IRandomDataGeneratorService dataGenerator, IConstraintHelper helper)
        {
            _dataGenerator = dataGenerator;
            _helper = helper;
        }

        /// <summary>
        /// Priority for the convention, last by default
        /// </summary>
        public override ConventionPriority Priority => ConventionPriority.Last;

        /// <summary>
        /// Generate date for the request, return Constrain.NoValue instead of null
        /// </summary>
        /// <param name="request">data request</param>
        /// <returns>generated data</returns>
        public override object GenerateData(DataRequest request)
        {
            if (_nameConventions == null)
            {
                _nameConventions = new Dictionary<string, Func<DataRequest, T>>();

                Initialize();
            }

            Func<DataRequest, T> stringFunc;
            var requestName = request.RequestName;

            if (_nameConventions.TryGetValue(request.RequestName.ToLowerInvariant(), out stringFunc))
            {
                return stringFunc(request);
            }

            var allUpper = true;

            foreach (var c in requestName)
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

            var lastUpper = 0;

            for (var i = 1; i < requestName.Length; i++)
            {
                if (char.IsUpper(requestName[i]))
                {
                    if (lastUpper == i - 1)
                    {
                        lastUpper = i;
                        continue;
                    }

                    var leftString = requestName.Substring(0, i - 1);

                    if (_nameConventions.TryGetValue(leftString.ToLowerInvariant(), out stringFunc))
                    {
                        return stringFunc(request);
                    }

                    var rightString = requestName.Substring(i);

                    if (_nameConventions.TryGetValue(rightString.ToLowerInvariant(), out stringFunc))
                    {
                        return stringFunc(request);
                    }
                }
            }

            return Convention.NoValue;
        }

        /// <summary>
        /// Add convention func for set of names
        /// </summary>
        /// <param name="stringFunc">convention</param>
        /// <param name="names">names</param>
        protected void AddConvention(Func<DataRequest, T> stringFunc, params string[] names)
        {
            foreach (var name in names)
            {
                _nameConventions[name.ToLowerInvariant()] = stringFunc;
            }
        }

        /// <summary>
        /// Initialze convention
        /// </summary>
        protected abstract void Initialize();
    }
}
