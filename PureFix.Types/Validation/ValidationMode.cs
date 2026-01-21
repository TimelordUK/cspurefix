namespace PureFix.Types.Validation;

/// <summary>
/// Defines the overall validation strictness level for message parsing and processing.
/// </summary>
public enum ValidationMode
{
    /// <summary>
    /// No validation - parse whatever we can, no errors thrown.
    /// Suitable for logging/replay scenarios where messages may be malformed.
    /// Unknown fields are silently accepted, type mismatches ignored.
    /// </summary>
    None,

    /// <summary>
    /// Lenient - warn on issues but continue parsing.
    /// Unknown fields preserved, type mismatches coerced where safe.
    /// Warnings collected but don't cause rejection.
    /// Recommended for initiators/clients connecting to exchanges.
    /// </summary>
    Lenient,

    /// <summary>
    /// Strict - QuickFix-compatible validation.
    /// Reject malformed messages, unknown fields, type mismatches.
    /// Recommended for acceptors/servers enforcing protocol compliance.
    /// </summary>
    Strict
}
