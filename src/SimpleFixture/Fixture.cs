using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private Dictionary<Type, IConventionList> _typedConventions;
        private IConventionList _conventions;
        private readonly IFixtureConfiguration _configuration;
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="configuration"></param>
        public Fixture(IFixtureConfiguration configuration = null)
        {
            _configuration = configuration ?? new DefaultFixtureConfiguration();

            Initalize();
        }
        #endregion

        #region Configuration
        public IFixtureConfiguration Configuration
        {
            get { return _configuration; }
        }
        #endregion

        #region Locate
        /// <summary>
        /// Creates a new instance of the specified type. It does not populate any properties
        /// </summary>
        /// <param name="type">type to create</param>
        /// <returns>new instance</returns>
        public object Locate(Type type)
        {
            DataRequest request = new DataRequest(null, this, type, string.Empty, false, null, null);

            return Generate(request);
        }

        /// <summary>
        /// Creates a new instance of T. It does not populate any properties
        /// </summary>
        /// <typeparam name="T">type to create</typeparam>
        /// <returns>new instance</returns>
        public T Locate<T>()
        {
            return (T)Locate(typeof(T));
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
            object returnValue;
            IConventionList conventionList;

            if (_typedConventions.TryGetValue(request.RequestedType, out conventionList))
            {
                if (conventionList.TryGetValue(request, out returnValue))
                {
                    return returnValue;
                }
            }

            if (request.RequestedType.IsConstructedGenericType)
            {
                Type openType = request.RequestedType.GetGenericTypeDefinition();

                if (_typedConventions.TryGetValue(openType, out conventionList))
                {
                    if (conventionList.TryGetValue(request, out returnValue))
                    {
                        return returnValue;
                    }
                }
            }

            if (_conventions.TryGetValue(request, out returnValue))
            {
                return returnValue;
            }

            throw new Exception("Could not construct type: " + request.RequestedType.FullName);
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
            if (name == null)
            {
                name = string.Empty;
            }

            DataRequest request = new DataRequest(null, this, type, name, true, constraints, null);

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
                throw new ArgumentNullException("instance");
            }

            var typePopulator = Configuration.Locate<ITypePopulator>();

            DataRequest request = new DataRequest(null, this, instance.GetType(), string.Empty, true, constraints, null);

            typePopulator.Populate(instance, request);
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
                throw new ArgumentNullException("returnValues", "you must provide at least one value");
            }

            int i = 0;

            var convention = new FilteredConvention<T>(g => returnValues[i++ % returnValues.Length]);

            Add(convention);

            return new ReturnConfiguration<T>(convention);
        }

        /// <summary>
        /// Provide a function that will be invoked when T is need
        /// </summary>
        /// <typeparam name="T">type to return</typeparam>
        /// <param name="returnFunc">return function</param>
        /// <returns>configuration object</returns>
        public ReturnConfiguration<T> Return<T>(Func<T> returnFunc)
        {
            var convention = new FilteredConvention<T>(g => returnFunc());

            return new ReturnConfiguration<T>(convention);
        }

        /// <summary>
        /// Provide a function that will be invoked when T is need
        /// </summary>
        /// <typeparam name="T">type to return</typeparam>
        /// <param name="returnFunc">return function</param>
        /// <returns>configuration object</returns>
        public ReturnConfiguration<T> Return<T>(Func<DataRequest, T> returnFunc)
        {
            var convention = new FilteredConvention<T>(returnFunc);

            return new ReturnConfiguration<T>(convention);
        }
        #endregion

        #region Add

        /// <summary>
        /// Add a convention to fixture
        /// </summary>
        /// <param name="convention">new convention</param>
        public void Add(IConvention convention)
        {
            var typedConvention = convention as ITypedConvention;

            if (typedConvention != null)
            {
                IConventionList conventionList;

                foreach (Type supportedType in typedConvention.SupportedTypes)
                {
                    if (!_typedConventions.TryGetValue(supportedType, out conventionList))
                    {
                        conventionList = _configuration.Locate<IConventionList>();
                        _typedConventions[supportedType] = conventionList;
                    }

                    conventionList.AddConvention(convention);
                }
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
            configuration.Customize(this);
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
            _conventions = _configuration.Locate<IConventionList>();

            _typedConventions = new Dictionary<Type, IConventionList>();

            IConventionProvider conventionProvider = _configuration.Locate<IConventionProvider>();

            foreach (IConvention providedConvention in conventionProvider.ProvideConventions(Configuration))
            {
                Add(providedConvention);
            }
        }

        #endregion
    }
}
