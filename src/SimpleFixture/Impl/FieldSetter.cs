using System.Reflection;

namespace SimpleFixture.Impl
{
    public interface IFieldSetter
    {
        void SetField(FieldInfo fieldInfo, object instance, object value);
    }

    public class FieldSetter : IFieldSetter
    {
        public void SetField(FieldInfo fieldInfo, object instance, object value)
        {
            fieldInfo.SetValue(instance, value);
        }
    }
}
