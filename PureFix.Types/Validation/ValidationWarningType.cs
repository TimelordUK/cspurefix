namespace PureFix.Types.Validation;

/// <summary>
/// Categories of validation warnings that can occur during message parsing.
/// </summary>
public enum ValidationWarningType
{
    /// <summary>
    /// Field tag not found in dictionary at message level.
    /// </summary>
    UnknownField,

    /// <summary>
    /// Field tag appeared within a component's span but wasn't recognized.
    /// More specific than UnknownField - indicates where the unknown field appeared.
    /// </summary>
    UnknownFieldInComponent,

    /// <summary>
    /// Enum value not in the defined set for this field.
    /// E.g., OrdStatus='Z' when only 'A','B','C' are defined.
    /// </summary>
    EnumOutOfRange,

    /// <summary>
    /// Type coercion was applied to parse the value.
    /// E.g., float "123.45" truncated to int 123.
    /// </summary>
    TypeCoerced,

    /// <summary>
    /// A required field was not present in the message.
    /// </summary>
    RequiredFieldMissing,

    /// <summary>
    /// The NoXxx count field doesn't match actual group instance count.
    /// </summary>
    GroupCountMismatch,

    /// <summary>
    /// Field appeared out of expected order.
    /// May prevent structured parsing of components/groups.
    /// </summary>
    FieldOutOfOrder,

    /// <summary>
    /// Checksum value doesn't match computed checksum.
    /// </summary>
    ChecksumMismatch,

    /// <summary>
    /// BodyLength value doesn't match actual body length.
    /// </summary>
    BodyLengthMismatch,

    /// <summary>
    /// Field value failed format validation (e.g., invalid date format).
    /// </summary>
    InvalidFormat
}
