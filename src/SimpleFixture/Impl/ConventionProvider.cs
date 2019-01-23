using System.Collections.Generic;
using SimpleFixture.Conventions;
using SimpleFixture.Conventions.Named;

namespace SimpleFixture.Impl
{
    public class ConventionProvider : IConventionProvider
    {
        public IEnumerable<IConvention> ProvideConventions(IFixtureConfiguration configuration)
        {
            var conventions = new List<IConvention>();

            if (configuration.UseDefaultConventions)
            {
                conventions.AddRange(ProvideDefaultConventions(configuration));
            }

            if (configuration.UseNamedConventions)
            {
                conventions.AddRange(ProvideNamedConventions(configuration));
            }

            return conventions;
        }

        private IEnumerable<IConvention> ProvideDefaultConventions(IFixtureConfiguration configuration)
        {
            var dataGenerator = configuration.Locate<IRandomDataGeneratorService>();
            var helper = configuration.Locate<IConstraintHelper>();

            yield return new BoolConvention(dataGenerator);
            yield return new ByteConvention(dataGenerator, helper);
            yield return new CharConvention(dataGenerator, helper);
            yield return new DateTimeConvention(dataGenerator, helper);
            yield return new DateTimeOffsetConvention(dataGenerator, helper);
            yield return new DecimalConvention(dataGenerator, helper);
            yield return new DelegateConvention(helper);
            yield return new DoubleConvention(dataGenerator, helper);
            yield return new EnumConvention(dataGenerator, helper);
            yield return new IntConvention(dataGenerator, helper);
            yield return new LongConvention(dataGenerator, helper);
            yield return new SByteConvention(dataGenerator, helper);
            yield return new ShortConvention(dataGenerator, helper);
            yield return new StringConvention(dataGenerator, helper);
            yield return new TimeSpanConvention(dataGenerator, helper);
            yield return new TypeConvention(dataGenerator);
            yield return new UIntConvention(dataGenerator, helper);
            yield return new ULongConvention(dataGenerator, helper);
            yield return new UriConvention(helper);
            yield return new UShortConvention(dataGenerator, helper);

            yield return new ComplexConvention(configuration);

            yield return new ArrayConvention();
            yield return new DictionaryConvention();
            yield return new ListConvention(configuration);
            yield return new ReadOnlyCollectionConvention();
            yield return new IComparerConvention();
            yield return new IEqualityComparerConvention();

            yield return new NullableConvention();
        }

        private IEnumerable<IConvention> ProvideNamedConventions(IFixtureConfiguration configuration)
        {
            var dataGenerator = configuration.Locate<IRandomDataGeneratorService>();
            var helper = configuration.Locate<IConstraintHelper>();

            yield return new StringNamedConvention(dataGenerator, helper);
        }
    }
}
