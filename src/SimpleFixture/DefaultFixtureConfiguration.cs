using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleFixture.DI;
using SimpleFixture.Impl;

namespace SimpleFixture
{
    /// <summary>
    /// Default configuration for Fixture
    /// </summary>
    public class DefaultFixtureConfiguration : GContainer, IFixtureConfiguration
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public DefaultFixtureConfiguration()
        {
            UseDefaultConventions = true;

            UseNamedConventions = true;

            SetupDefaults();
        }

        private void SetupDefaults()
        {
            ExportSingleton<IRandomDataGeneratorService>(g => new RandomDataGeneratorService());
            ExportSingleton<IConstraintHelper>(g => new ConstraintHelper());
            ExportSingleton<IModelService>(g => new ModelService());
            Export<IPropertySetter>(g => new PropertySetter());
            Export<IConventionProvider>(g => new ConventionProvider());
            Export<IConventionList>(g => new ConventionList());
            Export<ITypePropertySelector>(g => new TypePropertySelector(g.Locate<IConstraintHelper>()));
            Export<ITypePopulator>(g => new TypePopulator(g.Locate<IConstraintHelper>(),g.Locate<ITypePropertySelector>(), g.Locate<IPropertySetter>()));
            Export<ITypeCreator>(g => new TypeCreator(g.Locate<IConstructorSelector>(),g.Locate<IConstraintHelper>()));
            Export<IConstructorSelector>(g => new ConstructorSelector());
            Export<ICircularReferenceHandler>(g => new CircularReferenceHandler());
        }

        /// <summary>
        /// Use default conventions, true by default
        /// </summary>
        public bool UseDefaultConventions { get; set; }

        /// <summary>
        /// Use named conventions, true by default
        /// </summary>
        public bool UseNamedConventions { get; set; }

        /// <summary>
        /// Item count controls the how many instances should be constructed when populating enumerables
        /// </summary>
        public int? ItemCount { get; set; }
    }
}
