using System;

namespace SimpleFixture.NSubstitute
{
    /// <summary>
    /// Language extensions
    /// </summary>
    public static class LanguageExtensions
    {
        /// <summary>
        /// Substitute interface
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fixture"></param>
        /// <param name="substituteAction"></param>
        /// <param name="singleton"></param>
        /// <returns></returns>
        public static T Substitute<T>(this Fixture fixture, Action<T> substituteAction = null, bool? singleton = null)
        {
            var returnValue = fixture.Generate<T>(constraints: new
            {
                fakeSingleton = singleton
            });

            substituteAction?.Invoke(returnValue);

            return returnValue;
        }
    }
}
