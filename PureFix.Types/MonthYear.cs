using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Types
{
    /// <summary>
    /// Models a FIX Month-Year type/
    /// Valid encodings are:
    /// 
    ///     YYYYMM
    ///     YYYYMMDD
    ///     YYYYMMWW
    ///     
    ///     Where WW is w1, w2, w3, d4 or w5
    /// </summary>
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

        /// <summary>
        /// Initialises the instance from a chunk of memory
        /// </summary>
        /// <param name="data"></param>
        /// <exception cref="ArgumentException"></exception>
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

        /// <summary>
        /// Initializes the instance for a year and month
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        public MonthYear(int year, int month) : this(true, year, month, 6)
        {
        }

        /// <summary>
        /// Initializes the instance for a year, month and day
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="dayOfMonth"></param>
        /// <exception cref="ArgumentException"></exception>
        public MonthYear(int year, int month, int dayOfMonth) : this(true, year, month, 8)
        {
            if(dayOfMonth < 1 || dayOfMonth > 31) throw new ArgumentException("invalid day of month");
            
            var daysInTheMonth = DateTime.DaysInMonth(year, month);
            if(dayOfMonth < 1 || dayOfMonth > daysInTheMonth)
            {
                throw new ArgumentException("invalid day of month");
            }

            m_Data[7] = (byte)((dayOfMonth % 10) + '0');
            dayOfMonth /= 10;
            m_Data[6] = (byte)((dayOfMonth % 10) + '0');
        }

        /// <summary>
        /// Initializes the instance for a year, month and week
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="weekCode"></param>
        /// <exception cref="ArgumentException"></exception>
        public MonthYear(int year, int month, WeekCode weekCode) : this(true, year, month, 8)
        {
            if(weekCode < WeekCode.W1 || weekCode > WeekCode.W5) throw new ArgumentException("invalid week code");

            var asInt = (int)weekCode;
            m_Data[7] = (byte)(asInt + '0');
            m_Data[6] = (byte)('w');
        }

        /// <summary>
        /// Returns the lenth of the month year.
        /// This will be either 6 or 8, or 0 for an invalid month-year
        /// </summary>
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
        /// Returns the year, or zero if not valid
        /// </summary>
        public int Year
        {
            get
            {
                if(!IsValid) return 0;
                return (IntAt(0) * 1000) + (IntAt(1) * 100) + (IntAt(2) * 10) + IntAt(3);
            }
        }

        /// <summary>
        /// Returns the month, or zero if not valid
        /// </summary>
        public int Month
        {
            get
            {
                if(!IsValid) return 0;
                return (IntAt(4) * 10) + IntAt(5);
            }
        }

        /// <summary>
        /// Attempts to get the day of the month if it is part of the value
        /// </summary>
        /// <param name="dayOfMonth"></param>
        /// <returns>true if the instance contains a day of the month, otherwise false</returns>
        public bool TryGetDayOfMonth(out int dayOfMonth)
        {
            if(this.Length != 8)
            {
                dayOfMonth = 0;
                return false;
            }

            if(m_Data[6] == WeekByte)
            {
                dayOfMonth = 0;
                return false;
            }

            dayOfMonth = (IntAt(6) * 10) + IntAt(7);
            return true;
        }

        /// <summary>
        /// Attempts to get the week code if it is part of the value
        /// </summary>
        /// <param name="weekCode"></param>
        /// <returns>true if the instance contains a week code, otherwise false</returns>
        public bool TryGetWeekCode(out WeekCode weekCode)
        {
            if(this.Length != 8)
            {
                weekCode = WeekCode.None;
                return false;
            }

            if(m_Data[6] != WeekByte)
            {
                weekCode = WeekCode.None;
                return false;
            }

            var index = IntAt(7);
            weekCode = (WeekCode)index;
            return true;
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

        /// <summary>
        /// Returns the MonthYear as a FIX encoded string
        /// </summary>
        /// <returns></returns>
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
            return this.IsValid ? AsString() : "";
        }

        private int IntAt(int index)
        {
            return m_Data[index] - AsciiZero;
        }

        /// <summary>
        /// Attempts to parse the MonthYear from a byte buffer
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="monthYear"></param>
        /// <returns>trie if successfully parsed, otherwise false</returns>
        public static bool TryParse(scoped ReadOnlySpan<byte> buffer, out MonthYear monthYear)
        {
            if(IsValidEncoding(buffer))
            {
                monthYear = new(buffer);
                return true;
            }

            monthYear = default;
            return false;
        }

        /// <summary>
        /// Attempts to parse the MonthYear from a string
        /// </summary>
        /// <param name="value"></param>
        /// <param name="monthYear"></param>
        /// <returns>trie if successfully parsed, otherwise false</returns>
        public static bool TryParse(string value, out MonthYear monthYear)
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
