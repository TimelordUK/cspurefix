using System.Reflection;
using System.Text.Json;

namespace PureFix.LogMessageParser
{
    public class DictContainer : IDictContainer
    {
        public int Count => _messageParsers.Count;
        public IReadOnlyDictionary<string, IDictMessageParser> Parsers => _messageParsers;
        private readonly Dictionary<string, IDictMessageParser> _messageParsers = [];
        private DictMetaSet? Meta { get; set; }

        public IDictMessageParser? this[string name]
        {
            get
            {
                return _messageParsers.GetValueOrDefault(name);
            }
        }
        public void Init(string jsonMetaDesFile, Assembly typesAssembly)
        {
            using FileStream openStream = File.OpenRead(jsonMetaDesFile);
            DictMetaSet? set = JsonSerializer.Deserialize<DictMetaSet>(openStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (set != null)
            {
                Meta = set;
                foreach (var meta in Meta.Metas)
                {
                    var parser = new DictMessageParser(meta, typesAssembly);
                    _messageParsers[parser.Meta.Name ?? ""] = parser;
                }
            }
        }
    }
}
