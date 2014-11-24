using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFixture.Impl
{
    /// <summary>
    /// Object used to customize a model
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CustomizeModel<T> : ICustomizeModel<T>
    {
        private readonly ComplexModel _complexModel;

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="complexModel"></param>
        public CustomizeModel(ComplexModel complexModel)
        {
            _complexModel = complexModel;
        }

        /// <summary>
        /// Provide function to create new instance of type
        /// </summary>
        /// <param name="newFunc">creation functions</param>
        /// <returns>configuration object</returns>
        public ICustomizeModel<T> New(Func<T> newFunc)
        {
            return New(r => newFunc());
        }

        /// <summary>
        /// Provide customization on creating a new instance of T
        /// </summary>
        /// <param name="newFunc">create func</param>
        /// <returns>configuration object</returns>
        public ICustomizeModel<T> New(Func<DataRequest, T> newFunc)
        {
            _complexModel.New = r => newFunc(r);

            return this;
        }

        /// <summary>
        /// Provide customize creation logic for T
        /// </summary>
        /// <typeparam name="TIn">type of in value</typeparam>
        /// <param name="factory">factory</param>
        /// <returns>configuration object</returns>
        public ICustomizeModel<T> NewFactory<TIn>(Func<TIn, T> factory)
        {
            return New(r =>
                       {
                           var newRequest = new DataRequest(r, typeof(TIn));

                           return factory((TIn)newRequest.Fixture.Generate(newRequest));
                       });
        }

        /// <summary>
        /// Provide customize creation logic for T
        /// </summary>
        /// <typeparam name="TIn1">Value type 1</typeparam>
        /// <typeparam name="TIn2">Value type 2</typeparam>
        /// <param name="factory">creatation logic</param>
        /// <returns>configuration object</returns>
        public ICustomizeModel<T> NewFactory<TIn1, TIn2>(Func<TIn1, TIn2, T> factory)
        {
            return New(r =>
            {
                var newRequest1 = new DataRequest(r, typeof(TIn1));
                var newRequest2 = new DataRequest(r, typeof(TIn2));
                
                return factory((TIn1)newRequest1.Fixture.Generate(newRequest1),
                               (TIn2)newRequest2.Fixture.Generate(newRequest2));
            });
        }

        /// <summary>
        /// Provide customize creation logic for T
        /// </summary>
        /// <typeparam name="TIn1">Value Type 1</typeparam>
        /// <typeparam name="TIn2">Value Type 2</typeparam>
        /// <typeparam name="TIn3">Value Type 3</typeparam>
        /// <param name="factory">creation logic</param>
        /// <returns>configuration object</returns>
        public ICustomizeModel<T> NewFactory<TIn1, TIn2, TIn3>(Func<TIn1, TIn2, TIn3, T> factory)
        {
            return New(r =>
            {
                var newRequest1 = new DataRequest(r, typeof(TIn1));
                var newRequest2 = new DataRequest(r, typeof(TIn2));
                var newRequest3 = new DataRequest(r, typeof(TIn2));
                
                return factory((TIn1)newRequest1.Fixture.Generate(newRequest1),
                               (TIn2)newRequest2.Fixture.Generate(newRequest2),
                               (TIn3)newRequest3.Fixture.Generate(newRequest3));
            });
        }

        /// <summary>
        /// Provide customize creation logic for T
        /// </summary>
        /// <typeparam name="TIn1">Value Type 1</typeparam>
        /// <typeparam name="TIn2">Value Type 1</typeparam>
        /// <typeparam name="TIn3">Value Type 1</typeparam>
        /// <typeparam name="TIn4">Value Type 1</typeparam>
        /// <param name="factory">creation logic</param>
        /// <returns>configuration object</returns>
        public ICustomizeModel<T> NewFactory<TIn1, TIn2, TIn3, TIn4>(Func<TIn1, TIn2, TIn3, TIn4, T> factory)
        {
            return New(r =>
            {
                var newRequest1 = new DataRequest(r, typeof(TIn1));
                var newRequest2 = new DataRequest(r, typeof(TIn2));
                var newRequest3 = new DataRequest(r, typeof(TIn3));
                var newRequest4 = new DataRequest(r, typeof(TIn4));
                
                return factory((TIn1)newRequest1.Fixture.Generate(newRequest1),
                               (TIn2)newRequest2.Fixture.Generate(newRequest2),
                               (TIn3)newRequest3.Fixture.Generate(newRequest3),
                               (TIn4)newRequest4.Fixture.Generate(newRequest4));
            });
        }

        /// <summary>
        /// Set a property to a specific value on creation
        /// </summary>
        /// <typeparam name="TProp">property type</typeparam>
        /// <param name="propertyFunc"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ICustomizeModel<T> Set<TProp>(Expression<Func<T, TProp>> propertyFunc, TProp value)
        {
            return Set(propertyFunc, r => value);
        }

        /// <summary>
        /// Set a property to a specific value on creation
        /// </summary>
        /// <typeparam name="TProp">proeprty type to set</typeparam>
        /// <param name="propertyFunc">property to set</param>
        /// <param name="valueFunc">value function</param>
        /// <returns>configuration object</returns>
        public ICustomizeModel<T> Set<TProp>(Expression<Func<T, TProp>> propertyFunc, Func<TProp> valueFunc)
        {
            return Set(propertyFunc, r => valueFunc());
        }

        /// <summary>
        /// Set a property to a specific value on creation
        /// </summary>
        /// <typeparam name="TProp">property type to set</typeparam>
        /// <param name="propertyFunc">property to set</param>
        /// <param name="valueFunc">value func</param>
        /// <returns>configuration object</returns>
        public ICustomizeModel<T> Set<TProp>(Expression<Func<T, TProp>> propertyFunc, Func<DataRequest, TProp> valueFunc)
        {
            MemberExpression member = propertyFunc.Body as MemberExpression;

            if (member == null)
            {
                throw new ArgumentException("property", "Property must be a property on type" + typeof(T).FullName);
            }

            _complexModel.AddPropertyValue(member.Member.Name,r => valueFunc(r));

            return this;
        }

        /// <summary>
        /// Set a value to a specific set of properties
        /// </summary>
        /// <param name="matchingFunc">matching function</param>
        /// <param name="value">value to set</param>
        /// <returns>configuration object</returns>
        public ICustomizeModel<T> SetProperties(Func<PropertyInfo, bool> matchingFunc, object value)
        {
            return SetProperties(matchingFunc, (r, p) => value);
        }

        /// <summary>
        /// Set a value to a specific set of properties
        /// </summary>
        /// <param name="matchingFunc">matching function</param>
        /// <param name="value">value to set</param>
        /// <returns>configuration object</returns>
        public ICustomizeModel<T> SetProperties(Func<PropertyInfo, bool> matchingFunc, Func<object> value)
        {
            return SetProperties(matchingFunc, (r, p) => value());
        }

        /// <summary>
        /// Set a value to a specific set of properties
        /// </summary>
        /// <param name="matchingFunc">matching function</param>
        /// <param name="value">value set function</param>
        /// <returns>configuration object</returns>
        public ICustomizeModel<T> SetProperties(Func<PropertyInfo, bool> matchingFunc, Func<DataRequest, PropertyInfo, object> value)
        {
            _complexModel.AddPropertiesValue(matchingFunc,value);

            return this;
        }

        /// <summary>
        /// Skip a specific property
        /// </summary>
        /// <typeparam name="TProp">property type that is being skipped</typeparam>
        /// <param name="propertyFunc">property to skip</param>
        /// <returns>configuration object</returns>
        public ICustomizeModel<T> Skip<TProp>(Expression<Func<T, TProp>> propertyFunc)
        {
            MemberExpression member = propertyFunc.Body as MemberExpression;

            if (member == null)
            {
                throw new ArgumentException("property", "Property must be a property on type" + typeof(T).FullName);
            }

            _complexModel.AddSkipProperty(member.Member.Name);

            return this;
        }

        /// <summary>
        /// Specify a set of properties to skip
        /// </summary>
        /// <param name="matchingFunc">property matching function</param>
        /// <returns>configuration object</returns>
        public ICustomizeModel<T> SkipProperties(Func<PropertyInfo, bool> matchingFunc = null)
        {
            if (matchingFunc != null)
            {
                return SkipProperties((r, p) => matchingFunc(p));
            }
             
            return SkipProperties((r, p) => true);
        }

        /// <summary>
        /// Specify properties to skip
        /// </summary>
        /// <param name="matchingFunc">property matching function</param>
        /// <returns>configuration object</returns>
        public ICustomizeModel<T> SkipProperties(Func<DataRequest, PropertyInfo, bool> matchingFunc)
        {
            _complexModel.AddSkipMatchingProperty(matchingFunc);

            return this;
        }

        /// <summary>
        /// Apply an action when creating instance
        /// </summary>
        /// <param name="applyAction"></param>
        /// <returns></returns>
        public ICustomizeModel<T> Apply(Action<T> applyAction)
        {
            _complexModel.AddApply(o => applyAction((T)o));

            return this;
        }
    }
}
