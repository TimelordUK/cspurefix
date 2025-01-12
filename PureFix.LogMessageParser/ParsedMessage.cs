namespace PureFix.LogMessageParser
{
    public class ParsedMessage
    {
        public object? Json { get; set; }
        public string? Msg { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<MessageTag> Tags { get; set; } = [];
    }
}
