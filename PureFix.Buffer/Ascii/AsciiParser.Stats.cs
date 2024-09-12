using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Buffer.Ascii
{
    public partial class AsciiParser
    {
        public readonly record struct Stats(long ReceivedBytes, long ParsedMessages, long Rents, long Returns, double TotalSegmentParseMicro, double TotalElapsedParseMicro);
    }
}
