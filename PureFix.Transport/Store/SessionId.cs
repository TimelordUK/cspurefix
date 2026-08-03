using System.Text;

namespace PureFix.Transport.Store;

/// <summary>
/// Identifies a FIX session for file naming and lookup.
/// Format: {BeginString}-{SenderCompID}-{TargetCompID}
/// </summary>
public sealed record SessionId(string BeginString, string SenderCompID, string TargetCompID)
{
    /// <summary>
    /// Characters that are illegal or dangerous in a file name. Path.GetInvalidFileNameChars
    /// is platform-specific (on Linux it is only NUL and '/'), so the Windows set is listed
    /// explicitly to keep store file names identical across platforms.
    /// </summary>
    private static readonly char[] Unsafe =
        ['/', '\\', ':', '*', '?', '"', '<', '>', '|', '\0', '\r', '\n', '\t'];

    /// <summary>
    /// Creates a file prefix for QuickFix-compatible file naming.
    /// Example: "FIX.4.4-SENDER-TARGET"
    /// </summary>
    /// <remarks>
    /// Components are sanitised because a wildcard acceptor takes TargetCompID straight from
    /// the peer's Logon tag 49. Without this an untrusted counterparty could pick the store's
    /// file name - "../../.." to write outside the configured directory, or a character the
    /// platform rejects, which would fail the session on connect. Ordinary CompIDs are
    /// alphanumeric and pass through unchanged, so existing store files keep their names.
    /// </remarks>
    public string ToFilePrefix() =>
        $"{Sanitize(BeginString)}-{Sanitize(SenderCompID)}-{Sanitize(TargetCompID)}";

    /// <summary>
    /// Gets the full path for a specific file extension.
    /// </summary>
    /// <param name="directory">Base directory for store files</param>
    /// <param name="extension">File extension (e.g., "seqnums", "session", "header", "body")</param>
    public string GetFilePath(string directory, string extension)
    {
        var path = Path.Combine(directory, $"{ToFilePrefix()}.{extension}");

        // Defence in depth: whatever Sanitize let through, the result must still land
        // directly inside the configured directory.
        var root = Path.TrimEndingDirectorySeparator(Path.GetFullPath(directory));
        var resolved = Path.GetFullPath(path);
        if (Path.GetDirectoryName(resolved) != root)
        {
            throw new InvalidOperationException(
                $"session store path '{resolved}' escapes the store directory '{root}'");
        }

        return path;
    }

    private static string Sanitize(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return "_";

        var sb = new StringBuilder(value.Length);
        foreach (var c in value)
        {
            sb.Append(Array.IndexOf(Unsafe, c) >= 0 || char.IsControl(c) ? '_' : c);
        }

        // "FIX.4.4" must survive, but no component may navigate upwards.
        sb.Replace("..", "__");

        // Windows silently strips trailing dots and spaces, which would make two distinct
        // CompIDs resolve to one file.
        var cleaned = sb.ToString().TrimEnd('.', ' ');
        return cleaned.Length > 0 ? cleaned : "_";
    }

    /// <summary>
    /// The session's identity. Unlike <see cref="ToFilePrefix"/> this is not sanitised, so
    /// two counterparties whose CompIDs differ only in unsafe characters remain distinct
    /// keys in the session registry.
    /// </summary>
    public override string ToString() => $"{BeginString}-{SenderCompID}-{TargetCompID}";
}
