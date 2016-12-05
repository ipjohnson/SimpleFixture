using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFixture.Impl
{
    /// <summary>
    /// Min max value for a specific type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MinMaxValue<T>
    {
        /// <summary>
        /// Min value
        /// </summary>
        public T Min { get; set; }

        /// <summary>
        /// Max value
        /// </summary>
        public T Max { get; set; }
    }

    /// <summary>
    /// Constraint helper get's values from a contraint object
    /// </summary>
    public interface IConstraintHelper
    {
        /// <summary>
        /// Get value from a constraint object
        /// </summary>
        /// <typeparam name="TProp"></typeparam>
        /// <param name="constraintValue"></param>
        /// <param name="defaultValue"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        TProp GetValue<TProp>(object constraintValue, TProp defaultValue, params string[] propertyNames);

        /// <summary>
        /// Get untyped value from constraint object
        /// </summary>
        /// <param name="valueType"></param>
        /// <param name="constraintValue"></param>
        /// <param name="defaultValue"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        bool GetUnTypedValue(out object value, Type valueType, object constraintValue, object defaultValue, params string[] propertyNames);

        /// <summary>
        /// Get min max for data request
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        MinMaxValue<T> GetMinMax<T>(DataRequest request, T min, T max) where T : IComparable;
    }

    /// <summary>
    /// Constraint objects that implement this interface can be queried to provide values
    /// </summary>
    public interface IConstraintValueProvider
    {
        /// <summary>
        /// Provide value
        /// </summary>
        /// <param name="valueType"></param>
        /// <param name="defualtValue"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        object ProvideValue(Type valueType, object defualtValue, params string[] propertyNames);
    }

    public class ConstraintHelper : IConstraintHelper
    {
        public MinMaxValue<T> GetMinMax<T>(DataRequest request, T min, T max) where T : IComparable
        {
            MinMaxValue<T> returnValue = new MinMaxValue<T> { Min = min, Max = max };
            Attribute attribute = null;

            var memberInfo = request.ExtraInfo as MemberInfo;

            if (memberInfo != null)
            {
                attribute = memberInfo.GetCustomAttributes(true).OfType<Attribute>().FirstOrDefault(a => a.GetType().Name == "RangeAttribute");
            }
            else if (request.ExtraInfo is ParameterInfo)
            {
                attribute = ((ParameterInfo)request.ExtraInfo).GetCustomAttributes(true).OfType<Attribute>().FirstOrDefault(a => a.GetType().Name == "RangeAttribute");
            }

            if (attribute != null)
            {
                var minProperty = attribute.GetType().GetRuntimeProperty("Minimum");
                var maxProperty = attribute.GetType().GetRuntimeProperty("Maximum");

                if (minProperty != null && maxProperty != null)
                {
                    T localMin = (T)Convert.ChangeType(minProperty.GetValue(attribute), typeof(T));
                    T localMax = (T)Convert.ChangeType(maxProperty.GetValue(attribute), typeof(T));

                    if (localMax.CompareTo(returnValue.Max) < 0)
                    {
                        returnValue.Max = localMax;
                    }

                    if (localMin.CompareTo(returnValue.Min) > 0)
                    {
                        returnValue.Min = localMin;
                    }
                }
            }

            return returnValue;
        }

        public bool GetUnTypedValue(out object value, Type type, object constraintValue, object defaultValue, params string[] propertyNames)
        {
            bool returnValue = false;
            value = defaultValue;

            if (constraintValue == null)
            {
                return returnValue;
            }
                        
            var valueProvider = constraintValue as IConstraintValueProvider;

            if(valueProvider != null)
            {
                value = valueProvider.ProvideValue(type, defaultValue, propertyNames);

                returnValue = value != null;
            }
            else
            {
                IEnumerable<KeyValuePair<string, object>> dictionary = constraintValue as IEnumerable<KeyValuePair<string, object>>;

                if (dictionary != null)
                {
                    foreach (string propertyName in propertyNames)
                    {
                        var keyValuePair =
                            dictionary.FirstOrDefault(
                                kvp => string.Compare(kvp.Key, propertyName, StringComparison.CurrentCultureIgnoreCase) == 0);

                        if (!keyValuePair.Equals(default(KeyValuePair<string, object>)))
                        {
                            value = keyValuePair.Value;
                            returnValue = true;
                            break;
                        }
                    }

                    if (!returnValue)
                    {
                        var values = dictionary.FirstOrDefault(
                                kvp => string.Compare(kvp.Key, "_Values", StringComparison.CurrentCultureIgnoreCase) == 0);

                        if (values.Value is IEnumerable)
                        {
                            foreach (var enumerable in values.Value as IEnumerable)
                            {
                                if (type.GetTypeInfo().IsAssignableFrom(enumerable.GetType().GetTypeInfo()))
                                {
                                    value = enumerable;
                                    returnValue = true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    PropertyInfo propInfo = null;

                    foreach (string propertyName in propertyNames)
                    {
                        propInfo = constraintValue.GetType().GetRuntimeProperties().FirstOrDefault(
                            p => string.Compare(p.Name, propertyName, StringComparison.CurrentCultureIgnoreCase) == 0);

                        if (propInfo != null)
                        {
                            break;
                        }
                    }

                    if (propInfo != null)
                    {
                        value = propInfo.GetMethod.Invoke(constraintValue, new object[] { });
                        returnValue = true;
                    }

                    if (!returnValue)
                    {
                        propInfo = constraintValue.GetType().GetRuntimeProperties().FirstOrDefault(
                            p => string.Compare(p.Name, "_Values", StringComparison.CurrentCultureIgnoreCase) == 0);

                        if (propInfo != null)
                        {
                            var values = propInfo.GetValue(constraintValue) as IEnumerable;

                            if (values != null)
                            {
                                foreach (var enumerable in values)
                                {
                                    if (enumerable != null &&
                                        type.GetTypeInfo().IsAssignableFrom(enumerable.GetType().GetTypeInfo()))
                                    {
                                        value = enumerable;
                                        returnValue = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (!returnValue || value == null)
            {
                return returnValue;
            }

            if (!type.GetTypeInfo().IsAssignableFrom(value.GetType().GetTypeInfo()))
            {
                try
                {
                    value = Convert.ChangeType(value, type);
                }
                catch (Exception)
                {
                    value = defaultValue;
                    returnValue = value != null;
                }
            }

            return returnValue;
        }

        public TProp GetValue<TProp>(object constraintValue, TProp defaultValue, params string[] propertyNames)
        {
            object value;
            GetUnTypedValue(out value, typeof(TProp), constraintValue, defaultValue, propertyNames);

            return (TProp)value;
        }
    }
}
