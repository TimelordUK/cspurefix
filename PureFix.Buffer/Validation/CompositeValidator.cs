using PureFix.Types;
using PureFix.Types.Validation;

namespace PureFix.Buffer.Validation;

/// <summary>
/// Combines multiple validators into a single validator.
/// Runs all validators in sequence, accumulating warnings.
/// </summary>
public class CompositeValidator(params IMessageValidator[] validators) : IMessageValidator
{
    public CompositeValidator(IEnumerable<IMessageValidator> validators) : this(validators.ToArray())
    {
    }

    public void Validate(IMessageView view, ValidationResult result)
    {
        foreach (var validator in validators)
        {
            validator.Validate(view, result);
        }
    }

    /// <summary>
    /// Creates an empty composite (no validation).
    /// </summary>
    public static CompositeValidator Empty { get; } = new();
}
