using SimpleFixture.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFixture.Moq
{
    public class MoqFixtureInitializeAttribute : FixtureInitializationAttribute
    {
        public MoqFixtureInitializeAttribute()
        {
            DefaultSingleton = true;
        }

        public bool DefaultSingleton { get; set; }

        public override void Initialize(Fixture fixture)
        {
            fixture.Add(new MoqConvention(DefaultSingleton));
        }
    }
}
