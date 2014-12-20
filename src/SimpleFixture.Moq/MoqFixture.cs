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
        public MoqFixture(IFixtureConfiguration configuration = null, bool defaultSingleton = true)
            : base(configuration)
        {
            Add(new MoqConvention(defaultSingleton));
        }

        /// <summary>
        /// Mock a specific object, by default mocks are treated as singletons
        /// </summary>
        /// <typeparam name="T">type to mock</typeparam>
        /// <param name="mockAction">action to apply to the mock</param>
        /// <param name="singleton">should it be a singleton</param>
        /// <returns>new mock</returns>
        public Mock<T> Mock<T>(Action<Mock<T>> mockAction = null, bool? singleton = null) where T : class
        {
            Mock<T> returnValue = Generate<Mock<T>>(constraints: new
            {
                moqSingleton = singleton
            });

            if (mockAction != null)
            {
                mockAction(returnValue);
            }
            
            return returnValue;
        }
    }
}
