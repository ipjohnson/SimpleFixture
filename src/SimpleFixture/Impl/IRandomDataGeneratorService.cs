using System;
using System.Collections.Generic;

namespace SimpleFixture.Impl
{
    /// <summary>
    /// Type of string to return
    /// </summary>
    public enum StringType
    {
        /// <summary>
        /// Mostly characters
        /// </summary>
        MostCharacter,

        /// <summary>
        /// Alpha only
        /// </summary>
        Alpha,

        /// <summary>
        /// Alpha numeric
        /// </summary>
        AlphaNumeric,

        /// <summary>
        /// Numeric only
        /// </summary>
        Numeric,

        /// <summary>
        /// Lorem Ipsum
        /// </summary>
        LoremIpsum
    }

    /// <summary>
    /// Interface for generating random data
    /// </summary>
    public interface IRandomDataGeneratorService
    {
        /// <summary>
        /// Generate the next string
        /// </summary>
        /// <param name="stringType">type of string to return</param>
        /// <param name="min">min string length</param>
        /// <param name="max">max string length</param>
        /// <returns>random string</returns>
        string NextString(StringType stringType = StringType.MostCharacter, int min = 5, int max = 16);

        /// <summary>
        /// Next random bool
        /// </summary>
        /// <returns>random bool</returns>
        bool NextBool();

        /// <summary>
        /// Next random char
        /// </summary>
        /// <param name="min">min character</param>
        /// <param name="max">max character</param>
        /// <returns>random character</returns>
        char NextChar(char min = char.MinValue, char max = char.MaxValue);

        /// <summary>
        /// Next random alpha character
        /// </summary>
        /// <returns>random character</returns>
        char NextCharacter();

        /// <summary>
        /// Next random byte
        /// </summary>
        /// <param name="min">min byte value</param>
        /// <param name="max">max byte value</param>
        /// <returns>random byte</returns>
        byte NextByte(byte min = byte.MinValue, byte max = byte.MaxValue);

        /// <summary>
        /// Next random signed byte
        /// </summary>
        /// <param name="min">min sbyte</param>
        /// <param name="max">max sbyte</param>
        /// <returns>random sbyte</returns>
		sbyte NextSByte(sbyte min = sbyte.MinValue, sbyte max = sbyte.MaxValue);

        /// <summary>
        /// Next random short
        /// </summary>
        /// <param name="min">min short</param>
        /// <param name="max">max short</param>
        /// <returns>random short</returns>
        short NextShort(short min = short.MinValue, short max = short.MaxValue);

        /// <summary>
        /// Next random ushort
        /// </summary>
        /// <param name="min">min ushort</param>
        /// <param name="max">max ushort</param>
        /// <returns>random ushort</returns>
		ushort NextUShort(ushort min = ushort.MinValue, ushort max = ushort.MaxValue);

        /// <summary>
        /// Next random int
        /// </summary>
        /// <param name="min">min int</param>
        /// <param name="max">max int</param>
        /// <returns>random int</returns>
        int NextInt(int min = int.MinValue, int max = int.MaxValue);

        /// <summary>
        /// Next random unit
        /// </summary>
        /// <param name="min">min unit</param>
        /// <param name="max">max uint</param>
        /// <returns>random uint</returns>
		uint NextUInt(uint min = uint.MinValue, uint max = uint.MaxValue);

        /// <summary>
        /// Next random long
        /// </summary>
        /// <param name="min">min long</param>
        /// <param name="max">max long</param>
        /// <returns>random long</returns>
		long NextLong(long min = long.MinValue, long max = long.MaxValue);

        /// <summary>
        /// Next random ulong
        /// </summary>
        /// <param name="min">min ulon</param>
        /// <param name="max">max ulong</param>
        /// <returns>random ulong</returns>
		ulong NextULong(ulong min = ulong.MinValue, ulong max = ulong.MaxValue);

        /// <summary>
        /// Next random double
        /// </summary>
        /// <param name="min">min double</param>
        /// <param name="max">max double</param>
        /// <returns>random double</returns>
        double NextDouble(double min = double.MinValue, double max = double.MaxValue);

        /// <summary>
        /// Next random decimal
        /// </summary>
        /// <param name="min">min decimal</param>
        /// <param name="max">max decimal</param>
        /// <returns>random decimal</returns>
        decimal NextDecimal(decimal? min = null, decimal? max = null);

        /// <summary>
        /// Next random date time
        /// </summary>
        /// <param name="min">min datetime</param>
        /// <param name="max">max datetime</param>
        /// <returns>random datetime</returns>
        DateTime NextDateTime(DateTime? min = null, DateTime? max = null);

        /// <summary>
        /// Next random timespan
        /// </summary>
        /// <param name="min">min timespan</param>
        /// <param name="max">max timespan</param>
        /// <returns>random timespan</returns>
        TimeSpan NextTimeSpan(TimeSpan? min = null, TimeSpan? max = null);

        /// <summary>
        /// Next random enum value
        /// </summary>
        /// <param name="enumType">type of enum</param>
        /// <returns>enum</returns>
	    object NextEnum(Type enumType);

        /// <summary>
        /// Next random enum 
        /// </summary>
        /// <typeparam name="T">type of enum</typeparam>
        /// <returns>random enum</returns>
        T NextEnum<T>();

        /// <summary>
        /// Next random value in set
        /// </summary>
        /// <typeparam name="T">type of set</typeparam>
        /// <param name="set">set</param>
        /// <returns></returns>
        T NextInSet<T>(IEnumerable<T> set);

        /// <summary>
        /// Next random value in set
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="set">set</param>
        /// <returns>random value</returns>
        T NextT<T>(params T[] set);

        /// <summary>
        /// Randomize a set of values
        /// </summary>
        /// <typeparam name="T">value type</typeparam>
        /// <param name="set">set</param>
        /// <returns>randomized set</returns>
        IEnumerable<T> Randomize<T>(IEnumerable<T> set);
    }
}
