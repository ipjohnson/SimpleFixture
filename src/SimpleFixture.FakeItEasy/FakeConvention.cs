using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using FakeItEasy.Creation;
using SimpleFixture.Impl;

namespace SimpleFixture.FakeItEasy
{
    //public class FakeConvention : IConvention
    //{
        //private readonly Dictionary<Type, object> _singletons;
        //private readonly bool _defaultSingletons = true;

        //public FakeConvention(bool fakeSingleton = true)
        //{
        //    _defaultSingletons = fakeSingleton;
        //    _singletons = new Dictionary<Type, object>();
        //}

        //public ConventionPriority Priority
        //{
        //    get { return ConventionPriority.Last; }
        //}

        //public event EventHandler<PriorityChangedEventArgs> PriorityChanged;

        //public object GenerateData(DataRequest request)
        //{
        //    if (!request.RequestedType.IsInterface)
        //    {
        //        return Convention.NoValue;
        //    }

        //    object returnValue = null;
        //    var helper = request.Fixture.Configuration.Locate<IConstraintHelper>();

        //    bool? singleton = helper.GetValue<bool?>(request.Constraints, null, "fakeSingleton");
            
        //    if (!singleton.HasValue)
        //    {
        //        singleton = _defaultSingletons;
        //    }

        //    if (singleton.Value)
        //    {
        //        if (_singletons.TryGetValue(request.RequestedType, out returnValue))
        //        {
        //            return returnValue;
        //        }
        //    }

        //    var method = GetType().GetMethod("GenerateClosedFake",BindingFlags.Instance | BindingFlags.NonPublic).MakeGenericMethod(request.RequestedType);

        //    returnValue = method.Invoke(this, new object[] { request, helper });

        //    if (singleton.Value)
        //    {
        //        _singletons[request.RequestedType] = returnValue;
        //    }

        //    return returnValue;
        //}

        //private T GenerateClosedFake<T>(DataRequest request, IConstraintHelper helper)
        //{
        //    Action<IFakeOptionsBuilder<T>> options = 
        //        helper.GetValue<Action<IFakeOptionsBuilder<T>>>(request.Constraints, null, "builderOptions");

        //    return options != null ? A.Fake(options) : A.Fake<T>();
        //}
    //}
}
