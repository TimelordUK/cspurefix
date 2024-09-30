using PureFix.Dictionary.Definition;
using PureFix.Types;
using PureFix.Types.Config;

namespace PureFix.Transport.Session
{
    public interface IFixConfig
    {
        byte? LogDelimiter { get; set; }
        byte? Delimiter { get; set; }
        IFixDefinitions? Definitions { get; }
        ISessionDescription? Description { get; }
        ISessionMessageFactory? MessageFactory { get; }
    }
}
