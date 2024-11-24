using Newtonsoft.Json.Linq;

namespace SeeFixServer.State
{
    public class ParsedMessage
    {
        public string? Json { get; set; }
        public List<MessageTag> Tags { get; set; } = [];
    }
}
