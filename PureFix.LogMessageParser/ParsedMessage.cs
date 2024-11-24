using Newtonsoft.Json.Linq;

namespace SeeFixServer.State
{
    public class ParsedMessage
    {
        public object? Json { get; set; }
        public List<MessageTag> Tags { get; set; } = [];
    }
}
