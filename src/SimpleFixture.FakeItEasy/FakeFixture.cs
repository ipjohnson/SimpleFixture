using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeItEasy;
using FakeItEasy.Creation;

namespace SimpleFixture.FakeItEasy
{
    public class FakeFixture : Fixture
    {
        public FakeFixture(IFixtureConfiguration configuration = null, bool defaultSingleton = true) : base(configuration)
        {
            Add(new FakeConvention(defaultSingleton));
        }

        public T Fake<T>(Action<T> arrange = null, Action<IFakeOptions<T>> options = null, bool? singleton = null)
        {
            T returnValue = Generate<T>(constraints: new
            {
                fakeSingleton = singleton,
                builderOptions = options
            });

            arrange?.Invoke(returnValue);

            return returnValue;
        }
    }
}
