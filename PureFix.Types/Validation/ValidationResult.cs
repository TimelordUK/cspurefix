namespace PureFix.Types.Validation;

/// <summary>
/// Collects validation warnings during message parsing and provides
/// the overall validation outcome.
/// </summary>
public sealed class ValidationResult
{
    private readonly List<ValidationWarning> _warnings = [];

    /// <summary>
    /// Whether any warnings were recorded.
    /// </summary>
    public bool HasWarnings => _warnings.Count > 0;

    /// <summary>
    /// The collected warnings.
    /// </summary>
    public IReadOnlyList<ValidationWarning> Warnings => _warnings;

    /// <summary>
    /// Count of warnings by type, for quick filtering.
    /// </summary>
    public int WarningCount => _warnings.Count;

    /// <summary>
    /// Adds a warning to the result.
    /// </summary>
    public void AddWarning(ValidationWarning warning)
    {
        _warnings.Add(warning);
    }

    /// <summary>
    /// Adds multiple warnings.
    /// </summary>
    public void AddWarnings(IEnumerable<ValidationWarning> warnings)
    {
        _warnings.AddRange(warnings);
    }

    /// <summary>
    /// Gets warnings of a specific type.
    /// </summary>
    public IEnumerable<ValidationWarning> GetWarnings(ValidationWarningType type)
    {
        return _warnings.Where(w => w.Type == type);
    }

    /// <summary>
    /// Checks if there are any warnings of the specified types.
    /// </summary>
    public bool HasWarningTypes(params ValidationWarningType[] types)
    {
        var typeSet = new HashSet<ValidationWarningType>(types);
        return _warnings.Any(w => typeSet.Contains(w.Type));
    }

    /// <summary>
    /// Gets all unknown field warnings.
    /// </summary>
    public IEnumerable<ValidationWarning> UnknownFields =>
        _warnings.Where(w => w.Type is ValidationWarningType.UnknownField
                          or ValidationWarningType.UnknownFieldInComponent);

    /// <summary>
    /// Gets all missing required field warnings.
    /// </summary>
    public IEnumerable<ValidationWarning> MissingFields =>
        _warnings.Where(w => w.Type == ValidationWarningType.RequiredFieldMissing);

    /// <summary>
    /// Determines if the message should be rejected based on the validation mode.
    /// </summary>
    /// <param name="mode">The validation mode to apply.</param>
    /// <returns>True if the message should be rejected.</returns>
    public bool ShouldReject(ValidationMode mode)
    {
        if (mode == ValidationMode.None || !HasWarnings)
            return false;

        if (mode == ValidationMode.Lenient)
        {
            // In lenient mode, only reject for critical protocol errors
            return HasWarningTypes(
                ValidationWarningType.ChecksumMismatch,
                ValidationWarningType.BodyLengthMismatch);
        }

        // Strict mode - any warning is a rejection
        return true;
    }

    /// <summary>
    /// Clears all warnings. Useful when reusing the result for pooled objects.
    /// </summary>
    public void Clear()
    {
        _warnings.Clear();
    }

    /// <summary>
    /// Creates an empty result (no warnings).
    /// </summary>
    public static ValidationResult Empty { get; } = new();

    public override string ToString()
    {
        if (!HasWarnings)
            return "ValidationResult: No warnings";
        return $"ValidationResult: {WarningCount} warning(s) - {string.Join(", ", _warnings.Select(w => w.Type))}";
    }
}
