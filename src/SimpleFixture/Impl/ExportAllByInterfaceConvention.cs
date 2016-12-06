using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SimpleFixture.Impl
{
    public class ExportAllByInterfaceConvention : IConvention
    {
        private Dictionary<Type, Type> _interfaceMap;
        private Dictionary<Type, List<Type>> _openInterfaceMap;
        private readonly Type[] _types;
        private Dictionary<Type, object> _singletons;
        private Dictionary<Type, bool> _isSingleton;

        public ExportAllByInterfaceConvention(IEnumerable<Type> types)
        {
            _types = types.ToArray();
        }

        public ConventionPriority Priority => ConventionPriority.Low;

        public Func<Type, Type, bool> AsSingleton { get; set; }

        public Func<Type, Type, bool> TypeFilter { get; set; }

        public event EventHandler<PriorityChangedEventArgs> PriorityChanged;

        public object GenerateData(DataRequest request)
        {
            if (!request.RequestedType.GetTypeInfo().IsInterface)
            {
                return Convention.NoValue;
            }

            Initialize();

            Type concreteType = FindConcreteType(request);

            if (concreteType != null)
            {
                object value;

                if (_singletons.TryGetValue(concreteType, out value))
                {
                    return value;
                }

                bool isSingleton;

                if (!_isSingleton.TryGetValue(concreteType, out isSingleton))
                {
                    if (AsSingleton != null)
                    {
                        isSingleton = AsSingleton(concreteType, request.RequestedType);
                    }
                    else
                    {
                        isSingleton = false;
                    }

                    _isSingleton[concreteType] = isSingleton;
                }

                var newRequest = new DataRequest(request.ParentRequest, request.Fixture, concreteType,request.DependencyType, request.RequestName, request.Populate, request.Constraints, request.ExtraInfo);

                value = request.Fixture.Generate(newRequest);

                if (isSingleton)
                {
                    _singletons[concreteType] = value;
                }

                return value;
            }

            return Convention.NoValue;
        }

        private Type FindConcreteType(DataRequest request)
        {
            Type concreteType;

            if (!_interfaceMap.TryGetValue(request.RequestedType, out concreteType))
            {
                if (request.RequestedType.IsConstructedGenericType)
                {
                    var genericType = request.RequestedType.GetGenericTypeDefinition();
                    var genericArgs = request.RequestedType.GetTypeInfo().GenericTypeArguments;
                    List<Type> typeList;

                    if (_openInterfaceMap.TryGetValue(genericType, out typeList))
                    {
                        foreach (var openType in typeList)
                        {
                            var openParameters = openType.GetTypeInfo().GenericTypeParameters;

                            foreach (var implementedInterface in openType.GetTypeInfo().ImplementedInterfaces)
                            {
                                if (implementedInterface.GetTypeInfo().IsGenericType)
                                {
                                    var openTestType = implementedInterface.GetTypeInfo().GetGenericTypeDefinition();

                                    if (openTestType == genericType)
                                    {
                                        bool matched = true;
                                        List<Tuple<Type, int>> genericParameters = new List<Tuple<Type, int>>();
                                        var implementedInterfaceParameters = implementedInterface.GetTypeInfo().GenericTypeArguments;

                                        for (int i = 0; i < genericArgs.Length; i++)
                                        {
                                            var matchedParameter = openParameters.First(t => t == implementedInterfaceParameters[i]);

                                            var constraints = matchedParameter.GetTypeInfo().GetGenericParameterConstraints();

                                            foreach (var constraintType in constraints)
                                            {
                                                if (!constraintType.GetTypeInfo().IsAssignableFrom(genericArgs[i].GetTypeInfo()))
                                                {
                                                    matched = false;
                                                    break;
                                                }
                                            }

                                            if (matched)
                                            {
                                                genericParameters.Add(new Tuple<Type, int>(genericArgs[i], matchedParameter.GenericParameterPosition));
                                            }
                                        }

                                        if (matched)
                                        {
                                            genericParameters.Sort((x, y) => Comparer<int>.Default.Compare(x.Item2, y.Item2));
                                            concreteType = openType.MakeGenericType(genericParameters.Select(p => p.Item1).ToArray());
                                            break;
                                        }
                                    }
                                }
                            }

                            if (concreteType != null)
                            {
                                break;
                            }
                        }
                    }
                }
            }

            return concreteType;
        }

        protected virtual void Initialize()
        {
            if (_interfaceMap != null)
            {
                return;
            }

            _interfaceMap = new Dictionary<Type, Type>();
            _openInterfaceMap = new Dictionary<Type, List<Type>>();
            _singletons = new Dictionary<Type, object>();
            _isSingleton = new Dictionary<Type, bool>();

            foreach (var type in _types)
            {
                if (type.GetTypeInfo().IsGenericTypeDefinition)
                {
                    var typeArguements = type.GetTypeInfo().GenericTypeParameters;

                    foreach (var interfaceType in type.GetTypeInfo().ImplementedInterfaces)
                    {
                        if (!interfaceType.GetTypeInfo().IsGenericType)
                        {
                            continue;
                        }

                        var arguements = interfaceType.GenericTypeArguments.ToArray();

                        if (arguements.Length != typeArguements.Length)
                        {
                            continue;
                        }

                        var openInterface = interfaceType.GetGenericTypeDefinition();

                        if (TypeFilter == null || TypeFilter(type, openInterface))
                        {
                            List<Type> typeList;

                            if(!_openInterfaceMap.TryGetValue(openInterface,out typeList))
                            {
                                typeList = new List<Type>();

                                _openInterfaceMap[openInterface] = typeList;
                            }

                            typeList.Add(type);
                        }
                    }
                }
                else
                {
                    foreach (var interfaceType in type.GetTypeInfo().ImplementedInterfaces)
                    {
                        if (interfaceType == typeof(IDisposable))
                        {
                            continue;
                        }

                        if (TypeFilter == null || TypeFilter(type, interfaceType))
                        {
                            _interfaceMap[interfaceType] = type;
                        }
                    }
                }
            }
        }
    }

}
