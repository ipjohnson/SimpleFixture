using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SimpleFixture.Conventions;

namespace SimpleFixture
{
    public class ReturnConfiguration<T>
    {
        private FilteredConvention<T> _convention;

        public ReturnConfiguration(FilteredConvention<T> convention)
        {
            _convention = convention;
        }

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

        public ReturnConfiguration<T> WhenNamed(string name)
        {
            _convention.AddFilter(r => r.RequestName.ToLowerInvariant() == name.ToLowerInvariant());

            return this;
        }

        public ReturnConfiguration<T> WhenNamed(Func<string, bool> namedFunc)
        {
            _convention.AddFilter(r => namedFunc(r.RequestName));

            return this;
        }

        public ReturnConfiguration<T> WhenMatching(Func<DataRequest, bool> matchingFunc)
        {
            _convention.AddFilter(matchingFunc);

            return this;
        }
    }
}
