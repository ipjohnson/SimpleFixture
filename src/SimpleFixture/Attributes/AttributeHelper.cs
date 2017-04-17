using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SimpleFixture.Attributes
{
    /// <summary>
    /// Reflection helper for fetching attributes
    /// </summary>
    public static class AttributeHelper
    {
        private static readonly MethodInfo FreezeMethod;

        static AttributeHelper()
        {
            FreezeMethod = typeof(AttributeHelper).GetRuntimeMethods().First(m => m.Name == "Freeze" && m.IsGenericMethod);
        }

        /// <summary>
        /// Get attribute on a method, looks on method, then class, then assembly
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        public static T GetAttribute<T>(MethodInfo methodInfo) where T : class
        {
            var returnAttribute = methodInfo.GetCustomAttributes().FirstOrDefault(a => a is T) ??
                                (methodInfo.DeclaringType.GetTypeInfo().GetCustomAttributes().FirstOrDefault(a => a is T) ??
                                 methodInfo.DeclaringType.GetTypeInfo().Assembly.GetCustomAttributes().FirstOrDefault(a => a is T));

            return returnAttribute as T;
        }

        /// <summary>
        /// Gets attributes from method, class, then assembly
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        public static IEnumerable<T> GetAttributes<T>(MethodInfo methodInfo) where T : class
        {
            var returnList = new List<T>();

            returnList.AddRange(methodInfo.GetCustomAttributes().OfType<T>());

            returnList.AddRange(methodInfo.DeclaringType.GetTypeInfo().GetCustomAttributes().OfType<T>());

            returnList.AddRange(methodInfo.DeclaringType.GetTypeInfo().Assembly.GetCustomAttributes().OfType<T>());

            return returnList;
        }


        /// <summary>Returns the data to be used to test the theory.</summary>
        /// <param name="testMethod">The method that is being tested</param>
        /// <param name="parameters"></param>
        /// <returns>One or more sets of theory data. Each invocation of the test method
        /// is represented by a single object array.</returns>
        public static object[] GetData(MethodInfo testMethod, object[] parameters)
        {
            var fixture = CreateFixture(testMethod);
            var returnParameters = new List<object>();
            var externalParameters = new List<object>(parameters);

            foreach (var parameter in testMethod.GetParameters())
            {
                var found = false;
                object parameterValue = null;

                if (parameter.ParameterType == typeof(Fixture))
                {
                    returnParameters.Add(fixture);
                    continue;
                }

                parameterValue = ProvideValueForParameter(fixture, parameter);

                if (parameterValue != null)
                {
                    returnParameters.Add(parameterValue);
                    continue;
                }

                foreach (var attribute in parameter.GetCustomAttributes())
                {
                    var methodAware = attribute as IMethodInfoAware;

                    if (methodAware != null)
                    {
                        methodAware.Method = testMethod;
                    }

                    var memberAware = attribute as IParameterInfoAware;

                    if (memberAware != null)
                    {
                        memberAware.Parameter = parameter;
                    }

                    var freezeAttribute = attribute as FreezeAttribute;

                    if (freezeAttribute != null)
                    {
                        parameterValue = FreezeValue(fixture, parameter, freezeAttribute);
                        found = true;
                    }
                    else if (attribute is LocateAttribute)
                    {
                        var locateAttribute = (LocateAttribute)attribute;

                        parameterValue = LocateValue(fixture, parameter, locateAttribute);
                        found = true;
                    }
                    else if (attribute is GenerateAttribute)
                    {
                        var generateAttribute = (GenerateAttribute)attribute;

                        parameterValue = GenerateValue(fixture, parameter, generateAttribute);
                        found = true;
                    }
                }

                if (!found)
                {
                    if (externalParameters.Count > 0)
                    {
                        if (externalParameters[0] == null)
                        {
                            found = true;
                            externalParameters.RemoveAt(0);
                        }
                        else if (parameter.ParameterType.GetTypeInfo().IsAssignableFrom(externalParameters[0].GetType().GetTypeInfo()) ||
                               (parameter.ParameterType.IsByRef ||
                                parameter.ParameterType == typeof(string)))
                        {
                            parameterValue = externalParameters[0];
                            externalParameters.RemoveAt(0);
                            found = true;
                        }
                    }

                    if (!found)
                    {
                        parameterValue =
                            fixture.Generate(new DataRequest(null, fixture, parameter.ParameterType, DependencyType.Root, parameter.Name, true, null, parameter));
                    }
                }

                returnParameters.Add(parameterValue);
            }

            return returnParameters.ToArray();
        }

        /// <summary>
        /// Override to all inheriting class to provide data for a parameter
        /// </summary>
        /// <param name="fixture"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private static object ProvideValueForParameter(Fixture fixture, ParameterInfo parameter)
        {
            return null;
        }

        /// <summary>
        /// Generate value for parameter
        /// </summary>
        /// <param name="fixture"></param>
        /// <param name="parameter"></param>
        /// <param name="generateAttribute"></param>
        /// <returns></returns>
        private static object GenerateValue(Fixture fixture, ParameterInfo parameter, GenerateAttribute generateAttribute)
        {
            object min = null;
            object max = null;
            string constraintName = null;

            if (generateAttribute != null)
            {
                min = generateAttribute.Min;
                max = generateAttribute.Max;
                constraintName = generateAttribute.ConstraintName;
            }

            if (string.IsNullOrEmpty(constraintName))
            {
                constraintName = parameter.Name;
            }

            return fixture.Generate(new DataRequest(null, fixture, parameter.ParameterType, DependencyType.Root, constraintName, true, new { min, max }, parameter));
        }

        /// <summary>
        /// Locate value from fixture
        /// </summary>
        /// <param name="fixture"></param>
        /// <param name="parameter"></param>
        /// <param name="locateAttribute"></param>
        /// <returns></returns>
        private static object LocateValue(Fixture fixture, ParameterInfo parameter, LocateAttribute locateAttribute)
        {
            if (locateAttribute.Value != null)
            {
                return locateAttribute.Value;
            }

            return fixture.Locate(parameter.ParameterType, parameter.Name);
        }

        /// <summary>
        /// Freeze value in fixture
        /// </summary>
        /// <param name="fixture"></param>
        /// <param name="parameter"></param>
        /// <param name="freezeAttribute"></param>
        /// <returns></returns>
        private static object FreezeValue(Fixture fixture, ParameterInfo parameter, FreezeAttribute freezeAttribute)
        {
            var closedMethod = FreezeMethod.MakeGenericMethod(parameter.ParameterType);

            return closedMethod.Invoke(null, new object[] { fixture, parameter, freezeAttribute });
        }

        /// <summary>
        /// Freeze a specific type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fixture"></param>
        /// <param name="parameter"></param>
        /// <param name="freezeAttribute"></param>
        /// <returns></returns>
        private static T Freeze<T>(Fixture fixture, ParameterInfo parameter, FreezeAttribute freezeAttribute)
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

            if (freezeAttribute.For != null)
            {
                fixture.Return(value).For(freezeAttribute.For);
            }
            else
            {
                fixture.Return(value);
            }

            return value;
        }

        /// <summary>
        /// Create fixture for method
        /// </summary>
        /// <param name="testMethod"></param>
        /// <returns></returns>
        private static Fixture CreateFixture(MethodInfo testMethod)
        {
            var attribute = AttributeHelper.GetAttribute<FixtureCreationAttribute>(testMethod);

            var fixture = attribute != null ? attribute.CreateFixture() : new Fixture();

            var initializeAttributes = AttributeHelper.GetAttributes<FixtureInitializationAttribute>(testMethod);

            foreach (var initializeAttribute in initializeAttributes)
            {
                initializeAttribute.Initialize(fixture);
            }

            return fixture;
        }
    }
}
