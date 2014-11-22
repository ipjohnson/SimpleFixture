using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFixture
{
    public interface ICustomizeModel<T>
    {
        ICustomizeModel<T> New(Func<T> newFunc);

        ICustomizeModel<T> New(Func<DataRequest, T> newFunc);

        ICustomizeModel<T> NewFactory<TIn>(Func<TIn, T> factory);

        ICustomizeModel<T> NewFactory<TIn1, TIn2>(Func<TIn1, TIn2, T> factory);

        ICustomizeModel<T> NewFactory<TIn1, TIn2, TIn3>(Func<TIn1, TIn2, TIn3, T> factory);

        ICustomizeModel<T> NewFactory<TIn1, TIn2, TIn3, TIn4>(Func<TIn1, TIn2, TIn3, TIn4, T> factory);

        ICustomizeModel<T> Set<TProp>(Expression<Func<T, TProp>> propertyFunc, TProp value);

        ICustomizeModel<T> Set<TProp>(Expression<Func<T, TProp>> propertyFunc, Func<TProp> valueFunc);

        ICustomizeModel<T> Set<TProp>(Expression<Func<T, TProp>> propertyFunc, Func<DataRequest, TProp> valueFunc);

        ICustomizeModel<T> SetProperties(Func<PropertyInfo, bool> matchingFunc, object value);

        ICustomizeModel<T> SetProperties(Func<PropertyInfo, bool> matchingFunc, Func<object> value);

        ICustomizeModel<T> SetProperties(Func<PropertyInfo, bool> matchingFunc, Func<DataRequest, PropertyInfo, object> value);

        ICustomizeModel<T> Skip<TProp>(Expression<Func<T, TProp>> propertyFunc);

        ICustomizeModel<T> SkipProperties(Func<PropertyInfo, bool> matchingFunc);

        ICustomizeModel<T> SkipProperties(Func<DataRequest, PropertyInfo, bool> matchingFunc);

        ICustomizeModel<T> Apply(Action<T> applyAction);
    }
}
