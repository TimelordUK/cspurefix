namespace PureFix.Dictionary.Parser;

/// <summary>
/// Represents an enum value from a FIX field definition.
/// The Key is the raw value (e.g., "1", "Y", "+") and Description is the human-readable name.
/// Val provides a sanitized C# identifier-safe version of the Description.
/// </summary>
public record FieldEnum(string Key, string Description)
{
    /// <summary>
    /// Gets a sanitized version of the Description suitable for use as a C# identifier.
    /// Uses CSharpIdentifierSanitizer for robust handling of special characters,
    /// numbers at start, C# keywords, etc.
    /// </summary>
    public string Val => _val ??= CSharpIdentifierSanitizer.SanitizeStatic(Description);
    private string? _val;
}
