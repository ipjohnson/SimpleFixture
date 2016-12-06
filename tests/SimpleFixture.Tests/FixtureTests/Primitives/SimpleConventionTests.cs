using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using SimpleFixture.Conventions;
using SimpleFixture.Impl;
using Xunit;

namespace SimpleFixture.Tests.FixtureTests.Primitives
{
    public class SimpleConventionTests
    {
        [Theory]
        [InlineData(typeof(ByteConvention))]
        [InlineData(typeof(SByteConvention))]
        [InlineData(typeof(ShortConvention))]
        [InlineData(typeof(UShortConvention))]
        [InlineData(typeof(IntConvention))]
        [InlineData(typeof(UIntConvention))]
        [InlineData(typeof(LongConvention))]
        [InlineData(typeof(ULongConvention))]
        [InlineData(typeof(DoubleConvention))]
        [InlineData(typeof(DecimalConvention))]
        public void SimpleConvention_Min_Max_Test(Type conventionType)
        {
            var fixture = new Fixture();

            var convention = CreateTypedConvention(conventionType);

            var value = convention.GenerateData(new DataRequest(null, fixture, GetPrimitiveType(conventionType), DependencyType.Unknown, null, true, new {min = 3, max = 2}, null));

            Assert.Equal(2, Convert.ToInt32(value));
        }

        private Type GetPrimitiveType(Type type)
        {
            var baseType = type.GetTypeInfo().BaseType;

            if (!baseType.IsConstructedGenericType ||
                baseType.GetGenericTypeDefinition() != typeof(SimpleTypeConvention<>))
            {
                throw new Exception("Must be SimpleTypeConvention");
            }

            return baseType.GenericTypeArguments[0];
        }

        private ITypedConvention CreateTypedConvention(Type conventionType)
        {
            return (ITypedConvention)Activator.CreateInstance(conventionType, new RandomDataGeneratorService(), new ConstraintHelper());
        }
    }
}
