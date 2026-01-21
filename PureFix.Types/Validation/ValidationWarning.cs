namespace PureFix.Types.Validation;

/// <summary>
/// Represents a single validation warning encountered during message parsing.
/// Warnings are non-fatal issues that may or may not cause message rejection
/// depending on the configured ValidationMode.
/// </summary>
public sealed class ValidationWarning
{
    /// <summary>
    /// The category of validation issue.
    /// </summary>
    public ValidationWarningType Type { get; }

    /// <summary>
    /// The FIX tag number involved, or 0 if not applicable.
    /// </summary>
    public int Tag { get; }

    /// <summary>
    /// The raw value that caused the warning, if available.
    /// </summary>
    public string? Value { get; }

    /// <summary>
    /// Context where the warning occurred (e.g., component name, group name).
    /// Helps identify exactly where in a nested message structure the issue was found.
    /// </summary>
    public string? Context { get; }

    /// <summary>
    /// Human-readable description of the warning.
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// Position in the raw message where the issue was found, if applicable.
    /// </summary>
    public int? Position { get; }

    public ValidationWarning(
        ValidationWarningType type,
        int tag,
        string? value,
        string? context,
        string message,
        int? position = null)
    {
        Type = type;
        Tag = tag;
        Value = value;
        Context = context;
        Message = message;
        Position = position;
    }

    /// <summary>
    /// Creates a warning for an unknown field.
    /// </summary>
    public static ValidationWarning UnknownField(int tag, string? value, string? context = null, int? position = null)
    {
        var msg = context != null
            ? $"Unknown field {tag} in {context}"
            : $"Unknown field {tag}";
        return new ValidationWarning(
            context != null ? ValidationWarningType.UnknownFieldInComponent : ValidationWarningType.UnknownField,
            tag, value, context, msg, position);
    }

    /// <summary>
    /// Creates a warning for an enum value out of range.
    /// </summary>
    public static ValidationWarning EnumOutOfRange(int tag, string? value, string? context = null)
    {
        return new ValidationWarning(
            ValidationWarningType.EnumOutOfRange,
            tag, value, context,
            $"Enum value '{value}' not in defined set for tag {tag}");
    }

    /// <summary>
    /// Creates a warning for a required field that is missing.
    /// </summary>
    public static ValidationWarning RequiredFieldMissing(int tag, string? context = null)
    {
        var msg = context != null
            ? $"Required field {tag} missing in {context}"
            : $"Required field {tag} missing";
        return new ValidationWarning(
            ValidationWarningType.RequiredFieldMissing,
            tag, null, context, msg);
    }

    /// <summary>
    /// Creates a warning for checksum mismatch.
    /// </summary>
    public static ValidationWarning ChecksumMismatch(int received, int computed)
    {
        return new ValidationWarning(
            ValidationWarningType.ChecksumMismatch,
            10, // CheckSum tag
            received.ToString(),
            null,
            $"Checksum mismatch: received {received}, computed {computed}");
    }

    /// <summary>
    /// Creates a warning for body length mismatch.
    /// </summary>
    public static ValidationWarning BodyLengthMismatch(int declared, int actual)
    {
        return new ValidationWarning(
            ValidationWarningType.BodyLengthMismatch,
            9, // BodyLength tag
            declared.ToString(),
            null,
            $"BodyLength mismatch: declared {declared}, actual {actual}");
    }

    /// <summary>
    /// Creates a warning for group count mismatch.
    /// </summary>
    public static ValidationWarning GroupCountMismatch(int tag, int declared, int actual, string? groupName = null)
    {
        return new ValidationWarning(
            ValidationWarningType.GroupCountMismatch,
            tag,
            declared.ToString(),
            groupName,
            $"Group count mismatch for tag {tag}: declared {declared}, found {actual}");
    }

    public override string ToString()
    {
        var ctx = Context != null ? $" [{Context}]" : "";
        var pos = Position.HasValue ? $" @{Position}" : "";
        return $"{Type}: Tag {Tag}{ctx}{pos} - {Message}";
    }
}
