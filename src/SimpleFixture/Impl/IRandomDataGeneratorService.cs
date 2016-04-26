using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFixture.Impl
{
    public enum StringType
    {
        MostCharacter,
        Alpha,
        AlphaNumeric,
        Numeric,
        LoremIpsum
    }

    public interface IRandomDataGeneratorService
    {
        string NextString(StringType stringType = StringType.MostCharacter, int min = 5, int max = 16);

        bool NextBool();

        char NextChar(char min = char.MinValue, char max = char.MaxValue);

        char NextCharacter();

        byte NextByte(byte min = byte.MinValue, byte max = byte.MaxValue);

		sbyte NextSByte(sbyte min = sbyte.MinValue, sbyte max = sbyte.MaxValue);

        short NextShort(short min = short.MinValue, short max = short.MaxValue);

		ushort NextUShort(ushort min = ushort.MinValue, ushort max = ushort.MaxValue);

        int NextInt(int min = int.MinValue, int max = int.MaxValue);

		uint NextUInt(uint min = uint.MinValue, uint max = uint.MaxValue);

		long NextLong(long min = long.MinValue, long max = long.MaxValue);

		ulong NextULong(ulong min = ulong.MinValue, ulong max = ulong.MaxValue);

        double NextDouble(double min = double.MinValue, double max = double.MaxValue);

        decimal NextDecimal(decimal? min = null, decimal? max = null);

        DateTime NextDateTime(DateTime? min = null, DateTime? max = null);

        TimeSpan NextTimeSpan(TimeSpan? min = null, TimeSpan? max = null);

	    object NextEnum(Type enumType);

        T NextEnum<T>();

        T NextInSet<T>(IEnumerable<T> set);

        T NextT<T>(params T[] set);

        IEnumerable<T> Randomize<T>(IEnumerable<T> set);
    }
}
