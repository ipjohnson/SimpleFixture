using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFixture.Impl
{
    public interface IConstructorSelector
    {
        ConstructorInfo SelectConstructor(Type type);
    }

    public class ConstructorSelector : IConstructorSelector
    {
        public ConstructorInfo SelectConstructor(Type type)
        {
            List<ConstructorInfo> constructors = new List<ConstructorInfo>();

            int maxParameters = -1;

            List<ConstructorInfo> allConstructors = new List<ConstructorInfo>(type.GetTypeInfo()
                                                 .DeclaredConstructors
                                                 .Where(c => c.IsPublic && !c.IsStatic));

            allConstructors.Sort((x, y) => Comparer<int>.Default.Compare(y.GetParameters().Length,
                                                                         x.GetParameters().Length));


            foreach (ConstructorInfo info in allConstructors)
            {
                if (info.GetParameters().Count() < maxParameters)
                {
                    continue;
                }

                constructors.Add(info);
                maxParameters = info.GetParameters().Count();
            }

            if (constructors.Count == 1)
            {
                return constructors[0];
            }

            constructors.Sort(
                (x, y) => Comparer<int>.Default.Compare(y.GetParameters().Count(p => !p.ParameterType.GetTypeInfo().IsPrimitive),
                                                       x.GetParameters().Count(p => !p.ParameterType.GetTypeInfo().IsPrimitive)));

            return constructors.FirstOrDefault();
        }
    }
}
