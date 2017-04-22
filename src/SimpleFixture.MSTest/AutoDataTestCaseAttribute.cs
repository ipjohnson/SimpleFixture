using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleFixture.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleFixture.MSTest
{
    /// <summary>
    /// MSTest attribute to create data for parameters
    /// </summary>
    public class AutoDataTestCaseAttribute : DataTestMethodAttribute
    {
        private readonly object[] _parameters;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="parameters"></param>
        public AutoDataTestCaseAttribute(params object[] parameters)
        {
            _parameters = parameters;
        }

        /// <summary>
        /// Execute tests on method
        /// </summary>
        /// <param name="testMethod"></param>
        /// <returns></returns>
        public override TestResult[] Execute(ITestMethod testMethod)
        {
            var parameterValues = AttributeHelper.GetData(testMethod.MethodInfo, _parameters);

            var result = testMethod.Invoke(parameterValues);

            result.DisplayName = $"{testMethod.TestMethodName} ({string.Join(",", parameterValues)})";

            return new[] { result };
        }
    }
}
