using PureFix.Buffer.Ascii;

namespace SeeFixServer.State
{
    public class ParseRequest
    {
        public string? DictName { get; set; }
        public string? Message { get; set; }
        public byte Delim { get; set; } = AsciiChars.Pipe;
    }
}
