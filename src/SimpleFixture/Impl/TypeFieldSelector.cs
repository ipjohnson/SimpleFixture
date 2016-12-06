using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SimpleFixture.Impl
{
    public interface ITypeFieldSelector
    {
        IEnumerable<FieldInfo> SelectFields(object instance, DataRequest request, ComplexModel model);
    }

    public class TypeFieldSelector : ITypeFieldSelector
    {
        private IConstraintHelper _helper;

        public TypeFieldSelector(IConstraintHelper helper)
        {
            _helper = helper;
        }
        
        public IEnumerable<FieldInfo> SelectFields(object instance, DataRequest request, ComplexModel model)
        {
            var skipFields = new List<string>();

            var skipFieldsEnumerable = _helper.GetValue<IEnumerable<string>>(request.Constraints,
                                                                        null,
                                                                        "_skipFields");

            if (skipFieldsEnumerable != null)
            {
                skipFields.AddRange(skipFieldsEnumerable);
            }

            return instance.GetType()
                           .GetRuntimeFields()
                           .Where(f => f.IsPublic && !skipFields.Contains(f.Name));

        }
    }
}
