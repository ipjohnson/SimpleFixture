using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NSub = NSubstitute;

namespace SimpleFixture.NSubstitute
{
    public class SubstituteConvention : IConvention
    {
        private readonly Dictionary<Type, object> _substituted = new Dictionary<Type, object>();

        public ConventionPriority Priority
        {
            get { return ConventionPriority.Last; }
        }

        public object GenerateData(DataRequest request)
        {
            if (!request.RequestedType.GetTypeInfo().IsInterface)
            {
                return Convention.NoValue;
            }

            object returnValue;

            if (!_substituted.TryGetValue(request.RequestedType, out returnValue))
            {
                returnValue = NSub.Substitute.For(new[] { request.RequestedType }, new object[0]);

                _substituted[request.RequestedType] = returnValue;
            }

            return returnValue;
        }
    }
}
