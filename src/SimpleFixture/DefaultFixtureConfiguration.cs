using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleFixture.DI;
using SimpleFixture.Impl;

namespace SimpleFixture
{
    public class DefaultFixtureConfiguration : GContainer, IFixtureConfiguration
    {
        public DefaultFixtureConfiguration()
        {
            UseDefaultConventions = true;

            UseNamedConventions = true;

            SetupDefaults();
        }

        private void SetupDefaults()
        {
            ExportSingleton<IRandomDataGeneratorService>(g => new RandomDataGeneratorService());
            Export<IConventionProvider>(g => new ConventionProvider());
            Export<IConstraintHelper>(g => new ConstraintHelper());
            Export<IConventionList>(g => new ConventionList());
            Export<ITypePopulator>(g => new TypePopulator(g.Locate<IConstraintHelper>()));
            Export<ITypeCreator>(g => new TypeCreator(g.Locate<IConstructorSelector>(),g.Locate<IConstraintHelper>()));
            Export<IConstructorSelector>(g => new ConstructorSelector());
            Export<ICircularReferenceHandler>(g => new CircularReferenceHandler());
        }

        public bool UseDefaultConventions { get; set; }

        public bool UseNamedConventions { get; set; }

        public int? ItemCount { get; set; }
    }
}
