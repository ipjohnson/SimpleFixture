using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFixture.Impl
{
    public class RandomDataGeneratorService : IRandomDataGeneratorService
    {
        private readonly Random _random = new Random();
        private List<char> _allCharacters;
        private List<char> _numericCharacters;
        private List<char> _alphaNumericCharacters;
        private List<char> _alphaCharacters;

        public RandomDataGeneratorService()
        {
            SetupAlphaCharacters();

            SetupNumericCharacters();

            SetupAlphaNumericCharacters();

            SetupAllCharacters();
        }

		public ushort NextUShort(ushort min = ushort.MinValue, ushort max = ushort.MaxValue)
        {
            return Convert.ToUInt16((int)((_random.NextDouble() * (max - min)) + min));
        }

        public int NextInt(int min = int.MinValue, int max = int.MaxValue)
        {
            return _random.Next(min, max);
        }

		public uint NextUInt(uint min = uint.MinValue, uint max = uint.MaxValue)
        {
            return (uint)((_random.NextDouble() * (max - min)) + min);
        }

		public long NextLong(long min = long.MinValue, long max = long.MaxValue)
        {
            return (long)((_random.NextDouble() * (max - min)) + min);
        }

		public ulong NextULong(ulong min = ulong.MinValue, ulong max = ulong.MaxValue)
        {
            return (ulong)((_random.NextDouble() * (max - min)) + min);
        }

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
                decimal range = max.Value - min.Value;

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

	    public object NextEnum(Type enumType)
	    {
		    return NextInSet(Enum.GetValues(enumType).Cast<object>());
	    }

	    public T NextEnum<T>()
        {
            return NextInSet(Enum.GetValues(typeof(T)).Cast<T>());
        }

        public T NextInSet<T>(IEnumerable<T> set)
        {
            List<T> list = new List<T>(set);

            return list.Any() ? list[NextInt(0, list.Count)] : default(T);
        }

        public T NextT<T>(params T[] set)
        {
            return NextInSet(set);
        }

        public IEnumerable<T> Randomize<T>(IEnumerable<T> set)
        {
            List<T> list = new List<T>(set);

            for (int i = 0; i < list.Count; i++)
            {
                int indexSrc = _random.Next(0, list.Count);
                int indexDst = _random.Next(0, list.Count);

                T source = list[indexSrc];
                T dest = list[indexDst];

                list[indexDst] = source;
                list[indexSrc] = dest;
            }

            return list;
        }

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

        

        public double NextDouble(double min = Double.MinValue, double max = Double.MaxValue)
        {
            double range = max - min;

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

        public bool NextBool()
        {
            return _random.Next() % 2 == 0;
        }

        public char NextChar(char min = Char.MinValue, char max = Char.MaxValue)
        {
            return Convert.ToChar((int)((_random.NextDouble() * (max - min)) + min));
        }

        public char NextCharacter()
        {
            return _alphaCharacters[NextInt(0, _alphaCharacters.Count)];
        }

        public byte NextByte(byte min = Byte.MinValue, byte max = Byte.MaxValue)
        {
            return Convert.ToByte((int)((_random.NextDouble() * (max - min)) + min));
        }

	    public sbyte NextSByte(sbyte min = SByte.MinValue, sbyte max = SByte.MaxValue)
		{
			return Convert.ToSByte((int)((_random.NextDouble() * (max - min)) + min));
	    }

	    public short NextShort(short min = short.MinValue, short max = short.MaxValue)
        {
            return Convert.ToInt16((int)((_random.NextDouble() * (max - min)) + min));
        }

        private string BuildString(List<char> characters, int min, int max)
        {
            StringBuilder builder = new StringBuilder();
            int length = _random.Next(min, max);
            int totalCharacters = characters.Count;

            for (int i = 0; i < length; i++)
            {
                builder.Append(characters[_random.Next(0, totalCharacters)]);
            }

            return builder.ToString();
        }

        private void SetupAlphaCharacters()
        {
            List<char> list = new List<char>();

            for (char i = 'a'; i <= 'z'; i++)
            {
                list.Add(i);
            }

            for (char i = 'A'; i <= 'Z'; i++)
            {
                list.Add(i);
            }

            _alphaCharacters = list;
        }

        private void SetupNumericCharacters()
        {
            List<char> list = new List<char>();

            for (char i = '0'; i <= '9'; i++)
            {
                list.Add(i);
            }

            _numericCharacters = list;
        }

        private void SetupAlphaNumericCharacters()
        {
            List<char> list = new List<char>(_alphaCharacters);

            list.AddRange(_numericCharacters);

            _alphaNumericCharacters = list;
        }

        private void SetupAllCharacters()
        {
            List<char> list = new List<char>
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

        private string BuildLoremIpsum(int min, int max)
        {
            int totalLength = NextInt(min, max);

            StringBuilder builder = new StringBuilder(_loremIpsum);
            
            while (builder.Length < totalLength)
            {
                builder.AppendLine();

                builder.Append(_loremIpsum);
            }

            builder.Length = totalLength;

            return builder.ToString();
        }

        private const string _loremIpsum =
@"Lorem ipsum dolor sit amet, mea tincidunt argumentum ea, libris deleniti scripserit est ut. Putent prodesset constituto an has. Mei regione repudiandae dissentiunt an. Sit ut idque utroque pertinacia, ei duo iusto indoctum. Hinc laoreet at sit. Mea dicat veritus in, ius zril suscipiantur ei.

Quo ei amet affert doctus, ei epicurei ullamcorper has. Inermis graecis sententiae vix no. Suas erat error et sed, ad vis esse dolor aperiam. Ex sea melius fabulas appellantur, cum reque maluisset ei.

Ut aliquam persequeris interpretaris qui, solet saepe concludaturque sea cu. Eu option convenire pertinacia has. Ei decore cotidieque vix, ne quis graeco disputando nec. Falli exerci id pro. Ut diam legere posidonium per, at unum dolore commune eam, omnesque intellegam qui et. Pri nullam alienum honestatis et, te mazim instructior sit.

Sit in nibh officiis. Sed ne sapientem constituam, ut eam illum doctus, no pro ceteros ponderum. In mundi appareat gubergren usu. Duo ea doctus adolescens definiebas. Posse erroribus sit et.

Id has nihil libris atomorum, mea et ridens labore. Integre maluisset ea vix, idque legimus eos ex. An mei saepe possit fastidii, no vix veniam posidonium, eum in eros vide. Te sed mundi deseruisse, eum quem inimicus definitiones in. Legimus recteque elaboraret in sed, ei veniam verterem accusamus sit, te admodum tractatos est. Per et alii platonem, ut his platonem praesent mnesarchum.";
    }
}
