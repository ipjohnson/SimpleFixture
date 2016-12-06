using System;

namespace SimpleFixture.Attributes
{
    public class FixtureCustomizationAttribute : FixtureInitializationAttribute
    {
        private Type[] _types;

        public FixtureCustomizationAttribute(params Type[] types)
        {
            _types = types;
        }

        public override void Initialize(Fixture fixture)
        {
            foreach (var type in _types)
            {
                var initializer = fixture.Locate(type);

                if (initializer is IConvention)
                {
                    fixture.Add(initializer as IConvention);
                }
                else if (initializer is IFixtureCustomization)
                {
                    fixture.Add(initializer as IFixtureCustomization);
                }
            }
        }
    }
}
