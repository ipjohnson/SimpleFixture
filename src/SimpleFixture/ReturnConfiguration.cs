using System;
using System.Reflection;
using SimpleFixture.Conventions;
using SimpleFixture.Impl;

namespace SimpleFixture
{
    /// <summary>
    /// Class used to configure when a return value should be used
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ReturnConfiguration<T>
    {
        private readonly FilteredConvention<T> _convention;
        private readonly Fixture _fixture;
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="convention">filter convention</param>
        /// <param name="fixture"></param>
        public ReturnConfiguration(FilteredConvention<T> convention, Fixture fixture)
        {
            _convention = convention;
            _fixture = fixture;
        }

        /// <summary>
        /// Use return value for specific parent types
        /// </summary>
        /// <typeparam name="TValue">filter type</typeparam>
        /// <returns>return configuration</returns>
        public ReturnConfiguration<T> For<TValue>()
        {
            _convention.AddFilter(r =>
                                  {
                                      if (r.ParentRequest != null &&
                                          typeof(TValue).GetTypeInfo()
                                              .IsAssignableFrom(r.ParentRequest.RequestedType.GetTypeInfo()))
                                      {
                                          return true;
                                      }

                                      return false;
                                  });

            return this;
        }

        /// <summary>
        /// Use return value for specific parent types
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ReturnConfiguration<T> For(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            _convention.AddFilter(r =>
            {
                if (r.ParentRequest != null &&
                    type.GetTypeInfo()
                        .IsAssignableFrom(r.ParentRequest.RequestedType.GetTypeInfo()))
                {
                    return true;
                }

                return false;
            });

            return this;
        }

        /// <summary>
        /// Filter based on name (Parameter name or Property name)
        /// </summary>
        /// <param name="name">name to match exactly</param>
        /// <returns>return configuration</returns>
        public ReturnConfiguration<T> WhenNamed(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            _convention.AddFilter(r => string.Equals(r.RequestName, name, StringComparison.CurrentCultureIgnoreCase));

            return this;
        }

        /// <summary>
        /// Filter based on name function (Parameter name or Property name)
        /// </summary>
        /// <param name="namedFunc">name func used to filter</param>
        /// <returns>return configuration</returns>
        public ReturnConfiguration<T> WhenNamed(Func<string, bool> namedFunc)
        {
            if (namedFunc == null) throw new ArgumentNullException(nameof(namedFunc));

            _convention.AddFilter(r => namedFunc(r.RequestName));

            return this;
        }

        /// <summary>
        /// Filter based on data request
        /// </summary>
        /// <param name="matchingFunc">filter method</param>
        /// <returns>return configuration</returns>
        public ReturnConfiguration<T> WhenMatching(Func<DataRequest, bool> matchingFunc)
        {
            if (matchingFunc == null) throw new ArgumentNullException(nameof(matchingFunc));

            _convention.AddFilter(matchingFunc);

            return this;
        }
    }
}
