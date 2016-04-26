using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFixture.Impl
{
    public interface IConstraintHelper
    {
        TProp GetValue<TProp>(object constraintValue, TProp defualtValue, params string[] propertyNames);
    }

    public class ConstraintHelper : IConstraintHelper
    {
        public TProp GetValue<TProp>(object constraintValue, TProp defualtValue, params string[] propertyNames)
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
                    returnValue = (TProp)propInfo.GetMethod.Invoke(constraintValue, new object[] { });
                }
            }

            if (returnValue == null)
            {
                return defualtValue;
            }

            if (typeof(TProp).GetTypeInfo().IsAssignableFrom(returnValue.GetType().GetTypeInfo()))
            {
                return (TProp)returnValue;
            }

            try
            {
                return (TProp)Convert.ChangeType(returnValue, typeof(TProp));
            }
            catch(Exception)
            {
                return defualtValue;
            }
        }
    }
}
