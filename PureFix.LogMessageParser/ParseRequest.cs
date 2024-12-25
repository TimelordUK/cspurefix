using PureFix.Buffer.Ascii;

namespace PureFix.LogMessageParser
{
    public class ParseRequest
    {
        public string? DictName { get; set; }
        public List<string>? Messages { get; set; }
        public byte Delim { get; set; } = AsciiChars.Pipe;
    }
}
