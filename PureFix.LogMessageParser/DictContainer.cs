using PureFix.Dictionary.Compiler;
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

        private readonly JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

        public void Init(string jsonMetaDesFile, Assembly typesAssembly)
        {
            var baseDir = Path.GetDirectoryName(jsonMetaDesFile) ?? ".";

            using FileStream openStream = File.OpenRead(jsonMetaDesFile);
            DictMetaSet? set = JsonSerializer.Deserialize<DictMetaSet>(openStream, options);
            if (set != null)
            {
                Meta = set;
                foreach (var meta in Meta.Metas)
                {
                    if (!meta.Enabled) continue;
                    try
                    {
                        // Dynamically load the assembly for this entry
                        var asm = LoadAssemblyForMeta(meta, baseDir, typesAssembly);
                        var parser = new DictMessageParser(meta, asm);
                        _messageParsers[parser.Meta.Name ?? ""] = parser;
                    }
                    catch (Exception ex)
                    {
                        // Log but continue - don't fail entire registry for one bad entry
                        Console.WriteLine($"Warning: Failed to load parser for {meta.Name}: {ex.Message}");
                    }
                }
            }
        }

        /// <summary>
        /// Dynamically load the assembly for a registry entry.
        /// Uses Assembly field if specified, otherwise derives from Type field.
        /// </summary>
        private static Assembly LoadAssemblyForMeta(DictMeta meta, string baseDir, Assembly fallbackAssembly)
        {
            // If assembly path is specified, use it
            if (!string.IsNullOrEmpty(meta.Assembly))
            {
                var asmPath = Path.IsPathRooted(meta.Assembly)
                    ? meta.Assembly
                    : Path.Combine(baseDir, meta.Assembly);
                if (File.Exists(asmPath))
                {
                    return Assembly.LoadFrom(asmPath);
                }
            }

            // Derive assembly name from Type namespace (e.g., "PureFix.Types.FIX50SP2" -> "PureFix.Types.FIX50SP2.dll")
            if (!string.IsNullOrEmpty(meta.Type))
            {
                var derivedPath = Path.Combine(baseDir, $"{meta.Type}.dll");
                if (File.Exists(derivedPath))
                {
                    return Assembly.LoadFrom(derivedPath);
                }
            }

            // Fall back to the provided assembly (for backwards compatibility)
            return fallbackAssembly;
        }

        /// <summary>
        /// Find parser by SenderCompID (tag 49) and TargetCompID (tag 56).
        /// Priority: 1) exact match, 2) default, 3) wildcard match.
        /// </summary>
        public IDictMessageParser? FindByCompIds(string? senderCompId, string? targetCompId)
        {
            if (Meta == null) return null;

            // 1. First try to find an exact match (non-wildcard)
            foreach (var meta in Meta.Metas.Where(m => m.Enabled))
            {
                if (meta.IsExactMatch(senderCompId, targetCompId))
                {
                    return this[meta.Name ?? ""];
                }
            }

            // 2. Fall back to default
            var defaultParser = GetDefault();
            if (defaultParser != null) return defaultParser;

            // 3. Last resort: try wildcard matches
            foreach (var meta in Meta.Metas.Where(m => m.Enabled && !m.IsDefault))
            {
                if (meta.Matches(senderCompId, targetCompId))
                {
                    return this[meta.Name ?? ""];
                }
            }

            return null;
        }

        /// <summary>Get the default parser (marked with IsDefault=true)</summary>
        public IDictMessageParser? GetDefault()
        {
            var defaultMeta = Meta?.Metas.FirstOrDefault(m => m.IsDefault && m.Enabled);
            return defaultMeta != null ? this[defaultMeta.Name ?? ""] : null;
        }

        /// <summary>Get all registry metadata entries</summary>
        public IEnumerable<DictMeta> GetAllMetas()
        {
            return Meta?.Metas.Where(m => m.Enabled) ?? Enumerable.Empty<DictMeta>();
        }
    }
}
