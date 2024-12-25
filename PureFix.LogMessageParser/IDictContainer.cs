using System.Reflection;

namespace PureFix.LogMessageParser
{
    public interface IDictContainer
    {
        IDictMessageParser? this[string name] { get; }
        int Count { get; }
        IReadOnlyDictionary<string, IDictMessageParser> Parsers { get; }
        void Init(string jsonMetaDesFile, Assembly typesAssembly);
    }
}