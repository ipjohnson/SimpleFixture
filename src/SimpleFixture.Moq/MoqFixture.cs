using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace SimpleFixture.Moq
{
    public class MoqFixture : Fixture
    {
        public MoqFixture(IFixtureConfiguration configuration = null) : base(configuration)
        {
            Add(new MoqConvention());
        }

        public Mock<T> Mock<T>(Action<Mock<T>> mockAction) where T : class
        {
            var returnValue = Locate<Mock<T>>();

            mockAction(returnValue);

            return returnValue;
        }
    }
}
