using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFixture.Impl
{
    public class CustomizeModel<T> : ICustomizeModel<T>
    {
        private readonly ComplexModel _complexModel;

        public CustomizeModel(ComplexModel complexModel)
        {
            _complexModel = complexModel;
        }

        public ICustomizeModel<T> New(Func<DataRequest, T> newFunc)
        {
            _complexModel.New = r => newFunc(r);

            return this;
        }

        public ICustomizeModel<T> NewFactory<TIn>(Func<TIn, T> factory)
        {
            return New(r =>
                       {
                           var newRequest = new DataRequest(r, typeof(TIn));

                           return factory((TIn)newRequest.Fixture.Generate(newRequest));
                       });
        }

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

        public ICustomizeModel<T> New(Func<T> newFunc)
        {
            return New(r => newFunc());
        }

        public ICustomizeModel<T> Set<TProp>(Expression<Func<T, TProp>> propertyFunc, TProp value)
        {
            return Set(propertyFunc, r => value);
        }

        public ICustomizeModel<T> Set<TProp>(Expression<Func<T, TProp>> propertyFunc, Func<TProp> valueFunc)
        {
            return Set(propertyFunc, r => valueFunc());
        }

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

        public ICustomizeModel<T> SetProperties(Func<PropertyInfo, bool> matchingFunc, object value)
        {
            return SetProperties(matchingFunc, (r, p) => value);
        }

        public ICustomizeModel<T> SetProperties(Func<PropertyInfo, bool> matchingFunc, Func<object> value)
        {
            return SetProperties(matchingFunc, (r, p) => value());
        }

        public ICustomizeModel<T> SetProperties(Func<PropertyInfo, bool> matchingFunc, Func<DataRequest, PropertyInfo, object> value)
        {
            _complexModel.AddPropertiesValue(matchingFunc,value);

            return this;
        }

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

        public ICustomizeModel<T> SkipProperties(Func<PropertyInfo, bool> matchingFunc)
        {
            return SkipProperties((r, p) => matchingFunc(p));
        }

        public ICustomizeModel<T> SkipProperties(Func<DataRequest, PropertyInfo, bool> matchingFunc)
        {
            _complexModel.AddSkipMatchingProperty(matchingFunc);

            return this;
        }

        public ICustomizeModel<T> Apply(Action<T> applyAction)
        {
            _complexModel.AddApply(o => applyAction((T)o));

            return this;
        }
    }
}
