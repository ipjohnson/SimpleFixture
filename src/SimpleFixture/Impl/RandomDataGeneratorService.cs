using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleFixture.Impl
{
    /// <summary>
    /// Class for generating random data
    /// </summary>
    public class RandomDataGeneratorService : IRandomDataGeneratorService
    {
        private readonly Random _random = new Random();
        private List<char> _allCharacters;
        private List<char> _numericCharacters;
        private List<char> _alphaNumericCharacters;
        private List<char> _alphaCharacters;

        /// <summary>
        /// Default constructor
        /// </summary>
        public RandomDataGeneratorService()
        {
            SetupAlphaCharacters();

            SetupNumericCharacters();

            SetupAlphaNumericCharacters();

            SetupAllCharacters();
        }

        /// <summary>
        /// Next random ushort
        /// </summary>
        /// <param name="min">min ushort</param>
        /// <param name="max">max ushort</param>
        /// <returns>random ushort</returns>
        public ushort NextUShort(ushort min = ushort.MinValue, ushort max = ushort.MaxValue)
        {
            return Convert.ToUInt16((int)((_random.NextDouble() * (max - min)) + min));
        }

        /// <summary>
        /// Next random int
        /// </summary>
        /// <param name="min">min int</param>
        /// <param name="max">max int</param>
        /// <returns>random int</returns>
        public int NextInt(int min = int.MinValue, int max = int.MaxValue)
        {
            return _random.Next(min, max);
        }

        /// <summary>
        /// Next random unit
        /// </summary>
        /// <param name="min">min unit</param>
        /// <param name="max">max uint</param>
        /// <returns>random uint</returns>
        public uint NextUInt(uint min = uint.MinValue, uint max = uint.MaxValue)
        {
            return (uint)((_random.NextDouble() * (max - min)) + min);
        }

        /// <summary>
        /// Next random long
        /// </summary>
        /// <param name="min">min long</param>
        /// <param name="max">max long</param>
        /// <returns>random long</returns>
        public long NextLong(long min = long.MinValue, long max = long.MaxValue)
        {
            if(min == long.MinValue && max == long.MaxValue)
            {
                if(NextBool())
                {
                    return (long)(_random.NextDouble() * max);
                }

                return (long)(_random.NextDouble() * min);
            }

            return (long)((_random.NextDouble() * (max - min)) + min);
        }

        /// <summary>
        /// Next random ulong
        /// </summary>
        /// <param name="min">min ulon</param>
        /// <param name="max">max ulong</param>
        /// <returns>random ulong</returns>
        public ulong NextULong(ulong min = ulong.MinValue, ulong max = ulong.MaxValue)
        {
            if (min == ulong.MinValue && max == ulong.MaxValue)
            {
                return (ulong)(_random.NextDouble() * max);                
            }

            return (ulong)((_random.NextDouble() * (max - min)) + min);
        }

        /// <summary>
        /// Next random decimal
        /// </summary>
        /// <param name="min">min decimal</param>
        /// <param name="max">max decimal</param>
        /// <returns>random decimal</returns>
        public decimal NextDecimal(decimal? min = null, decimal? max = null)
        {
            if (!min.HasValue)
            {
                min = decimal.MinValue;
            }

            if (!max.HasValue)
            {
                max = decimal.MaxValue;
            }

            try
            {
                var range = max.Value - min.Value;

                return range * (decimal)_random.NextDouble() + min.Value;
            }
            catch (Exception)
            {
                if (NextBool())
                {
                    return (((decimal)_random.NextDouble()) * decimal.MaxValue) + min.Value;
                }

                return max.Value - ((decimal)_random.NextDouble() * decimal.MaxValue);
            }
        }

        /// <summary>
        /// Next random enum value
        /// </summary>
        /// <param name="enumType">type of enum</param>
        /// <returns>enum</returns>
        public object NextEnum(Type enumType)
	    {
		    return NextInSet(Enum.GetValues(enumType).Cast<object>());
	    }

        /// <summary>
        /// Next random enum 
        /// </summary>
        /// <typeparam name="T">type of enum</typeparam>
        /// <returns>random enum</returns>
        public T NextEnum<T>()
        {
            return NextInSet(Enum.GetValues(typeof(T)).Cast<T>());
        }

        /// <summary>
        /// Next random value in set
        /// </summary>
        /// <typeparam name="T">type of set</typeparam>
        /// <param name="set">set</param>
        /// <returns></returns>
        public T NextInSet<T>(IEnumerable<T> set)
        {
            var list = new List<T>(set);

            return list.Any() ? list[NextInt(0, list.Count)] : default(T);
        }

        /// <summary>
        /// Next random value in set
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="set">set</param>
        /// <returns>random value</returns>
        public T NextT<T>(params T[] set)
        {
            return NextInSet(set);
        }

        /// <summary>
        /// Randomize a set of values
        /// </summary>
        /// <typeparam name="T">value type</typeparam>
        /// <param name="set">set</param>
        /// <returns>randomized set</returns>
        public IEnumerable<T> Randomize<T>(IEnumerable<T> set)
        {
            var list = new List<T>(set);

            for (var i = 0; i < list.Count; i++)
            {
                var indexSrc = _random.Next(0, list.Count);
                var indexDst = _random.Next(0, list.Count);

                var source = list[indexSrc];
                var dest = list[indexDst];

                list[indexDst] = source;
                list[indexSrc] = dest;
            }

            return list;
        }

        /// <summary>
        /// Generate the next string
        /// </summary>
        /// <param name="stringType">type of string to return</param>
        /// <param name="min">min string length</param>
        /// <param name="max">max string length</param>
        /// <returns>random string</returns>
        public string NextString(StringType stringType = StringType.MostCharacter, int min = 5, int max = 16)
        {
            switch (stringType)
            {
                case StringType.Alpha:
                    return BuildString(_alphaCharacters, min, max);

                case StringType.AlphaNumeric:
                    return BuildString(_alphaNumericCharacters, min, max);

                case StringType.Numeric:
                    return BuildString(_numericCharacters, min, max);

                case StringType.LoremIpsum:
                    return BuildLoremIpsum(min, max);

                default:
                    return BuildString(_allCharacters, min, max);
            }
        }

        /// <summary>
        /// Next random double
        /// </summary>
        /// <param name="min">min double</param>
        /// <param name="max">max double</param>
        /// <returns>random double</returns>
        public double NextDouble(double min = double.MinValue, double max = double.MaxValue)
        {
            var range = max - min;

            if (double.IsInfinity(range))
            {
                if (NextBool())
                {
                    return _random.NextDouble() * double.MaxValue + min;
                }

                return max - _random.NextDouble() * double.MaxValue;
            }

            return _random.NextDouble() * (range) + min;
        }

        /// <summary>
        /// Next random bool
        /// </summary>
        /// <returns>random bool</returns>
        public bool NextBool()
        {
            return _random.Next() % 2 == 0;
        }

        /// <summary>
        /// Next random char
        /// </summary>
        /// <param name="min">min character</param>
        /// <param name="max">max character</param>
        /// <returns>random character</returns>
        public char NextChar(char min = char.MinValue, char max = char.MaxValue)
        {
            return Convert.ToChar((int)((_random.NextDouble() * (max - min)) + min));
        }

        /// <summary>
        /// Next random alpha character
        /// </summary>
        /// <returns>random character</returns>
        public char NextCharacter()
        {
            return _alphaCharacters[NextInt(0, _alphaCharacters.Count)];
        }

        /// <summary>
        /// Next random byte
        /// </summary>
        /// <param name="min">min byte value</param>
        /// <param name="max">max byte value</param>
        /// <returns>random byte</returns>
        public byte NextByte(byte min = byte.MinValue, byte max = byte.MaxValue)
        {
            return Convert.ToByte((int)((_random.NextDouble() * (max - min)) + min));
        }

        /// <summary>
        /// Next random signed byte
        /// </summary>
        /// <param name="min">min sbyte</param>
        /// <param name="max">max sbyte</param>
        /// <returns>random sbyte</returns>
        public sbyte NextSByte(sbyte min = sbyte.MinValue, sbyte max = sbyte.MaxValue)
		{
			return Convert.ToSByte((int)((_random.NextDouble() * (max - min)) + min));
	    }

        /// <summary>
        /// Next random short
        /// </summary>
        /// <param name="min">min short</param>
        /// <param name="max">max short</param>
        /// <returns>random short</returns>
        public short NextShort(short min = short.MinValue, short max = short.MaxValue)
        {
            return Convert.ToInt16((int)((_random.NextDouble() * (max - min)) + min));
        }

        /// <summary>
        /// Next random date time
        /// </summary>
        /// <param name="min">min datetime</param>
        /// <param name="max">max datetime</param>
        /// <returns>random datetime</returns>
        public DateTime NextDateTime(DateTime? min = default(DateTime?), DateTime? max = default(DateTime?))
        {
            if(!min.HasValue)
            {
                min = DateTime.MinValue;
            }

            if(!max.HasValue)
            {
                max = DateTime.MaxValue;
            }

            var ticks = NextLong(min.Value.Ticks, max.Value.Ticks);

            return new DateTime(ticks);
        }

        /// <summary>
        /// Next random timespan
        /// </summary>
        /// <param name="min">min timespan</param>
        /// <param name="max">max timespan</param>
        /// <returns>random timespan</returns>
        public TimeSpan NextTimeSpan(TimeSpan? min = default(TimeSpan?), TimeSpan? max = default(TimeSpan?))
        {
            if (!min.HasValue)
            {
                min = TimeSpan.MinValue;
            }

            if (!max.HasValue)
            {
                max = TimeSpan.MaxValue;
            }

            var ticks = NextLong(min.Value.Ticks, max.Value.Ticks);

            return new TimeSpan(ticks);
        }

        /// <summary>
        /// Build random string
        /// </summary>
        /// <param name="characters">character list</param>
        /// <param name="min">min string length</param>
        /// <param name="max">max string length</param>
        /// <returns></returns>
        protected virtual string BuildString(List<char> characters, int min, int max)
        {
            var builder = new StringBuilder();
            var length = _random.Next(min, max);
            var totalCharacters = characters.Count;

            for (var i = 0; i < length; i++)
            {
                builder.Append(characters[_random.Next(0, totalCharacters)]);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Setup alpha character set
        /// </summary>
        protected virtual void SetupAlphaCharacters()
        {
            var list = new List<char>();

            for (var i = 'a'; i <= 'z'; i++)
            {
                list.Add(i);
            }

            for (var i = 'A'; i <= 'Z'; i++)
            {
                list.Add(i);
            }

            _alphaCharacters = list;
        }

        /// <summary>
        /// Setup numeric character set
        /// </summary>
        protected virtual void SetupNumericCharacters()
        {
            var list = new List<char>();

            for (var i = '0'; i <= '9'; i++)
            {
                list.Add(i);
            }

            _numericCharacters = list;
        }


        /// <summary>
        /// Setup alpha numeric characters
        /// </summary>
        protected virtual void SetupAlphaNumericCharacters()
        {
            var list = new List<char>(_alphaCharacters);

            list.AddRange(_numericCharacters);

            _alphaNumericCharacters = list;
        }

        /// <summary>
        /// Setup a list of call charactes
        /// </summary>
        protected virtual void SetupAllCharacters()
        {
            var list = new List<char>
                              {
                                  '!',
                                  '@',
                                  '#',
                                  '$',
                                  '%',
                                  '^',
                                  '&',
                                  '*',
                                  '?',
                                  ';',
                                  '(',
                                  ')',
                                  '\\',
                                  '[',
                                  ']',
                                  ',',
                                  '.',
                                  '/',
                                  '_',
                                  '-',
                                  '+',
                                  '=',
                                  '~',
                                  '`',
                                  ':',
                                  '<',
                                  '>'
                              };

            list.AddRange(_alphaNumericCharacters);

            _allCharacters = list;
        }

        /// <summary>
        /// Build a lorem ipsum string
        /// </summary>
        /// <param name="min">min string size</param>
        /// <param name="max">max string size</param>
        /// <returns>random string</returns>
        protected virtual string BuildLoremIpsum(int min, int max)
        {
            var totalLength = NextInt(min, max);

            var builder = new StringBuilder(LoremIpsum);
            
            while (builder.Length < totalLength)
            {
                builder.AppendLine();

                builder.Append(LoremIpsum);
            }

            builder.Length = totalLength;

            return builder.ToString();
        }

        public const string LoremIpsum =
@"Lorem ipsum dolor sit amet, mea tincidunt argumentum ea, libris deleniti scripserit est ut. Putent prodesset constituto an has. Mei regione repudiandae dissentiunt an. Sit ut idque utroque pertinacia, ei duo iusto indoctum. Hinc laoreet at sit. Mea dicat veritus in, ius zril suscipiantur ei.

Quo ei amet affert doctus, ei epicurei ullamcorper has. Inermis graecis sententiae vix no. Suas erat error et sed, ad vis esse dolor aperiam. Ex sea melius fabulas appellantur, cum reque maluisset ei.

Ut aliquam persequeris interpretaris qui, solet saepe concludaturque sea cu. Eu option convenire pertinacia has. Ei decore cotidieque vix, ne quis graeco disputando nec. Falli exerci id pro. Ut diam legere posidonium per, at unum dolore commune eam, omnesque intellegam qui et. Pri nullam alienum honestatis et, te mazim instructior sit.

Sit in nibh officiis. Sed ne sapientem constituam, ut eam illum doctus, no pro ceteros ponderum. In mundi appareat gubergren usu. Duo ea doctus adolescens definiebas. Posse erroribus sit et.

Id has nihil libris atomorum, mea et ridens labore. Integre maluisset ea vix, idque legimus eos ex. An mei saepe possit fastidii, no vix veniam posidonium, eum in eros vide. Te sed mundi deseruisse, eum quem inimicus definitiones in. Legimus recteque elaboraret in sed, ei veniam verterem accusamus sit, te admodum tractatos est. Per et alii platonem, ut his platonem praesent mnesarchum.";
    }
}
