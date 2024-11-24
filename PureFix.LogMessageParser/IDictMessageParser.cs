using PureFix.Buffer;
using PureFix.Dictionary.Definition;
using PureFix.Types;
using SeeFixServer.State;
using System.Reflection;

namespace PureFix.LogMessageParser
{
    public interface IDictMessageParser
    {
        IFixDefinitions Definitions { get; }
        IFixMessageFactory? MessageFactory { get; }
        TypeInfo? MessageFactoryTypeInfo { get; }
        DictMeta Meta { get; }
        ParseResult Parse(ParseRequest request);
    }
}