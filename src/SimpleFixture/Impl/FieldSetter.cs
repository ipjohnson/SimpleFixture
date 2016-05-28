using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
