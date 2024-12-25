namespace PureFix.LogMessageParser
{
    public class ParsedMessage
    {
        public object? Json { get; set; }
        public string? Msg { get; set; }
        public List<MessageTag> Tags { get; set; } = [];
    }
}
