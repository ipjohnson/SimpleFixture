using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFixture.Impl
{
    public interface ITypePropertySelector
    {
        IEnumerable<PropertyInfo> SelectProperties(object instance, DataRequest request, ComplexModel model);
    }

    public class TypePropertySelector : ITypePropertySelector
    {
        private IConstraintHelper _helper;

        public TypePropertySelector(IConstraintHelper helper)
        {
            _helper = helper;
        }

        public virtual IEnumerable<PropertyInfo> SelectProperties(object instance, DataRequest request, ComplexModel model)
        {
            var skipProperties = new List<string>();

            var skipPropertiesEnumerable = _helper.GetValue<IEnumerable<string>>(request.Constraints,
                                                                        null,
                                                                        "_skipProps",
                                                                        "_skipProperties");

            if (skipPropertiesEnumerable != null)
            {
                skipProperties.AddRange(skipPropertiesEnumerable);
            }

            return instance.GetType()
                           .GetRuntimeProperties()
                           .Where(p => p.CanWrite &&
                                       p.SetMethod.IsPublic &&
                                      !p.SetMethod.IsStatic &&
                                       p.SetMethod.GetParameters().Count() == 1 &&
                                      !skipProperties.Contains(p.Name));
        }
    }
}
