using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFixture.Conventions
{
    public abstract class SimpleTypeConvention<T> : ITypedConvention
    {
        public virtual ConventionPriority Priority
        {
            get { return ConventionPriority.Last; }
        }

        public abstract object GenerateData(DataRequest request);

        public virtual IEnumerable<Type> SupportedTypes
        {
            get { yield return typeof(T); }
        }
    }
}
