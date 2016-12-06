using System;
using System.Collections.Generic;

namespace SimpleFixture.Conventions
{
    public class FilteredConvention<T> : SimpleTypeConvention<T>
    {
        private readonly List<Func<DataRequest, bool>> _filters = new List<Func<DataRequest, bool>>();
        private readonly Func<DataRequest, T> _valueFunc;
        private ConventionPriority _priority;

        public FilteredConvention(Func<DataRequest, T>  valueFunc, ConventionPriority priority = ConventionPriority.Low)
        {
            _valueFunc = valueFunc;
            _priority = priority;
        }

        public override ConventionPriority Priority => _priority;

        public void AddFilter(Func<DataRequest, bool> matchingFilter)
        {
            _filters.Add(matchingFilter);

            CalculatePriority();
        }

        private void CalculatePriority()
        {
            switch (_filters.Count)
            {
                case 0:
                    _priority = ConventionPriority.Low;
                    break;
                case 1:
                    _priority = ConventionPriority.Normal;
                    break;
                default:
                    _priority = ConventionPriority.High;
                    break;
            }

            RaisePriorityChanged(_priority);
        }

        public override object GenerateData(DataRequest request)
        {
            foreach (var filter in _filters)
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
