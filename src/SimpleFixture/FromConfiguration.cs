using SimpleFixture.Impl;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SimpleFixture
{
    public class FromConfiguration
    {
        private Fixture _fixture;

        public FromConfiguration(Fixture fixture)
        {
            _fixture = fixture;
        }

        public TypeMatchingConfiguration From(IEnumerable<Type> types)
        {
            var convention = new ExportAllByInterfaceConvention(types);

            _fixture.Add(convention);

            return new TypeMatchingConfiguration(convention);
        }

        public TypeMatchingConfiguration FromAssembly(Assembly assembly)
        {
            return From(assembly.ExportedTypes);
        }

        public TypeMatchingConfiguration FromAssemblyContaining<T>()
        {
            return From(typeof(T).GetTypeInfo().Assembly.ExportedTypes);
        }
    }

    public class TypeMatchingConfiguration
    {
        private ExportAllByInterfaceConvention _convention;
        private List<Func<Type, Type, bool>> _filters;

        public TypeMatchingConfiguration(ExportAllByInterfaceConvention convention)
        {
            _convention = convention;
            _convention.TypeFilter = TypeFilter;
            _filters = new List<Func<Type, Type, bool>>();
        }

        private bool TypeFilter(Type implementationType, Type interfaceType)
        {
            foreach(var filter in _filters)
            {
                if(!filter(implementationType,interfaceType))
                {
                    return false;
                }
            }

            return true;
        }

        public TypeMatchingConfiguration Matching(Func<Type, Type, bool> matchingDelegate)
        {
            _filters.Add(matchingDelegate);

            return this;
        }

        public TypeMatchingConfiguration InterfaceMatching<T>()
        {
            _filters.Add((c, i) => typeof(T).GetTypeInfo().IsAssignableFrom(i.GetTypeInfo()));

            return this;
        }

        public TypeMatchingConfiguration InNamespace(string namespaceStr, bool inherit = true)
        {
            if (!inherit)
            {
                _filters.Add((c, i) => c.Namespace == namespaceStr);
            }
            else
            {
                _filters.Add((c, i) => c.Namespace == namespaceStr || c.Namespace.StartsWith(namespaceStr + "."));
            }

            return this;
        }

        public TypeMatchingConfiguration EndsWith(string name)
        {
            _filters.Add((c, i) => c.Name.EndsWith(name));

            return this;
        }

        public TypeMatchingConfiguration AsSingleton(Func<Type,Type,bool> match = null)
        {
            if(match == null)
            {
                match = (c, t) => true;
            }

            _convention.AsSingleton = match;

            return this;
        }
    }
}
