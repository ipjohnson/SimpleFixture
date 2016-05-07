using SimpleFixture.Attributes;
using SimpleFixture.xUnit.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace SimpleFixture.xUnit
{
    public class AutoDataAttribute : DataAttribute
    {
        private static MethodInfo _freezeMethod;

        static AutoDataAttribute()
        {
            _freezeMethod = typeof(AutoDataAttribute).GetRuntimeMethods().First(m => m.Name == "Freeze");
        }

        private object[] _parameters;

        public AutoDataAttribute(params object[] parameters)
        {
            _parameters = parameters;
        }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            Fixture fixture = CreateFixture(testMethod);
            List<object> returnParameters = new List<object>();
            List<object> externalParameters = new List<object>(_parameters);

            foreach(var parameter in testMethod.GetParameters())
            {
                bool found = false;
                object parameterValue = null;

                if(parameter.ParameterType == typeof(Fixture))
                {
                    returnParameters.Add(fixture);
                    continue;
                }

                foreach(var attribute in parameter.GetCustomAttributes())
                {
                    var methodAware = attribute as IMethodInfoAware;

                    if(methodAware != null)
                    {
                        methodAware.Method = testMethod;
                    }

                    var memberAware = attribute as IParameterInfoAware;

                    if(memberAware != null)
                    {
                        memberAware.Parameter = parameter;
                    }

                    if(attribute is FreezeAttribute)
                    {
                        var freezeAttribute = (FreezeAttribute)attribute;

                        parameterValue = FreezeValue(fixture, parameter, freezeAttribute);
                        found = true;
                    }
                    else if(attribute is LocateAttribute)
                    {
                        var locateAttribute = (LocateAttribute)attribute;

                        parameterValue = LocateValue(fixture, parameter, locateAttribute);
                        found = true;
                    }
                    else if(attribute is GenerateAttribute)
                    {
                        var generateAttribute = (GenerateAttribute)attribute;

                        parameterValue = GenerateValue(fixture, parameter, generateAttribute);
                        found = true;
                    }
                }

                if(!found)
                {
                    if(externalParameters.Count > 0)
                    {
                        if(externalParameters[0] == null)
                        {
                            found = true;
                            externalParameters.RemoveAt(0);
                        }
                        else if(parameter.ParameterType.GetTypeInfo().IsAssignableFrom(externalParameters[0].GetType().GetTypeInfo()) ||
                               (parameter.ParameterType.IsByRef || 
                                parameter.ParameterType == typeof(string)))
                        {
                            parameterValue = externalParameters[0];
                            externalParameters.RemoveAt(0);
                            found = true;
                        }
                    }

                    if(!found)
                    {
                        parameterValue = 
                            fixture.Generate(new DataRequest(null, fixture, parameter.ParameterType, parameter.Name, true, null, parameter));
                    }
                }

                returnParameters.Add(parameterValue);
            }

            yield return returnParameters.ToArray();
        }

        private object GenerateValue(Fixture fixture, ParameterInfo parameter, GenerateAttribute generateAttribute)
        {
            object min = null;
            object max = null;
            string constraintName = null;

            if(generateAttribute != null)
            {
                min = generateAttribute.Min;
                max = generateAttribute.Max;
                constraintName = generateAttribute.ConstraintName;
            }

            if(string.IsNullOrEmpty(constraintName))
            {
                constraintName = parameter.Name;
            }

            return fixture.Generate(new DataRequest(null,fixture,parameter.ParameterType,constraintName,true,new { min, max }, parameter));
        }

        private object LocateValue(Fixture fixture, ParameterInfo parameter, LocateAttribute locateAttribute)
        {
            if( locateAttribute.Value != null)
            {
                return locateAttribute.Value;
            }

            return fixture.Locate(parameter.ParameterType, parameter.Name);
        }

        private object FreezeValue(Fixture fixture, ParameterInfo parameter, FreezeAttribute freezeAttribute)
        {
            var closedMethod = _freezeMethod.MakeGenericMethod(parameter.ParameterType);

            return closedMethod.Invoke(this, new object[] { fixture, parameter, freezeAttribute });
        }

        private T Freeze<T>(Fixture fixture, ParameterInfo parameter, FreezeAttribute freezeAttribute)
        {            
            T value;

            if (freezeAttribute.Value is T)
            {
                value = (T)freezeAttribute.Value;
            }
            else
            {
                object min = null;
                object max = null;
                string constraintName = null;

                if (freezeAttribute != null)
                {
                    min = freezeAttribute.Min;
                    max = freezeAttribute.Max;
                    constraintName = freezeAttribute.ConstraintName;
                }

                if (string.IsNullOrEmpty(constraintName))
                {
                    constraintName = parameter.Name;
                }

                value = fixture.Generate<T>(parameter.Name);
            }

            if(freezeAttribute.For != null)
            {
                fixture.Return(value).For(freezeAttribute.For);
            }
            else
            {
                fixture.Return(value);
            }

            return value;
        }

        private Fixture CreateFixture(MethodInfo testMethod)
        {
            Fixture fixture;
            var attribute = ReflectionHelper.GetAttribute<FixtureCreationAttribute>(testMethod);

            if(attribute != null)
            {
                fixture = attribute.CreateFixture();
            }
            else
            {
                fixture = new Fixture();
            }

            var initializeAttribute = ReflectionHelper.GetAttribute<FixtureInitializationAttribute>(testMethod);

            if(initializeAttribute != null)
            {
                initializeAttribute.Initialize(fixture);
            }

            return fixture;
        }
    }
}
