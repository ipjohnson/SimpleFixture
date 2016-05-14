using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFixture.Moq
{
    public static class LanguageExtensions
    {
        /// <summary>
        /// Mock a specific object, by default mocks are treated as singletons
        /// </summary>
        /// <typeparam name="T">type to mock</typeparam>
        /// <param name="mockAction">action to apply to the mock</param>
        /// <param name="singleton">should it be a singleton</param>
        /// <returns>new mock</returns>
        public static Mock<T> Mock<T>(this Fixture fixture, Action<Mock<T>> mockAction = null, bool? singleton = null) where T : class
        {
            Mock<T> returnValue = fixture.Generate<Mock<T>>(constraints: new
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
