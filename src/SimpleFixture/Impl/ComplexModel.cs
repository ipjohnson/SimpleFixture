using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFixture.Impl
{
    public class ComplexModel
    {
        private List<string> _skipProperties;
        private List<Func<DataRequest, PropertyInfo, bool>> _skipMatchingProperties;
        private List<Tuple<string, Func<DataRequest, object>>> _propertyValues;
        private List<Tuple<Func<PropertyInfo, bool>, Func<DataRequest, PropertyInfo, object>>> _propertiesValue;
        private List<Action<object>> _applyActions; 

        public Func<DataRequest, object> New { get; set; }

        public void AddSkipProperty(string propertyName)
        {
            if (_skipProperties == null)
            {
                _skipProperties = new List<string>();
            }

            _skipProperties.Add(propertyName);
        }

        public void AddSkipMatchingProperty(Func<DataRequest, PropertyInfo, bool> matchingFunc)
        {
            if (_skipMatchingProperties == null)
            {
                _skipMatchingProperties = new List<Func<DataRequest, PropertyInfo, bool>>();
            }

            _skipMatchingProperties.Add(matchingFunc);
        }

        public void AddPropertyValue(string propertyName, Func<DataRequest, object> propertyValue)
        {
            if (_propertyValues == null)
            {
                _propertyValues = new List<Tuple<string, Func<DataRequest, object>>>();
            }

            _propertyValues.Add(new Tuple<string, Func<DataRequest, object>>(propertyName, propertyValue));
        }

        public void AddPropertiesValue(Func<PropertyInfo, bool> propertyPicker, Func<DataRequest, PropertyInfo, object> propertyValue)
        {
            if (_propertiesValue == null)
            {
                _propertiesValue = new List<Tuple<Func<PropertyInfo, bool>, Func<DataRequest, PropertyInfo, object>>>();
            }

            _propertiesValue.Add(new Tuple<Func<PropertyInfo, bool>, Func<DataRequest, PropertyInfo, object>>(propertyPicker, propertyValue));
        }

        public void AddApply(Action<object> apply)
        {
            if (_applyActions == null)
            {
                _applyActions = new List<Action<object>>();
            }

            _applyActions.Add(apply);
        }

        public void Apply(object instance)
        {
            if (_applyActions != null)
            {
                foreach (Action<object> applyAction in _applyActions)
                {
                    applyAction(instance);
                }
            }
        }

        public bool SkipProperty(DataRequest request, PropertyInfo info)
        {
            if (_skipProperties != null)
            {
                if (_skipProperties.Contains(info.Name))
                {
                    return true;
                }
            }

            if (_skipMatchingProperties != null)
            {
                if (_skipMatchingProperties.Any(m => m(request, info)))
                {
                    return true;
                }
            }

            return false;
        }

        public bool GetPropertyValue(DataRequest request, PropertyInfo info, out object value)
        {
            value = null;

            if (_propertyValues != null)
            {
                foreach (Tuple<string, Func<DataRequest, object>> propertyValue in _propertyValues)
                {
                    if (info.Name.ToLowerInvariant() == propertyValue.Item1.ToLowerInvariant())
                    {
                        value = propertyValue.Item2(request);

                        if (value != Convention.NoValue)
                        {
                            return true;
                        }
                    }
                }
            }

            if (_propertiesValue != null)
            {
                foreach (Tuple<Func<PropertyInfo, bool>, Func<DataRequest, PropertyInfo, object>> tuple in _propertiesValue)
                {
                    if (tuple.Item1(info))
                    {
                        value = tuple.Item2(request, info);

                        if (value != Convention.NoValue)
                        {
                            return true;
                        }
                    }
                }   
            }

            return false;
        }

        public ComplexModel Clone()
        {
            return new ComplexModel
                   {
                       New = New,
                       _skipProperties = _skipProperties == null ? _skipProperties : new List<string>(_skipProperties),
                       _skipMatchingProperties = _skipMatchingProperties == null ? _skipMatchingProperties : new List<Func<DataRequest, PropertyInfo, bool>>(_skipMatchingProperties),
                       _propertyValues = _propertyValues == null ? _propertyValues : new List<Tuple<string, Func<DataRequest, object>>>(_propertyValues),
                       _propertiesValue = _propertiesValue == null ? _propertiesValue : new List<Tuple<Func<PropertyInfo, bool>, Func<DataRequest, PropertyInfo, object>>>(_propertiesValue)
                   };
        }
    }
}
