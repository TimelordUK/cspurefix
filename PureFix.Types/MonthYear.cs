using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Types
{
    public readonly struct MonthYear
    {
        private const byte AsciiZero = (byte)'0';
        private const byte AsciiNine = (byte)'9';
        private const byte WeekByte = (byte)'w';

        private readonly EncodedData m_Data;

        private MonthYear(bool _, int year, int month, int length)
        {
            if(year < 0 || year > 9999) throw new ArgumentException("invalid year");
            if(month < 1 || month > 12) throw new ArgumentException("invalid month");

            m_Data[3] = (byte)((year % 10) + '0');
            year /= 10;
            m_Data[2] = (byte)((year % 10) + '0');
            year /= 10;
            m_Data[1] = (byte)((year % 10) + '0');
            year /= 10;
            m_Data[0] = (byte)((year % 10) + '0');

            m_Data[5] = (byte)((month % 10) + '0');
            month /= 10;
            m_Data[4] = (byte)((month % 10) + '0');
        }

        public MonthYear(scoped ReadOnlySpan<byte> data)
        {
            if(IsValidEncoding(data))
            {
                data.CopyTo(m_Data);
            }
            else
            {
                throw new ArgumentException($"invalid encoding for a {nameof(MonthYear)}");
            }
        }

        public MonthYear(int year, int month) : this(true, year, month, 6)
        {
        }

        public MonthYear(int year, int month, int dayOfMonth) : this(true, year, month, 8)
        {
            if(dayOfMonth < 1 || dayOfMonth > 31) throw new ArgumentException("invalid day of month");
            
            if(dayOfMonth == 31 && (month == 4 || month == 6 || month == 9 || month == 11))
            {
                throw new ArgumentException("month can only have 30 days");
            }

            // Let's give February the love it deserves!
            if(month == 2 && dayOfMonth > 29) throw new ArgumentException("invalid day of month");
            if(month == 2 && DateTime.IsLeapYear(year) == false && dayOfMonth == 29) throw new ArgumentException("invalid day of month for non leap year");

            m_Data[7] = (byte)((dayOfMonth % 10) + '0');
            dayOfMonth /= 10;
            m_Data[6] = (byte)((dayOfMonth % 10) + '0');
        }

        public MonthYear(int year, int month, WeekCode weekCode) : this(true, year, month, 8)
        {
            if(weekCode < 0 || weekCode > WeekCode.W5) throw new ArgumentException("invalid week code");

            var asInt = (int)weekCode;
            m_Data[7] = (byte)((asInt + 1) + '0');
            m_Data[6] = (byte)('w');
        }

        public int Length
        {
            get
            {
                if(m_Data[0] == 0) return 0;

                // We're always at least 6 characters long, but 8 if there's a day/week part
                return m_Data[6] == 0 ? 6 : 8;
            }
        }

        public bool IsValid
        {
            get{return m_Data[0] != 0;}
        }

        /// <summary>
        /// Returns the ascii value at the specified index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public byte this[int index]
        {
            get
            {
                if((uint)index >= (uint)this.Length)
                {
                    throw new IndexOutOfRangeException();
                }

                return m_Data[index];
            }
        }

        /// <summary>
        /// Copies the ascii encoded bytes to a destination buffer
        /// </summary>
        /// <param name="destination"></param>
        /// <returns>The number of bytes written</returns>
        public int CopyTo(scoped Span<byte> destination)
        {
            ReadOnlySpan<byte> data = m_Data[0..this.Length];
            data.CopyTo(destination);
            return data.Length;
        }

        public string AsString()
        {
            // We can creates the integer directly into the string buffer
            return string.Create(this.Length, this, static (span, state) =>
            {
                var length = state.Length;

                for(var i = 0; i < length; i++)
                {
                    span[i] = (char)state.m_Data[i];
                }
            });
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.IsValid ? AsString() : "<invalid>";
        }

        public static bool TryParse(scoped ReadOnlySpan<byte> buffer, out MonthYear? monthYear)
        {
            if(IsValidEncoding(buffer))
            {
                monthYear = new(buffer);
                return true;
            }

            monthYear = default;
            return false;
        }

        public static bool TryParse(string value, out MonthYear? monthYear)
        {
            ArgumentNullException.ThrowIfNull(value);

            if(value.Length == 6 || value.Length == 8)
            {
                Span<byte> buffer = stackalloc byte[value.Length];

                for(int i = 0; i < value.Length; i++)
                {
                    buffer[i] = (byte)value[i];
                }

                return TryParse(buffer, out monthYear);
            }

            monthYear = default;
            return false;
        }

        private static bool IsValidEncoding(scoped ReadOnlySpan<byte> buffer)
        {
            if(buffer.Length == 6 || buffer.Length == 8)
            {
                var year = 0;

                // First, the year
                for(int i = 0; i < 4; i++)
                {
                    var b = buffer[i];
                    if(b < AsciiZero && b > AsciiNine) return false;

                    year = (year * 10) + (b - AsciiZero);
                }

                // Grab the month
                var month = ((buffer[4] - AsciiZero) * 10) + (buffer[5] - AsciiZero);
                if(month < 1 || month > 12) return false;

                if(buffer.Length == 6) return true;

                // There a week/date bit
                if(buffer[6] == WeekByte)
                {
                    var weekNumber = buffer[7] - AsciiZero;
                    return weekNumber >= 1 && weekNumber <= 5;
                }

                // It's a day of the month
                var dayOfMonth = ((buffer[6] - AsciiZero) * 10) + (buffer[7] - AsciiZero);
                if(dayOfMonth < 1 || month > 31) return false;

                return dayOfMonth <= DateTime.DaysInMonth(year, month);
                
            }

            return false;
        }

        /// <summary>
        /// Used to hold the tag information
        /// </summary>
        [InlineArray(8)]
        private struct EncodedData
        {
            public byte Data;
        }
    }
}
