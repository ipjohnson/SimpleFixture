using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SimpleFixture.Impl
{
    public interface IConstructorSelector
    {
        ConstructorInfo SelectConstructor(DataRequest request, Type type);
    }

    public class ConstructorSelector : IConstructorSelector
    {

        public ConstructorInfo SelectConstructor(DataRequest request, Type type)
        {
            var constructors = new List<ConstructorInfo>();

            var maxParameters = -1;

            var allConstructors = new List<ConstructorInfo>(type.GetTypeInfo()
                                                 .DeclaredConstructors
                                                 .Where(c => c.IsPublic && !c.IsStatic));

            var constructor = PickConstructorInfo(allConstructors, maxParameters, constructors);

            if (constructor != null)
            {
                return constructor;
            }

            if (request.Fixture.Configuration.UseNonPublicConstructors)
            {
                allConstructors.AddRange(type.GetTypeInfo()
                                                 .DeclaredConstructors
                                                 .Where(c => !c.IsPublic && !c.IsStatic));

                return PickConstructorInfo(allConstructors, maxParameters, constructors);
            }

            return null;
        }

        private static ConstructorInfo PickConstructorInfo(List<ConstructorInfo> allConstructors, int maxParameters, List<ConstructorInfo> constructors)
        {
            allConstructors.Sort((x, y) => Comparer<int>.Default.Compare(y.GetParameters().Length,
                x.GetParameters().Length));


            foreach (var info in allConstructors)
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
                (x, y) =>
                    Comparer<int>.Default.Compare(y.GetParameters().Count(p => !p.ParameterType.GetTypeInfo().IsPrimitive),
                        x.GetParameters().Count(p => !p.ParameterType.GetTypeInfo().IsPrimitive)));

            return constructors.FirstOrDefault();
        }
    }
}
