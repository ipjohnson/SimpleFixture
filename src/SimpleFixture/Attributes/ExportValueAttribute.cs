using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SimpleFixture.Attributes
{
    /// <summary>
    /// Set a particular simple value through attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class ExportValueAttribute : FixtureInitializationAttribute, IConvention
    {
        private readonly string _key;
        private readonly object _value;

        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public ExportValueAttribute(string key, object value)
        {
            _key = key;
            _value = value;
        }

        /// <summary>Initialize fixture</summary>
        /// <param name="fixture">fixture</param>
        public override void Initialize(Fixture fixture)
        {
            fixture.Add(this);
        }

        /// <inheritdoc/>
        public ConventionPriority Priority => ConventionPriority.High;

        /// <inheritdoc/>
        public event EventHandler<PriorityChangedEventArgs> PriorityChanged;

        /// <inheritdoc />
        public object GenerateData(DataRequest request)
        {
            return request.RequestName == _key &&
                   request.RequestedType.IsAssignableFrom(_value.GetType()) ?
                _value :
                Convention.NoValue;
        }
    }
}
