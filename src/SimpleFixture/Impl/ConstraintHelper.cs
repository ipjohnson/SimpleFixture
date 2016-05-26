using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFixture.Impl
{
    public class MinMaxValue<T>
    {
        public T Min { get; set; }

        public T Max { get; set; }
    }

    public interface IConstraintHelper
    {
        TProp GetValue<TProp>(object constraintValue, TProp defualtValue, params string[] propertyNames);

        object GetUnTypedValue(Type valueType, object constraintValue, object defualtValue, params string[] propertyNames);

        MinMaxValue<T> GetMinMax<T>(DataRequest request, T min, T max) where T : IComparable;
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

        public object GetUnTypedValue(Type type, object constraintValue, object defualtValue, params string[] propertyNames)
        {
            if (constraintValue == null)
            {
                return defualtValue;
            }

            object returnValue = null;

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

            if (returnValue == null)
            {
                return defualtValue;
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
                return defualtValue;
            }
        }

        public TProp GetValue<TProp>(object constraintValue, TProp defualtValue, params string[] propertyNames)
        {
            return (TProp)GetUnTypedValue(typeof(TProp), constraintValue, defualtValue, propertyNames);
        }
    }
}
