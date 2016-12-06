using System;

namespace SimpleFixture.NSubstitute
{
    public static class LanguageExtensions
    {
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
