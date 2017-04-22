using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SimpleFixture.Attributes
{
    /// <summary>
    /// Export specific type to be used in Fixture
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Assembly, AllowMultiple = true)]
    public class ExportAttribute : FixtureInitializationAttribute
    {
        private readonly Type _type;

        /// <summary>
        /// Default constuctor
        /// </summary>
        /// <param name="type"></param>
        public ExportAttribute(Type type)
        {
            _type = type;
        }

        /// <summary>
        /// Singleton
        /// </summary>
        public bool Singleton { get; set; }

        /// <summary>Initialize fixture</summary>
        /// <param name="fixture">fixture</param>
        public override void Initialize(Fixture fixture)
        {
            foreach (var implementedInterface in _type.GetTypeInfo().ImplementedInterfaces)
            {
                var closedMethod = _exportInterface.MakeGenericMethod(_type, implementedInterface);

                closedMethod.Invoke(this, new object[] { fixture });
            }
        }
        
        private void ExportInterface<T, TInterface>(Fixture fixture) where T : class, TInterface
        {
            fixture.ExportAs<T, TInterface>();
        }

        private static readonly MethodInfo _exportInterface =
            typeof(ExportAttribute).GetTypeInfo().DeclaredMethods.First(m => m.Name == "ExportInterface");
    }
}
