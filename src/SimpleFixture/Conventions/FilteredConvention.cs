using System;
using System.Collections.Generic;

namespace SimpleFixture.Conventions
{
    /// <summary>
    /// Convention for filtering
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FilteredConvention<T> : SimpleTypeConvention<T>
    {
        private readonly List<Func<DataRequest, bool>> _filters = new List<Func<DataRequest, bool>>();
        private readonly Func<DataRequest, T> _valueFunc;
        private ConventionPriority _priority;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="valueFunc"></param>
        /// <param name="priority"></param>
        public FilteredConvention(Func<DataRequest, T>  valueFunc, ConventionPriority priority = ConventionPriority.Low)
        {
            _valueFunc = valueFunc;
            _priority = priority;
        }

        /// <summary>
        /// Priority for the convention, last by default
        /// </summary>
        public override ConventionPriority Priority => _priority;

        /// <summary>
        /// Add filter to convention
        /// </summary>
        /// <param name="matchingFilter"></param>
        public void AddFilter(Func<DataRequest, bool> matchingFilter)
        {
            _filters.Add(matchingFilter);

            CalculatePriority();
        }
        
        /// <summary>
        /// Generate data for the request, return Constrain.NoValue instead of null
        /// </summary>
        /// <param name="request">data request</param>
        /// <returns>generated data</returns>
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
    }
}
