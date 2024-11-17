using PureFix.LogMessageParser;
using System.Reflection;

namespace PureFix.Dictionary.Contained
{
    public interface IDictContainer
    {
        IDictMessageParser? this[string name] { get; }
        int Count { get; }
        IReadOnlyDictionary<string, IDictMessageParser> Parsers { get; }
        void Init(string jsonMetaDesFile, Assembly typesAssembly);
    }
}