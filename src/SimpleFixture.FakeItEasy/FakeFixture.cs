using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy.Creation;

namespace SimpleFixture.FakeItEasy
{
    public class FakeFixture : Fixture
    {
        public FakeFixture(IFixtureConfiguration configuration = null, bool fakeSingleton = true) : base(configuration)
        {
            Add(new FakeConvention(fakeSingleton));
        }
        
        public T Fake<T>(Action<T> arrange = null, Action<IFakeOptionsBuilder<T>> options = null, bool? singleton = null)
        {
            T returnValue = Generate<T>(constraints: new
                                                     {
                                                         fakeSingleton = singleton,
                                                         builderOptions = options
                                                     });

            if (arrange != null)
            {
                arrange(returnValue);
            }

            return returnValue;
        }
    }
}
