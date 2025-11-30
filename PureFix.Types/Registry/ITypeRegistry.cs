using PureFix.Types.Config;

namespace PureFix.Types.Registry;

/// <summary>
/// Central registry for managing multiple FIX type systems.
/// Enables dynamic loading and lookup of broker-specific type assemblies.
/// </summary>
public interface ITypeRegistry
{
    /// <summary>
    /// Load registrations from a JSON configuration file
    /// </summary>
    /// <param name="path">Path to TypeRegistry.json</param>
    void LoadFromFile(string path);

    /// <summary>
    /// Load registrations from JSON string
    /// </summary>
    /// <param name="json">JSON configuration content</param>
    void LoadFromJson(string json);

    /// <summary>
    /// Register a type system programmatically
    /// </summary>
    void Register(TypeRegistration registration);

    /// <summary>
    /// Register a type system with its provider directly (for pre-loaded assemblies)
    /// </summary>
    void Register(TypeRegistration registration, ITypeSystemProvider provider);

    /// <summary>
    /// Get registration by name (e.g., "fix44", "jpmfx")
    /// </summary>
    TypeRegistration? GetByName(string name);

    /// <summary>
    /// Find registration by SenderCompID (tag 49)
    /// </summary>
    TypeRegistration? FindBySenderCompId(string senderCompId);

    /// <summary>
    /// Find registration by both SenderCompID and TargetCompID
    /// </summary>
    TypeRegistration? FindByCompIds(string senderCompId, string targetCompId);

    /// <summary>
    /// Get the default registration (if one is marked as default)
    /// </summary>
    TypeRegistration? GetDefault();

    /// <summary>
    /// Get all enabled registrations
    /// </summary>
    IEnumerable<TypeRegistration> GetAllEnabled();

    /// <summary>
    /// Get all registrations (including disabled)
    /// </summary>
    IEnumerable<TypeRegistration> GetAll();

    /// <summary>
    /// Get the type system provider for a registration.
    /// Loads the assembly if not already loaded.
    /// </summary>
    ITypeSystemProvider? GetProvider(string registrationName);

    /// <summary>
    /// Create a message factory for a registration
    /// </summary>
    IFixMessageFactory? CreateMessageFactory(string registrationName);

    /// <summary>
    /// Create a session message factory for a registration
    /// </summary>
    ISessionMessageFactory? CreateSessionMessageFactory(string registrationName, ISessionDescription sessionDescription);

    /// <summary>
    /// Check if a registration exists
    /// </summary>
    bool Contains(string name);

    /// <summary>
    /// Remove a registration by name
    /// </summary>
    bool Remove(string name);

    /// <summary>
    /// Clear all registrations
    /// </summary>
    void Clear();
}
