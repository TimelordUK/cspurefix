namespace PureFix.Types.Validation;

/// <summary>
/// Configuration for message validation behavior.
/// Can be specified in session config JSON or programmatically.
/// </summary>
public class ValidationConfig
{
    /// <summary>
    /// The overall validation strictness level.
    /// Default depends on session role (Lenient for initiators, Strict for acceptors).
    /// </summary>
    public ValidationMode Mode { get; set; } = ValidationMode.Lenient;

    /// <summary>
    /// Whether to validate checksum on received messages.
    /// Default: true
    /// </summary>
    public bool CheckChecksum { get; set; } = true;

    /// <summary>
    /// Whether to validate body length on received messages.
    /// Default: true
    /// </summary>
    public bool CheckBodyLength { get; set; } = true;

    /// <summary>
    /// Whether to allow fields not defined in the dictionary.
    /// When true, unknown fields generate warnings but don't cause rejection (in Lenient mode).
    /// Default: true
    /// </summary>
    public bool AllowUnknownFields { get; set; } = true;

    /// <summary>
    /// Whether to allow enum values outside the defined set.
    /// When true, out-of-range enums generate warnings but are accepted.
    /// Default: false (unknown enums rejected even in Lenient mode)
    /// </summary>
    public bool AllowEnumOutOfRange { get; set; } = false;

    /// <summary>
    /// Whether to coerce float values to int when the field expects int.
    /// E.g., "123.45" becomes 123.
    /// Default: true
    /// </summary>
    public bool CoerceFloatToInt { get; set; } = true;

    /// <summary>
    /// Whether to check required fields are present.
    /// Default: true
    /// </summary>
    public bool CheckRequiredFields { get; set; } = true;

    /// <summary>
    /// Whether to check repeating group counts match actual instances.
    /// Default: true
    /// </summary>
    public bool CheckGroupCounts { get; set; } = true;

    /// <summary>
    /// Callback invoked for each validation warning.
    /// Useful for logging or custom handling.
    /// </summary>
    public Action<ValidationWarning>? OnWarning { get; set; }

    /// <summary>
    /// Per-field validation overrides.
    /// Key is the tag number.
    /// </summary>
    public Dictionary<int, FieldValidationConfig>? FieldOverrides { get; set; }

    /// <summary>
    /// Creates default config for an initiator (client).
    /// Lenient mode - accepts unknown fields, logs warnings.
    /// </summary>
    public static ValidationConfig ForInitiator()
    {
        return new ValidationConfig
        {
            Mode = ValidationMode.Lenient,
            AllowUnknownFields = true,
            AllowEnumOutOfRange = false,
            CoerceFloatToInt = true
        };
    }

    /// <summary>
    /// Creates default config for an acceptor (server).
    /// Strict mode - rejects malformed messages.
    /// </summary>
    public static ValidationConfig ForAcceptor()
    {
        return new ValidationConfig
        {
            Mode = ValidationMode.Strict,
            AllowUnknownFields = false,
            AllowEnumOutOfRange = false,
            CoerceFloatToInt = false
        };
    }

    /// <summary>
    /// Creates config with no validation (for logging/replay).
    /// </summary>
    public static ValidationConfig NoValidation()
    {
        return new ValidationConfig
        {
            Mode = ValidationMode.None,
            CheckChecksum = false,
            CheckBodyLength = false,
            CheckRequiredFields = false,
            CheckGroupCounts = false,
            AllowUnknownFields = true,
            AllowEnumOutOfRange = true
        };
    }
}

/// <summary>
/// Per-field validation configuration overrides.
/// </summary>
public class FieldValidationConfig
{
    /// <summary>
    /// Override: allow unknown enum values for this field.
    /// </summary>
    public bool? AllowUnknownEnum { get; set; }

    /// <summary>
    /// Override: whether this field is required.
    /// </summary>
    public bool? Required { get; set; }

    /// <summary>
    /// Additional date/time formats to accept for this field.
    /// </summary>
    public string[]? DateFormats { get; set; }
}
