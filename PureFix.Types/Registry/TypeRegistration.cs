using System.Text.Json.Serialization;

namespace PureFix.Types.Registry;

/// <summary>
/// Metadata describing a registered FIX type system.
/// Can be loaded from TypeRegistry.json configuration.
/// </summary>
public class TypeRegistration
{
    /// <summary>
    /// Unique identifier for this registration (e.g., "jpmfx", "fix44")
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Human-readable name (e.g., "JPMorgan FIX 4.4")
    /// </summary>
    [JsonPropertyName("displayName")]
    public string DisplayName { get; set; } = string.Empty;

    /// <summary>
    /// Path to QuickFix XML dictionary file
    /// </summary>
    [JsonPropertyName("dictionaryPath")]
    public string DictionaryPath { get; set; } = string.Empty;

    /// <summary>
    /// Path to assembly containing generated types.
    /// Can be relative to the registry file or absolute.
    /// </summary>
    [JsonPropertyName("assemblyPath")]
    public string? AssemblyPath { get; set; }

    /// <summary>
    /// Fully qualified type name of the TypeSystemProvider class.
    /// If not specified, defaults to "{RootNamespace}.TypeSystemProvider"
    /// </summary>
    [JsonPropertyName("providerTypeName")]
    public string? ProviderTypeName { get; set; }

    /// <summary>
    /// SenderCompIDs that should use this dictionary (tag 49).
    /// Use "*" for wildcard matching.
    /// </summary>
    [JsonPropertyName("senderCompIds")]
    public List<string> SenderCompIds { get; set; } = new();

    /// <summary>
    /// TargetCompIDs that should use this dictionary (tag 56).
    /// Use "*" for wildcard matching.
    /// </summary>
    [JsonPropertyName("targetCompIds")]
    public List<string> TargetCompIds { get; set; } = new();

    /// <summary>
    /// FIX version string (e.g., "FIX.4.4")
    /// </summary>
    [JsonPropertyName("version")]
    public string Version { get; set; } = string.Empty;

    /// <summary>
    /// Root namespace in the assembly (e.g., "PureFix.Types.FIX44")
    /// </summary>
    [JsonPropertyName("rootNamespace")]
    public string RootNamespace { get; set; } = string.Empty;

    /// <summary>
    /// Path to message store directory for session persistence.
    /// Used for sequence number recovery.
    /// </summary>
    [JsonPropertyName("messageStorePath")]
    public string? MessageStorePath { get; set; }

    /// <summary>
    /// Whether this registration is active
    /// </summary>
    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// Whether this is the default/fallback dictionary when no match is found
    /// </summary>
    [JsonPropertyName("isDefault")]
    public bool IsDefault { get; set; }

    /// <summary>
    /// Optional tags for categorization (e.g., "broker", "standard", "custom")
    /// </summary>
    [JsonPropertyName("tags")]
    public List<string> Tags { get; set; } = new();

    /// <summary>
    /// Matches a SenderCompID against this registration's patterns
    /// </summary>
    public bool MatchesSenderCompId(string senderCompId)
    {
        if (SenderCompIds.Count == 0) return false;
        return SenderCompIds.Any(pattern => MatchesPattern(pattern, senderCompId));
    }

    /// <summary>
    /// Matches both SenderCompID and TargetCompID
    /// </summary>
    public bool MatchesCompIds(string senderCompId, string targetCompId)
    {
        var matchesSender = SenderCompIds.Count == 0 || SenderCompIds.Any(p => MatchesPattern(p, senderCompId));
        var matchesTarget = TargetCompIds.Count == 0 || TargetCompIds.Any(p => MatchesPattern(p, targetCompId));
        return matchesSender && matchesTarget;
    }

    private static bool MatchesPattern(string pattern, string value)
    {
        if (pattern == "*") return true;
        if (pattern.EndsWith("*"))
        {
            var prefix = pattern[..^1];
            return value.StartsWith(prefix, StringComparison.OrdinalIgnoreCase);
        }
        return string.Equals(pattern, value, StringComparison.OrdinalIgnoreCase);
    }

    public override string ToString() => $"{Name} ({DisplayName}) - {Version}";
}

/// <summary>
/// Root configuration object for TypeRegistry.json
/// </summary>
public class TypeRegistryConfig
{
    /// <summary>
    /// Configuration file version
    /// </summary>
    [JsonPropertyName("version")]
    public string Version { get; set; } = "1.0";

    /// <summary>
    /// List of type registrations
    /// </summary>
    [JsonPropertyName("registrations")]
    public List<TypeRegistration> Registrations { get; set; } = new();
}
