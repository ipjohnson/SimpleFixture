using FakeItEasy;
using System;

namespace SimpleFixture.FakeItEasy
{
    public static class LanguageExtensions
    {
        public static T Fake<T>(this Fixture fixture, Action<T> arrange = null, Action<FakeOptionsBuilder<T>> options = null, bool? singleton = null) where T : class
        {
            T returnValue = fixture.Generate<T>(constraints: new
            {
                fakeSingleton = singleton,
                builderOptions = options
            });

            arrange?.Invoke(returnValue);

            return returnValue;
        }
    }
}
