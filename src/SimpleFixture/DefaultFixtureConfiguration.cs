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
        /// Configuration with circular reference handling set to AutoWire
        /// </summary>
        public static DefaultFixtureConfiguration AutoWire => new DefaultFixtureConfiguration { CircularReferenceHandling = CircularReferenceHandlingAlgorithm.AutoWire };

        /// <summary>
        /// Configuration with circular refernce handling set to omit
        /// </summary>
        public static DefaultFixtureConfiguration OmitCircularReferences => new DefaultFixtureConfiguration { CircularReferenceHandling = CircularReferenceHandlingAlgorithm.OmitCircularReferences };

        /// <summary>
        /// Default constructor
        /// </summary>
        public DefaultFixtureConfiguration()
        {
            UseDefaultConventions = true;

            UseNamedConventions = true;

            PopulateProperties = true;

            PopulateFields = false;

            CircularReferenceHandling = CircularReferenceHandlingAlgorithm.MaxDepth;

            SetupDefaults();
        }

        private void SetupDefaults()
        {
            ExportSingleton<IRandomDataGeneratorService>(g => new RandomDataGeneratorService());
            ExportSingleton<IConstraintHelper>(g => new ConstraintHelper());
            ExportSingleton<IModelService>(g => new ModelService());
            Export<IPropertySetter>(g => new PropertySetter());
            Export<IFieldSetter>(g => new FieldSetter());
            Export<IConventionProvider>(g => new ConventionProvider());
            Export<IConventionList>(g => new ConventionList());
            Export<ITypeCreator>(g => new TypeCreator(this, g.Locate<IConstructorSelector>(), g.Locate<IConstraintHelper>()));
            Export<IConstructorSelector>(g => new ConstructorSelector());
            Export<ICircularReferenceHandler>(g => new CircularReferenceHandler());
            Export<ITypePropertySelector>(g => new TypePropertySelector(this, g.Locate<IConstraintHelper>()));
            Export<ITypeFieldSelector>(g => new TypeFieldSelector(g.Locate<IConstraintHelper>()));
            Export<ITypePopulator>(g => new TypePopulator(this,
                                                          g.Locate<IConstraintHelper>(),
                                                          g.Locate<ITypePropertySelector>(),
                                                          g.Locate<IPropertySetter>(),
                                                          g.Locate<ITypeFieldSelector>(),
                                                          g.Locate<IFieldSetter>()));
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

        /// <summary>
        /// Populate properties
        /// </summary>
        public bool PopulateProperties { get; set; }

        /// <summary>
        /// Populate public fields
        /// </summary>
        public bool PopulateFields { get; set; }

        /// <summary>
        /// Configure how to handle circular references
        /// </summary>
        public CircularReferenceHandlingAlgorithm CircularReferenceHandling { get; set; }
    }
}
