using PureFix.Buffer.Ascii;

namespace PureFix.LogMessageParser
{
    public class ParseRequest
    {
        public string? DictName { get; set; }
        public string? Message { get; set; }
        public byte Delim { get; set; } = AsciiChars.Pipe;
    }
}
