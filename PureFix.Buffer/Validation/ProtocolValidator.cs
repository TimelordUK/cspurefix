using PureFix.Types;
using PureFix.Types.Validation;

namespace PureFix.Buffer.Validation;

/// <summary>
/// Validates FIX protocol-level integrity: checksum and body length.
/// These are fundamental protocol requirements that should typically be checked.
/// </summary>
public class ProtocolValidator : IMessageValidator
{
    private const int CheckSumTag = 10;
    private const int BodyLengthTag = 9;

    private readonly bool _checkChecksum;
    private readonly bool _checkBodyLength;

    /// <summary>
    /// Creates a protocol validator with the specified checks enabled.
    /// </summary>
    /// <param name="checkChecksum">Whether to validate checksum (tag 10).</param>
    /// <param name="checkBodyLength">Whether to validate body length (tag 9).</param>
    public ProtocolValidator(bool checkChecksum = true, bool checkBodyLength = true)
    {
        _checkChecksum = checkChecksum;
        _checkBodyLength = checkBodyLength;
    }

    /// <summary>
    /// Creates a protocol validator from ValidationConfig settings.
    /// </summary>
    public static ProtocolValidator FromConfig(ValidationConfig? config)
    {
        if (config == null)
        {
            return new ProtocolValidator();
        }

        return new ProtocolValidator(
            checkChecksum: config.CheckChecksum,
            checkBodyLength: config.CheckBodyLength);
    }

    /// <summary>
    /// Validates the message's protocol-level integrity.
    /// </summary>
    public void Validate(IMessageView view, ValidationResult result)
    {
        if (_checkChecksum)
        {
            ValidateChecksum(view, result);
        }

        if (_checkBodyLength)
        {
            ValidateBodyLength(view, result);
        }
    }

    private static void ValidateChecksum(IMessageView view, ValidationResult result)
    {
        if (!view.TryGetInt32(CheckSumTag, out var received))
        {
            result.AddWarning(ValidationWarning.RequiredFieldMissing(CheckSumTag, "trailer"));
            return;
        }

        var computed = view.Checksum();
        if (received != computed)
        {
            result.AddWarning(ValidationWarning.ChecksumMismatch(received, computed));
        }
    }

    private static void ValidateBodyLength(IMessageView view, ValidationResult result)
    {
        if (!view.TryGetInt32(BodyLengthTag, out _))
        {
            result.AddWarning(ValidationWarning.RequiredFieldMissing(BodyLengthTag, "header"));
            return;
        }

        // Body length validation requires access to the actual body length
        // which is computed during parsing. The view exposes Checksum() but not
        // the body length computation. For now, we just verify the tag is present.
        //
        // TODO: Add IMessageView.ComputedBodyLength property or similar to enable
        // full body length validation. The parser already tracks this in AsciiParseState.
        //
        // When implemented:
        // var actual = view.ComputedBodyLength;
        // if (declared != actual)
        // {
        //     result.AddWarning(ValidationWarning.BodyLengthMismatch(declared, actual));
        // }
    }
}
