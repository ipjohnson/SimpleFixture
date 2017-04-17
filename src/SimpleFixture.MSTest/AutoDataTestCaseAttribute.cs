using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleFixture.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleFixture.MSTest
{
    public class AutoDataTestCaseAttribute : TestMethodAttribute
    {
        private object[] _parameters;

        public AutoDataTestCaseAttribute(params object[] parameters)
        {
            _parameters = parameters;
        }

        public override TestResult[] Execute(ITestMethod testMethod)
        {
            var parameterValues = AttributeHelper.GetData(testMethod.MethodInfo, _parameters);

            var result = testMethod.Invoke(parameterValues);

            result.DisplayName = $"{testMethod.TestMethodName} ({string.Join(",", parameterValues)})";

            return new[] { result };
        }
    }
}
