using System;

namespace SimpleFixture.NSubstitute
{
    /// <summary>
    /// Fixture using NSubstitute
    /// </summary>
    public class SubFixture : Fixture
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="defaultSingleton"></param>
        public SubFixture(IFixtureConfiguration configuration = null, bool defaultSingleton = true)
            : base(configuration)
        {
            Add(new SubstituteConvention(defaultSingleton));
        }

        /// <summary>
        /// Substitute for a particular type, by default substitute types are treated as singletons
        /// </summary>
        /// <typeparam name="T">Type to substitute</typeparam>
        /// <param name="substituteAction">arrange</param>
        /// <param name="singleton">singleton</param>
        /// <returns>new substituted type</returns>
        public T Substitute<T>(Action<T> substituteAction = null, bool? singleton = null)
        {
            var returnValue = Generate<T>(constraints: new
                                                     {
                                                         fakeSingleton = singleton
                                                     });

            substituteAction?.Invoke(returnValue);

            return returnValue;
        }
    }
}
