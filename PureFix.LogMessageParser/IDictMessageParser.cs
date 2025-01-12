using PureFix.Buffer;
using PureFix.Buffer.Ascii;
using PureFix.Dictionary.Definition;
using PureFix.Types;
using System.Reflection;

namespace PureFix.LogMessageParser
{
    public interface IDictMessageParser
    {
        IFixDefinitions Definitions { get; }
        IFixMessageFactory? MessageFactory { get; }
        TypeInfo? MessageFactoryTypeInfo { get; }
        DictMeta Meta { get; }
        (ParseResult result, List<AsciiView> views) Parse(ParseRequest request);
        ParseResult Structure(ParseRequest request);
    }
}