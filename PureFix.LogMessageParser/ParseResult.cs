namespace PureFix.LogMessageParser
{
    public class ParseResult
    {
        public ParseRequest? Request { get; set; }
        public List<ParsedMessage> Messages { get; set; } = [];
    }
}
