namespace PureFix.Transport.Store;

/// <summary>
/// Identifies a FIX session for file naming and lookup.
/// Format: {BeginString}-{SenderCompID}-{TargetCompID}
/// </summary>
public sealed record SessionId(string BeginString, string SenderCompID, string TargetCompID)
{
    /// <summary>
    /// Creates a file prefix for QuickFix-compatible file naming.
    /// Example: "FIX.4.4-SENDER-TARGET"
    /// </summary>
    public string ToFilePrefix() => $"{BeginString}-{SenderCompID}-{TargetCompID}";

    /// <summary>
    /// Gets the full path for a specific file extension.
    /// </summary>
    /// <param name="directory">Base directory for store files</param>
    /// <param name="extension">File extension (e.g., "seqnums", "session", "header", "body")</param>
    public string GetFilePath(string directory, string extension)
        => Path.Combine(directory, $"{ToFilePrefix()}.{extension}");

    public override string ToString() => ToFilePrefix();
}
