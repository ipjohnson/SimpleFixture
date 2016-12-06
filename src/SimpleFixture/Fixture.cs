using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SimpleFixture.Conventions;
using SimpleFixture.Impl;

namespace SimpleFixture
{
    /// <summary>
    /// Testing fixture
    /// </summary>
    public class Fixture : IEnumerable<object>
    {
        #region fields
        protected readonly BehaviorCollection _behavior;
        protected readonly TypedConventions _typedConventions;
        protected readonly TypedConventions _returnConventions;
        protected readonly IConventionList _conventions;
        protected readonly IFixtureConfiguration _configuration;
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="configuration"></param>
        public Fixture(IFixtureConfiguration configuration = null)
        {
            _configuration = configuration ?? new DefaultFixtureConfiguration();

            _conventions = _configuration.Locate<IConventionList>();

            _behavior = new BehaviorCollection();

            _returnConventions = new TypedConventions(Configuration, ConventionPriority.First);

            _typedConventions = new TypedConventions(Configuration);

            Initalize();
        }
        #endregion

        #region Configuration

        /// <summary>
        /// Configuration for the fixture
        /// </summary>
        public IFixtureConfiguration Configuration => _configuration;

        #endregion

        #region Behaviors

        /// <summary>
        /// Allows you to apply a behavior to every object created by the fixture
        /// </summary>
        public BehaviorCollection Behavior => _behavior;

        #endregion

        #region Locate

        /// <summary>
        /// Creates a new instance of the specified type. It does not populate any properties
        /// </summary>
        /// <param name="type">type to create</param>
        /// <param name="requestName"></param>
        /// <param name="constraints"></param>
        /// <returns>new instance</returns>
        public object Locate(Type type, string requestName = null, object constraints = null)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            var request = new DataRequest(null, this, type, DependencyType.Root, requestName, false, constraints, null);

            return Generate(request);
        }

        /// <summary>
        /// Creates a new instance of T. It does not populate any properties
        /// </summary>
        /// <typeparam name="T">type to create</typeparam>
        /// <returns>new instance</returns>
        public T Locate<T>(string requestName = null, object constraints = null)
        {
            return (T)Locate(typeof(T), requestName, constraints);
        }
        #endregion

        #region Generate

        /// <summary>
        /// Create a new instance of the requested type and populate all public writable properties
        /// </summary>
        /// <param name="request">data request</param>
        /// <returns>new instance</returns>
        public object Generate(DataRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            object returnValue = null;

            _conventions.TryGetValue(request, out returnValue);

            if (returnValue == null)
            {
                throw new Exception("Could not construct type: " + request.RequestedType.FullName);
            }

            returnValue = _behavior.Apply(request, returnValue);

            return returnValue;
        }

        /// <summary>
        /// Generate a new instance of the specified type and populate public writable properties
        /// </summary>
        /// <param name="type">type to create</param>
        /// <param name="name">name of the request</param>
        /// <param name="constraints">constraints to apply to the request</param>
        /// <returns>new instance</returns>
        public object Generate(Type type, string name = null, object constraints = null)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            if (name == null)
            {
                name = string.Empty;
            }

            var request = new DataRequest(null, this, type, DependencyType.Root, name, true, constraints, null);

            return Generate(request);
        }

        /// <summary>
        /// Generate a new instance of T and populate public writable properties
        /// </summary>
        /// <typeparam name="T">type to create</typeparam>
        /// <param name="name">request name</param>
        /// <param name="constraints">constraint object</param>
        /// <returns></returns>
        public T Generate<T>(string name = null, object constraints = null)
        {
            return (T)Generate(typeof(T), name, constraints);
        }

        #endregion

        #region Freeze

        /// <summary>
        /// Generate a new value and add it to the Fixture as a Return
        /// </summary>
        /// <typeparam name="T">type to generate</typeparam>
        /// <param name="requestName">request name</param>
        /// <param name="constraints">constraints for generate</param>
        /// <param name="value">action to specify when to use the froozen value (value: i => i.For&lt;T&gt;)</param>
        /// <returns>new T</returns>
        public T Freeze<T>(string requestName = null, object constraints = null, Action<ReturnConfiguration<T>> value = null)
        {
            var returnValue = Generate<T>(requestName, constraints);

            var returnStatement = Return(returnValue);

            value?.Invoke(returnStatement);

            return returnValue;
        }

        #endregion

        #region Populate

        /// <summary>
        /// Populate writable properties on instance
        /// </summary>
        /// <param name="instance">instance to populate</param>
        /// <param name="constraints">constraint object</param>
        public void Populate(object instance, object constraints = null)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            var modelService = Configuration.Locate<IModelService>();

            var typePopulator = Configuration.Locate<ITypePopulator>();

            var request = new DataRequest(null, this, instance.GetType(), DependencyType.Root, string.Empty, true, constraints, null);

            typePopulator.Populate(instance, request, modelService.GetModel(instance.GetType()));
        }

        #endregion

        #region Return

        /// <summary>
        /// Return the specified sequence
        /// </summary>
        /// <typeparam name="T">Type to return</typeparam>
        /// <param name="returnValues">return values</param>
        /// <returns>configuration object</returns>
        public ReturnConfiguration<T> Return<T>(params T[] returnValues)
        {
            if (returnValues == null || returnValues.Length == 0)
            {
                throw new ArgumentNullException(nameof(returnValues), "you must provide at least one value");
            }

            var i = 0;

            var convention = new FilteredConvention<T>(g => returnValues[i++ % returnValues.Length]);

            _returnConventions.AddConvention(convention);

            return new ReturnConfiguration<T>(convention, this);
        }

        /// <summary>
        /// Provide a function that will be invoked when T is need
        /// </summary>
        /// <typeparam name="T">type to return</typeparam>
        /// <param name="returnFunc">return function</param>
        /// <returns>configuration object</returns>
        public ReturnConfiguration<T> Return<T>(Func<T> returnFunc)
        {
            if (returnFunc == null) throw new ArgumentNullException(nameof(returnFunc));

            var convention = new FilteredConvention<T>(g => returnFunc());

            _returnConventions.AddConvention(convention);

            return new ReturnConfiguration<T>(convention, this);
        }

        /// <summary>
        /// Provide a function that will be invoked when T is need
        /// </summary>
        /// <typeparam name="T">type to return</typeparam>
        /// <param name="returnFunc">return function</param>
        /// <returns>configuration object</returns>
        public ReturnConfiguration<T> Return<T>(Func<DataRequest, T> returnFunc)
        {
            if (returnFunc == null) throw new ArgumentNullException(nameof(returnFunc));
            var convention = new FilteredConvention<T>(returnFunc);

            _returnConventions.AddConvention(convention);

            return new ReturnConfiguration<T>(convention, this);
        }

        /// <summary>
        /// Return a set of T as an IEnumerable&lt;T&gt;
        /// </summary>
        /// <typeparam name="T">T Type for IEnumerable</typeparam>
        /// <param name="set">set of T</param>
        /// <returns>configuration object</returns>
        public ReturnConfiguration<IEnumerable<T>> ReturnIEnumerable<T>(params T[] set)
        {
            if (set == null) throw new ArgumentNullException(nameof(set));
            
            var convention = new FilteredConvention<IEnumerable<T>>(request => set);

            _returnConventions.AddConvention(convention);

            return new ReturnConfiguration<IEnumerable<T>>(convention, this);
        }

        #endregion

        #region Export
        
        /// <summary>
        /// Export specific implementation as an interface, you must call As after
        /// </summary>
        /// <typeparam name="T">Type being exported</typeparam>
        /// <returns></returns>
        public ExportAs<T> Export<T>()
        {
            return new ExportAs<T>(this, false);
        }

        /// <summary>
        /// Export a type as something
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TExport"></typeparam>
        /// <returns></returns>
        public ReturnConfiguration<TExport> ExportAs<T,TExport>() where T : TExport
        {
            return new ExportAs<T>(this, false).As<TExport>();
        }

        /// <summary>
        /// Export specific implemantion as a singleton interface, you must call As after
        /// </summary>
        /// <typeparam name="T">type being exported</typeparam>
        /// <returns></returns>
        public ExportAs<T> ExportSingleton<T>()
        {
            return new ExportAs<T>(this, true);   
        }

        /// <summary>
        /// Export specific implemantion as a singleton interface
        /// </summary>
        /// <typeparam name="T">type being exported</typeparam>
        /// <typeparam name="TExport">exporting interface</typeparam>
        /// <returns></returns>
        public ReturnConfiguration<TExport> ExportSingletonAs<T, TExport>() where T : TExport
        {
            return new ExportAs<T>(this, true).As<TExport>();
        }

        /// <summary>
        /// Export a set of types by interface
        /// </summary>
        public FromConfiguration ExportAllByInterface()
        {
            return new FromConfiguration(this);
        }

        #endregion

        #region Add

        /// <summary>
        /// Add a convention to fixture
        /// </summary>
        /// <param name="convention">new convention</param>
        public void Add(IConvention convention)
        {
            if (convention == null) throw new ArgumentNullException(nameof(convention));

            var typedConvention = convention as ITypedConvention;

            if (typedConvention != null)
            {
                _typedConventions.AddConvention(typedConvention);
            }
            else
            {
                _conventions.AddConvention(convention);
            }
        }

        /// <summary>
        /// Add customization
        /// </summary>
        /// <param name="configuration"></param>
        public void Add(IFixtureCustomization configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            configuration.Customize(this);
        }
        #endregion

        #region Customize

        /// <summary>
        /// Customize the creation of a particular type
        /// </summary>
        /// <typeparam name="T">type ot customize</typeparam>
        /// <returns>customize object</returns>
        public ICustomizeModel<T> Customize<T>()
        {
            var service = Configuration.Locate<IModelService>();

            var model = service.GetModel(typeof(T));

            return new CustomizeModel<T>(model);
        }

        #endregion

        #region Enumerator
        /// <summary>
        /// Get enumerator
        /// </summary>
        /// <returns></returns>
        public IEnumerator<object> GetEnumerator()
        {
            return Enumerable.Empty<object>().GetEnumerator();
        }

        /// <summary>
        /// Get enumerator
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

        #region Non Public Members

        private void Initalize()
        {            
            Add(_returnConventions);

            Add(_typedConventions);

            var conventionProvider = _configuration.Locate<IConventionProvider>();

            foreach (var providedConvention in conventionProvider.ProvideConventions(Configuration))
            {
                Add(providedConvention);
            }

            // using specific type incase it's inheritance
            Return<Fixture>(this);

            Return(_configuration.Locate<IRandomDataGeneratorService>());
        }

        #endregion
    }
}
