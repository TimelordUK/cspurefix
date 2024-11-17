using PureFix.Dictionary.Definition;
using PureFix.LogMessageParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PureFix.Dictionary.Contained
{
    public class DictContainer
    {
        public void Init(string jsonMetaDesFile, Assembly typesAssembly)
        {
            using FileStream openStream = File.OpenRead(jsonMetaDesFile);
            DictMetaSet? set = JsonSerializer.Deserialize<DictMetaSet>(openStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true});
            if (set != null)
            {
                Meta = set;
                foreach (var meta in Meta.Metas) {
                    var parser = new DictMessageParser(meta, typesAssembly);
                    _messageParsers[parser.Meta.Name ?? ""] = parser;
                }
            }
        }

        private readonly Dictionary<string, DictMessageParser> _messageParsers = [];
        private DictMetaSet? Meta { get; set; }

    }
}
