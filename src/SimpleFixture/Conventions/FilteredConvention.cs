using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFixture.Conventions
{
    public class FilteredConvention<T> : SimpleTypeConvention<T>
    {
        private readonly List<Func<DataRequest, bool>> _filters = new List<Func<DataRequest, bool>>();
        private readonly Func<DataRequest, T> _valueFunc;
        private readonly ConventionPriority _priority;

        public FilteredConvention(Func<DataRequest, T>  valueFunc, ConventionPriority priority = ConventionPriority.High)
        {
            _valueFunc = valueFunc;
            _priority = priority;
        }

        public override ConventionPriority Priority
        {
            get { return _priority; }
        }

        public void AddFilter(Func<DataRequest, bool> matchingFilter)
        {
            _filters.Add(matchingFilter);
        }

        public override object GenerateData(DataRequest request)
        {
            foreach (Func<DataRequest, bool> filter in _filters)
            {
                if (!filter(request))
                {
                    return Convention.NoValue;
                }
            }

            return _valueFunc(request);
        }
    }
}
