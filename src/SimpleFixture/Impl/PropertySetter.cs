using System.Reflection;

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
