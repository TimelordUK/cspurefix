using PureFix.Types.Core;

namespace PureFix.Types.Validation;

/// <summary>
/// Validates FIX messages according to configured rules.
/// Implementations can be composed to build validation pipelines.
/// </summary>
public interface IMessageValidator
{
    /// <summary>
    /// Validates a parsed message view.
    /// </summary>
    /// <param name="view">The message view to validate.</param>
    /// <param name="result">Result object to accumulate warnings.</param>
    void Validate(IMessageView view, ValidationResult result);
}

/// <summary>
/// Extension methods for IMessageValidator.
/// </summary>
public static class MessageValidatorExtensions
{
    /// <summary>
    /// Validates a message and returns the result.
    /// </summary>
    public static ValidationResult Validate(this IMessageValidator validator, IMessageView view)
    {
        var result = new ValidationResult();
        validator.Validate(view, result);
        return result;
    }

    /// <summary>
    /// Validates a message and determines if it should be rejected.
    /// </summary>
    public static bool ShouldReject(this IMessageValidator validator, IMessageView view, ValidationMode mode)
    {
        var result = validator.Validate(view);
        return result.ShouldReject(mode);
    }
}
