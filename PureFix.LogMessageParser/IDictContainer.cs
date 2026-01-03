using System.Reflection;

namespace PureFix.LogMessageParser
{
    public interface IDictContainer
    {
        /// <summary>Get parser by registry name</summary>
        IDictMessageParser? this[string name] { get; }

        /// <summary>Number of registered parsers</summary>
        int Count { get; }

        /// <summary>All registered parsers by name</summary>
        IReadOnlyDictionary<string, IDictMessageParser> Parsers { get; }

        /// <summary>Initialize from JSON metadata file</summary>
        void Init(string jsonMetaDesFile, Assembly typesAssembly);

        /// <summary>
        /// Find parser by SenderCompID (tag 49) and TargetCompID (tag 56).
        /// Returns first matching entry or null.
        /// </summary>
        IDictMessageParser? FindByCompIds(string? senderCompId, string? targetCompId);

        /// <summary>Get the default parser (marked with IsDefault=true)</summary>
        IDictMessageParser? GetDefault();

        /// <summary>Get all registry metadata entries</summary>
        IEnumerable<DictMeta> GetAllMetas();
    }
}