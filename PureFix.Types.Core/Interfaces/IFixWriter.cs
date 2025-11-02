using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Types
{
    /// <summary>
    /// Writes data in FIX format
    /// </summary>
    public interface IFixWriter
    {
        /// <summary>
        /// Writes a string to FIX and returns
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public void WriteString(int tag, string value);
        public void WriteWholeNumber(int tag, int value);
        public void WriteNumber(int tag, double value);
        public void WriteBoolean(int tag, bool value);
        public void WriteUtcTimeStamp(int tag, DateTime value);
        public void WriteUtcDateOnly(int tag, DateOnly value);
        public void WriteLocalDateOnly(int tag, DateOnly value);
        public void WriteBuffer(int tag, byte[] value);
        public void WriteMonthYear(int tag, MonthYear value);
        public void WriteTimeOnly(int tag, TimeOnly value);
    }
}
