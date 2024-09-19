using PureFix.Dictionary.Definition;
using PureFix.Types;
using PureFix.Types.Config;

namespace PureFix.Transport.Session
{
    public interface IFixConfig
    {
        byte? LogDelimiter { get; }
        byte? Delimiter { get; }
        ILogFactory LogFactory { get; }
        IFixDefinitions Definitions { get; }
        ISessionDescription Description { get; }
    }
}
