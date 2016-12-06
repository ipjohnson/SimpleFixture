using System;
using System.Linq.Expressions;
using System.Reflection;

namespace SimpleFixture
{
    /// <summary>
    /// Interface for customizing how a Type gets created
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICustomizeModel<T>
    {
        /// <summary>
        /// Provide a function for creating a new instance of T
        /// </summary>
        /// <param name="newFunc">function to create new T</param>
        /// <returns>customization instance</returns>
        ICustomizeModel<T> New(Func<T> newFunc);

        /// <summary>
        /// Provide a function for creating a new instance of T
        /// </summary>
        /// <param name="newFunc">delegate that accepts a data request and returns T</param>
        /// <returns>customization instance</returns>
        ICustomizeModel<T> New(Func<DataRequest, T> newFunc);

        /// <summary>
        /// Provide function for creating a new instance of T that depends on TIn
        /// </summary>
        /// <typeparam name="TIn">type for dependency</typeparam>
        /// <param name="factory">new factory that takes TIn</param>
        /// <returns>customization instance</returns>
        ICustomizeModel<T> NewFactory<TIn>(Func<TIn, T> factory);

        /// <summary>
        /// Provide function for creating a new instance of T that depends on TIn1 and TIn2
        /// </summary>
        /// <typeparam name="TIn1">first dependency type</typeparam>
        /// <typeparam name="TIn2">second dependency type</typeparam>
        /// <param name="factory">new factory that takes TIn1 and TIn2</param>
        /// <returns>customization instance</returns>
        ICustomizeModel<T> NewFactory<TIn1, TIn2>(Func<TIn1, TIn2, T> factory);

        /// <summary>
        /// Provide function for creating a new instance of T that depends on TIn1, TIn2, and TIn3
        /// </summary>
        /// <typeparam name="TIn1">first dependency type</typeparam>
        /// <typeparam name="TIn2">second dependency type</typeparam>
        /// <typeparam name="TIn3">third dependency type</typeparam>
        /// <param name="factory">new factory that takes TIn1, TIn2, and TIn3</param>
        /// <returns>customization instance</returns>
        ICustomizeModel<T> NewFactory<TIn1, TIn2, TIn3>(Func<TIn1, TIn2, TIn3, T> factory);

        /// <summary>
        /// Provide function for creating a new instance of T that depends on TIn1, TIn2, TIn3, and TIn4
        /// </summary>
        /// <typeparam name="TIn1">first dependency type</typeparam>
        /// <typeparam name="TIn2">second dependency type</typeparam>
        /// <typeparam name="TIn3">third dependency type</typeparam>
        /// <typeparam name="TIn4">fourth dependency type</typeparam>
        /// <param name="factory">new factory that takes TIn1, TIn2, TIn3, and TIn4</param>
        /// <returns>customization instance</returns>
        ICustomizeModel<T> NewFactory<TIn1, TIn2, TIn3, TIn4>(Func<TIn1, TIn2, TIn3, TIn4, T> factory);

        /// <summary>
        /// Set a value for a particular property on T
        /// </summary>
        /// <typeparam name="TProp">type of property to set</typeparam>
        /// <param name="propertyFunc">method to specify property (x => x.PropertyName)</param>
        /// <param name="value">value to use when setting property</param>
        /// <returns>customization instance</returns>
        ICustomizeModel<T> Set<TProp>(Expression<Func<T, TProp>> propertyFunc, TProp value);

        /// <summary>
        /// Set a value for a particular property on T
        /// </summary>
        /// <typeparam name="TProp">type of property to set</typeparam>
        /// <param name="propertyFunc">method to specify property (x => x.PropertyName)</param>
        /// <param name="valueFunc">function to provide value</param>
        /// <returns>customization instance</returns>
        ICustomizeModel<T> Set<TProp>(Expression<Func<T, TProp>> propertyFunc, Func<TProp> valueFunc);

        /// <summary>
        /// Set a value for a particular property on T
        /// </summary>
        /// <typeparam name="TProp">type of property to set</typeparam>
        /// <param name="propertyFunc">method to specify property (x => x.PropertyName)</param>
        /// <param name="valueFunc">function to provide value</param>
        /// <returns>customization instance</returns>
        ICustomizeModel<T> Set<TProp>(Expression<Func<T, TProp>> propertyFunc, Func<DataRequest, TProp> valueFunc);

        /// <summary>
        /// Set a specific value into a set of Properties specified by the matching func
        /// </summary>
        /// <param name="matchingFunc">property matching func</param>
        /// <param name="value">property value</param>
        /// <returns>customization instance</returns>
        ICustomizeModel<T> SetProperties(Func<PropertyInfo, bool> matchingFunc, object value);

        /// <summary>
        /// Set a specific value into a set of Properties specified by the matching func
        /// </summary>
        /// <param name="matchingFunc">property matching func</param>
        /// <param name="value">func to be used to provide value</param>
        /// <returns>customization instance</returns>
        ICustomizeModel<T> SetProperties(Func<PropertyInfo, bool> matchingFunc, Func<object> value);

        /// <summary>
        /// Set a specific value into a set of Properties specified by the matching func
        /// </summary>
        /// <param name="matchingFunc">property matching func</param>
        /// <param name="value">func to be used to provide value</param>
        /// <returns>customization instance</returns>
        ICustomizeModel<T> SetProperties(Func<PropertyInfo, bool> matchingFunc, Func<DataRequest, PropertyInfo, object> value);

        /// <summary>
        /// Skip a particular property from being populated
        /// </summary>
        /// <typeparam name="TProp">property type</typeparam>
        /// <param name="propertyFunc">property expression</param>
        /// <returns>customization instance</returns>
        ICustomizeModel<T> Skip<TProp>(Expression<Func<T, TProp>> propertyFunc);

        /// <summary>
        /// Skip a particular set of properties
        /// </summary>
        /// <param name="matchingFunc">property matching function</param>
        /// <returns>customization instance</returns>
        ICustomizeModel<T> SkipProperties(Func<PropertyInfo, bool> matchingFunc);

        /// <summary>
        /// Skip a particular set of properties
        /// </summary>
        /// <param name="matchingFunc">property matching function</param>
        /// <returns>customization instance</returns>
        ICustomizeModel<T> SkipProperties(Func<DataRequest, PropertyInfo, bool> matchingFunc);

        /// <summary>
        /// Apply a piece of logic to each instance being created
        /// </summary>
        /// <param name="applyAction">apply function</param>
        /// <returns>customization instanc</returns>
        ICustomizeModel<T> Apply(Action<T> applyAction);
    }
}
