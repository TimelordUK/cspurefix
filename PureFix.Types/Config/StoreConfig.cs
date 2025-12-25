namespace PureFix.Types.Config;

/// <summary>
/// Configuration for session message store.
/// </summary>
public class StoreConfig
{
    /// <summary>
    /// Store type: "memory" or "file"
    /// </summary>
    public string Type { get; set; } = "memory";

    /// <summary>
    /// Directory for file-based store (only used when Type is "file").
    /// Uses QuickFix-compatible file format.
    /// </summary>
    public string? Directory { get; set; }
}
