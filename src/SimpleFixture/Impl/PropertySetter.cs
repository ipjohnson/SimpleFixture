using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFixture.Impl
{
    public interface IPropertySetter
    {
        void SetProperty(PropertyInfo propertyInfo, object instance, object value);
    }

    public class PropertySetter : IPropertySetter
    {
        public void SetProperty(PropertyInfo propertyInfo, object instance, object value)
        {
            if (propertyInfo.CanWrite)
            {
                propertyInfo.SetValue(instance, value);
            }
        }
    }
}
