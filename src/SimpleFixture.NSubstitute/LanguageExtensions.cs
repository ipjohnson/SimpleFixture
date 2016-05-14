using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFixture.NSubstitute
{
    public static class LanguageExtensions
    {
        public static T Substitute<T>(this Fixture fixture, Action<T> substituteAction = null, bool? singleton = null)
        {
            T returnValue = fixture.Generate<T>(constraints: new
            {
                fakeSingleton = singleton
            });

            substituteAction?.Invoke(returnValue);

            return returnValue;
        }
    }
}
