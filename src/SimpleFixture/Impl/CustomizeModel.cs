using System;
using System.Linq.Expressions;
using System.Reflection;

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
        /// Provide a function for creating a new instance of T
        /// </summary>
        /// <param name="newFunc">function to create new T</param>
        /// <returns>customization instance</returns>
        public ICustomizeModel<T> New(Func<T> newFunc)
        {
            return New(r => newFunc());
        }

        /// <summary>
        /// Provide a function for creating a new instance of T
        /// </summary>
        /// <param name="newFunc">delegate that accepts a data request and returns T</param>
        /// <returns>customization instance</returns>
        public ICustomizeModel<T> New(Func<DataRequest, T> newFunc)
        {
            _complexModel.New = r => newFunc(r);

            return this;
        }

        /// <summary>
        /// Provide function for creating a new instance of T that depends on TIn
        /// </summary>
        /// <typeparam name="TIn">type for dependency</typeparam>
        /// <param name="factory">new factory that takes TIn</param>
        /// <returns>customization instance</returns>
        public ICustomizeModel<T> NewFactory<TIn>(Func<TIn, T> factory)
        {
            return New(r =>
                       {
                           var newRequest = new DataRequest(r, typeof(TIn));

                           return factory((TIn)newRequest.Fixture.Generate(newRequest));
                       });
        }

        /// <summary>
        /// Provide function for creating a new instance of T that depends on TIn1 and TIn2
        /// </summary>
        /// <typeparam name="TIn1">first dependency type</typeparam>
        /// <typeparam name="TIn2">second dependency type</typeparam>
        /// <param name="factory">new factory that takes TIn1 and TIn2</param>
        /// <returns>customization instance</returns>
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
        /// Provide function for creating a new instance of T that depends on TIn1, TIn2, and TIn3
        /// </summary>
        /// <typeparam name="TIn1">first dependency type</typeparam>
        /// <typeparam name="TIn2">second dependency type</typeparam>
        /// <typeparam name="TIn3">third dependency type</typeparam>
        /// <param name="factory">new factory that takes TIn1, TIn2, and TIn3</param>
        /// <returns>customization instance</returns>
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
        /// Provide function for creating a new instance of T that depends on TIn1, TIn2, TIn3, and TIn4
        /// </summary>
        /// <typeparam name="TIn1">first dependency type</typeparam>
        /// <typeparam name="TIn2">second dependency type</typeparam>
        /// <typeparam name="TIn3">third dependency type</typeparam>
        /// <typeparam name="TIn4">fourth dependency type</typeparam>
        /// <param name="factory">new factory that takes TIn1, TIn2, TIn3, and TIn4</param>
        /// <returns>customization instance</returns>
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
        /// Set a value for a particular property on T
        /// </summary>
        /// <typeparam name="TProp">type of property to set</typeparam>
        /// <param name="propertyFunc">method to specify property (x => x.PropertyName)</param>
        /// <param name="value">value to use when setting property</param>
        /// <returns>customization instance</returns>
        public ICustomizeModel<T> Set<TProp>(Expression<Func<T, TProp>> propertyFunc, TProp value)
        {
            return Set(propertyFunc, r => value);
        }

        /// <summary>
        /// Set a value for a particular property on T
        /// </summary>
        /// <typeparam name="TProp">type of property to set</typeparam>
        /// <param name="propertyFunc">method to specify property (x => x.PropertyName)</param>
        /// <param name="valueFunc">function to provide value</param>
        /// <returns>customization instance</returns>
        public ICustomizeModel<T> Set<TProp>(Expression<Func<T, TProp>> propertyFunc, Func<TProp> valueFunc)
        {
            return Set(propertyFunc, r => valueFunc());
        }

        /// <summary>
        /// Set a value for a particular property on T
        /// </summary>
        /// <typeparam name="TProp">type of property to set</typeparam>
        /// <param name="propertyFunc">method to specify property (x => x.PropertyName)</param>
        /// <param name="valueFunc">function to provide value</param>
        /// <returns>customization instance</returns>
        public ICustomizeModel<T> Set<TProp>(Expression<Func<T, TProp>> propertyFunc, Func<DataRequest, TProp> valueFunc)
        {
            var member = propertyFunc.Body as MemberExpression;

            if (member == null)
            {
                throw new ArgumentException("property", "Property must be a property on type" + typeof(T).FullName);
            }

            _complexModel.AddPropertyValue(member.Member.Name,r => valueFunc(r));

            return this;
        }

        /// <summary>
        /// Set a specific value into a set of Properties specified by the matching func
        /// </summary>
        /// <param name="matchingFunc">property matching func</param>
        /// <param name="value">property value</param>
        /// <returns>customization instance</returns>
        public ICustomizeModel<T> SetProperties(Func<PropertyInfo, bool> matchingFunc, object value)
        {
            return SetProperties(matchingFunc, (r, p) => value);
        }

        /// <summary>
        /// Set a specific value into a set of Properties specified by the matching func
        /// </summary>
        /// <param name="matchingFunc">property matching func</param>
        /// <param name="value">func to be used to provide value</param>
        /// <returns>customization instance</returns>
        public ICustomizeModel<T> SetProperties(Func<PropertyInfo, bool> matchingFunc, Func<object> value)
        {
            return SetProperties(matchingFunc, (r, p) => value());
        }

        /// <summary>
        /// Set a specific value into a set of Properties specified by the matching func
        /// </summary>
        /// <param name="matchingFunc">property matching func</param>
        /// <param name="value">func to be used to provide value</param>
        /// <returns>customization instance</returns>
        public ICustomizeModel<T> SetProperties(Func<PropertyInfo, bool> matchingFunc, Func<DataRequest, PropertyInfo, object> value)
        {
            _complexModel.AddPropertiesValue(matchingFunc,value);

            return this;
        }

        /// <summary>
        /// Skip a particular property from being populated
        /// </summary>
        /// <typeparam name="TProp">property type</typeparam>
        /// <param name="propertyFunc">property expression</param>
        /// <returns>customization instance</returns>
        public ICustomizeModel<T> Skip<TProp>(Expression<Func<T, TProp>> propertyFunc)
        {
            var member = propertyFunc.Body as MemberExpression;

            if (member == null)
            {
                throw new ArgumentException("property", "Property must be a property on type" + typeof(T).FullName);
            }

            _complexModel.AddSkipProperty(member.Member.Name);

            return this;
        }

        /// <summary>
        /// Skip a particular set of properties
        /// </summary>
        /// <param name="matchingFunc">property matching function</param>
        /// <returns>customization instance</returns>
        public ICustomizeModel<T> SkipProperties(Func<PropertyInfo, bool> matchingFunc = null)
        {
            if (matchingFunc != null)
            {
                return SkipProperties((r, p) => matchingFunc(p));
            }
             
            return SkipProperties((r, p) => true);
        }

        /// <summary>
        /// Skip a particular set of properties
        /// </summary>
        /// <param name="matchingFunc">property matching function</param>
        /// <returns>customization instance</returns>
        public ICustomizeModel<T> SkipProperties(Func<DataRequest, PropertyInfo, bool> matchingFunc)
        {
            _complexModel.AddSkipMatchingProperty(matchingFunc);

            return this;
        }

        /// <summary>
        /// Apply a piece of logic to each instance being created
        /// </summary>
        /// <param name="applyAction">apply function</param>
        /// <returns>customization instanc</returns>
        public ICustomizeModel<T> Apply(Action<T> applyAction)
        {
            _complexModel.AddApply(o => applyAction((T)o));

            return this;
        }
    }
}
