using PureFix.Types.Config;

namespace PureFix.Types.Registry;

/// <summary>
/// Interface that all generated type systems must implement.
/// Allows the registry to create factories dynamically via reflection.
/// </summary>
public interface ITypeSystemProvider
{
    /// <summary>
    /// Get the FIX version this type system supports (e.g., "FIX.4.4")
    /// </summary>
    string GetVersion();

    /// <summary>
    /// Create a message factory instance for converting views to typed messages
    /// </summary>
    IFixMessageFactory CreateMessageFactory();

    /// <summary>
    /// Create a session message factory instance for creating session-level messages
    /// </summary>
    ISessionMessageFactory CreateSessionMessageFactory(ISessionDescription sessionDescription);

    /// <summary>
    /// Get all message types in this type system
    /// </summary>
    IEnumerable<Type> GetMessageTypes();

    /// <summary>
    /// Get message type by MsgType tag value (e.g., "D" -> NewOrderSingle)
    /// </summary>
    Type? GetMessageTypeByMsgType(string msgType);

    /// <summary>
    /// Get the root namespace of this type system (e.g., "PureFix.Types.FIX44")
    /// </summary>
    string GetRootNamespace();
}
