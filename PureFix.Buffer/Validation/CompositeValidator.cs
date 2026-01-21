using PureFix.Types;
using PureFix.Types.Validation;

namespace PureFix.Buffer.Validation;

/// <summary>
/// Combines multiple validators into a single validator.
/// Runs all validators in sequence, accumulating warnings.
/// </summary>
public class CompositeValidator : IMessageValidator
{
    private readonly IMessageValidator[] _validators;

    public CompositeValidator(params IMessageValidator[] validators)
    {
        _validators = validators;
    }

    public CompositeValidator(IEnumerable<IMessageValidator> validators)
    {
        _validators = validators.ToArray();
    }

    public void Validate(IMessageView view, ValidationResult result)
    {
        foreach (var validator in _validators)
        {
            validator.Validate(view, result);
        }
    }

    /// <summary>
    /// Creates an empty composite (no validation).
    /// </summary>
    public static CompositeValidator Empty { get; } = new();
}
