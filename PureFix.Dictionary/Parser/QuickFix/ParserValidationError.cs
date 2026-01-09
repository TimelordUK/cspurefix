namespace PureFix.Dictionary.Parser.QuickFix;

/// <summary>
/// Severity level for validation errors
/// </summary>
public enum ValidationSeverity
{
    Warning,
    Error
}

/// <summary>
/// Represents a validation error found during XML dictionary parsing
/// </summary>
public record ParserValidationError(
    ValidationSeverity Severity,
    string Code,
    string Message,
    string? ElementName = null,
    string? ElementType = null,
    int? LineNumber = null,
    string? Suggestion = null)
{
    public override string ToString()
    {
        var parts = new List<string> { $"[{Severity}] {Code}: {Message}" };

        if (ElementType != null && ElementName != null)
        {
            parts.Add($"  Element: {ElementType} '{ElementName}'");
        }

        if (LineNumber.HasValue)
        {
            parts.Add($"  Line: {LineNumber}");
        }

        if (Suggestion != null)
        {
            parts.Add($"  Suggestion: {Suggestion}");
        }

        return string.Join(Environment.NewLine, parts);
    }
}

/// <summary>
/// Exception thrown when XML dictionary validation fails
/// </summary>
public class DictionaryValidationException(IReadOnlyList<ParserValidationError> errors)
    : Exception(FormatMessage(errors))
{
    public IReadOnlyList<ParserValidationError> Errors { get; } = errors;

    private static string FormatMessage(IReadOnlyList<ParserValidationError> errors)
    {
        var errorCount = errors.Count(e => e.Severity == ValidationSeverity.Error);
        var warningCount = errors.Count(e => e.Severity == ValidationSeverity.Warning);

        var header = $"FIX dictionary validation failed with {errorCount} error(s) and {warningCount} warning(s):";
        var details = string.Join(Environment.NewLine + Environment.NewLine, errors.Select(e => e.ToString()));

        return $"{header}{Environment.NewLine}{Environment.NewLine}{details}";
    }
}
