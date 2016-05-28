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
        object GetUnTypedValue(Type valueType, object constraintValue, object defaultValue, params string[] propertyNames);

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
                attribute = memberInfo.GetCustomAttributes(true).FirstOrDefault(a => a.GetType().Name == "RangeAttribute");
            }
            else if (request.ExtraInfo is ParameterInfo)
            {
                attribute = ((ParameterInfo)request.ExtraInfo).GetCustomAttributes(true).FirstOrDefault(a => a.GetType().Name == "RangeAttribute");
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

        public object GetUnTypedValue(Type type, object constraintValue, object defaultValue, params string[] propertyNames)
        {
            if (constraintValue == null)
            {
                return defaultValue;
            }

            object returnValue = null;

            var valueProvider = constraintValue as IConstraintValueProvider;

            if(valueProvider != null)
            {
                returnValue = valueProvider.ProvideValue(type, defaultValue, propertyNames);
            }
            else
            {
                IEnumerable<KeyValuePair<string, object>> dictionary = constraintValue as IEnumerable<KeyValuePair<string, object>>;

                if (dictionary != null)
                {
                    foreach (string propertyName in propertyNames)
                    {
                        var value =
                            dictionary.FirstOrDefault(
                                kvp => string.Compare(kvp.Key, propertyName, StringComparison.CurrentCultureIgnoreCase) == 0);

                        if (!value.Equals(default(KeyValuePair<string, object>)))
                        {
                            returnValue = value.Value;
                            break;
                        }
                    }

                    if (returnValue == null)
                    {
                        var values = dictionary.FirstOrDefault(
                                kvp => string.Compare(kvp.Key, "_Values", StringComparison.CurrentCultureIgnoreCase) == 0);

                        if (values.Value is IEnumerable)
                        {
                            foreach (var value in values.Value as IEnumerable)
                            {
                                if (type.GetTypeInfo().IsAssignableFrom(value.GetType().GetTypeInfo()))
                                {
                                    returnValue = value;
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
                        returnValue = propInfo.GetMethod.Invoke(constraintValue, new object[] { });
                    }

                    if (returnValue == null)
                    {
                        propInfo = constraintValue.GetType().GetRuntimeProperties().FirstOrDefault(
                            p => string.Compare(p.Name, "_Values", StringComparison.CurrentCultureIgnoreCase) == 0);

                        if (propInfo != null)
                        {
                            var values = propInfo.GetValue(constraintValue) as IEnumerable;

                            if (values != null)
                            {
                                foreach (var value in values)
                                {
                                    if (type.GetTypeInfo().IsAssignableFrom(value.GetType().GetTypeInfo()))
                                    {
                                        returnValue = value;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (returnValue == null)
            {
                return defaultValue;
            }

            if (type.GetTypeInfo().IsAssignableFrom(returnValue.GetType().GetTypeInfo()))
            {
                return returnValue;
            }

            try
            {
                return Convert.ChangeType(returnValue, type);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        public TProp GetValue<TProp>(object constraintValue, TProp defaultValue, params string[] propertyNames)
        {
            return (TProp)GetUnTypedValue(typeof(TProp), constraintValue, defaultValue, propertyNames);
        }
    }
}
