using System;
using System.Reflection;

namespace SimpleFixture.Impl
{
    public interface ITypePopulator
    {
        void Populate(object instance, DataRequest request, ComplexModel model);
    }

    public class TypePopulator : ITypePopulator
    {
        private readonly IPropertySetter _setter;
        private readonly ITypePropertySelector _propertySelector;
        private readonly IConstraintHelper _helper;
        private readonly IFixtureConfiguration _configuration;
        private readonly ITypeFieldSelector _fieldSelector;
        private readonly IFieldSetter _fieldSetter;

        public TypePopulator(IFixtureConfiguration configuration,
                             IConstraintHelper helper,
                             ITypePropertySelector propertySelector,
                             IPropertySetter setter,
                             ITypeFieldSelector fieldSelector,
                             IFieldSetter fieldSetter)
        {
            _configuration = configuration;
            _helper = helper;
            _propertySelector = propertySelector;
            _setter = setter;
            _fieldSelector = fieldSelector;
            _fieldSetter = fieldSetter;
        }

        public virtual void Populate(object instance, DataRequest request, ComplexModel model)
        {
            if (instance == null)
            {
                return;
            }

            if (_configuration.PopulateProperties)
            {
                foreach (var propertyInfo in _propertySelector.SelectProperties(instance, request, model))
                {
                    object propertyValue = null;

                    if (_configuration.CircularReferenceHandling == CircularReferenceHandlingAlgorithm.AutoWire)
                    {
                        var currentRequest = request;
                        var requestTypeInfo = propertyInfo.PropertyType.GetTypeInfo();

                        while (currentRequest != null)
                        {
                            if (requestTypeInfo.IsAssignableFrom(currentRequest.RequestedType.GetTypeInfo()))
                            {
                                propertyValue = currentRequest.Instance;
                                break;
                            }

                            currentRequest = currentRequest.ParentRequest;
                        }                        
                    }

                    var newRequest = CreateDataRequestForProperty(propertyInfo, request);
                    
                    var foundProperty = false;

                    if (propertyValue == null)
                    {
                        foundProperty = _helper.GetUnTypedValue(out propertyValue, propertyInfo.PropertyType, request.Constraints, null, propertyInfo.Name);

                        if (propertyValue != null)
                        {
                            propertyValue = newRequest.Fixture.Behavior.Apply(newRequest, propertyValue);
                        }
                    }

                    if(!foundProperty && propertyValue == null)
                    {
                        if (!model.GetPropertyValue(newRequest, propertyInfo, out propertyValue))
                        {
                            if (model.SkipProperty(newRequest, propertyInfo))
                            {
                                continue;
                            }

                            propertyValue = newRequest.Fixture.Generate(newRequest);
                        }
                        else
                        {
                            propertyValue = newRequest.Fixture.Behavior.Apply(newRequest, propertyValue);
                        }
                    }

                    if (!foundProperty && propertyValue == null)
                    {
                        throw new Exception("Could not create type " + propertyInfo.PropertyType.FullName);
                    }

                    _setter.SetProperty(propertyInfo, instance, propertyValue);
                }
            }

            if (_configuration.PopulateFields)
            {
                foreach (var fieldInfo in _fieldSelector.SelectFields(instance, request, model))
                {
                    object propertyValue;
                    var foundValue = _helper.GetUnTypedValue(out propertyValue, fieldInfo.FieldType, request.Constraints, null, fieldInfo.Name);

                    var newRequest = CreateDataRequestForField(fieldInfo, request);

                    if (propertyValue != null)
                    {
                        propertyValue = newRequest.Fixture.Behavior.Apply(newRequest, propertyValue);
                    }
                    else if(!foundValue)
                    {
                        propertyValue = newRequest.Fixture.Generate(newRequest);
                    }

                    if (!foundValue && propertyValue == null)
                    {
                        throw new Exception("Could not create type " + fieldInfo.FieldType.FullName);
                    }
                    
                    _fieldSetter.SetField(fieldInfo, instance, propertyValue);
                }
            }
        }

        protected virtual DataRequest CreateDataRequestForProperty(PropertyInfo propertyInfo, DataRequest request)
        {
            return new DataRequest(request,
                                    request.Fixture,
                                    propertyInfo.PropertyType,
                                    DependencyType.PropertyDependency,
                                    propertyInfo.Name,
                                    true,
                                    request.Constraints,
                                    propertyInfo);
        }

        protected virtual DataRequest CreateDataRequestForField(FieldInfo fieldInfo, DataRequest request)
        {
            return new DataRequest(request,
                                    request.Fixture,
                                    fieldInfo.FieldType,
                                    DependencyType.PropertyDependency,
                                    fieldInfo.Name,
                                    true,
                                    request.Constraints,
                                    fieldInfo);
        }
    }
}
