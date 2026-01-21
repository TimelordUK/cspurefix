using PureFix.Types.Validation;

namespace PureFix.Buffer.Validation;

/// <summary>
/// Factory for creating message validators from configuration.
/// </summary>
public static class MessageValidatorFactory
{
    /// <summary>
    /// Creates a validator based on ValidationConfig.
    /// Returns a composite of all enabled validators.
    /// </summary>
    /// <param name="config">The validation configuration. If null, uses defaults.</param>
    /// <param name="isAcceptor">Whether this is for an acceptor (server) session.</param>
    /// <returns>A configured message validator.</returns>
    public static IMessageValidator Create(ValidationConfig? config, bool isAcceptor = false)
    {
        // If no config provided, use role-based defaults
        config ??= isAcceptor
            ? ValidationConfig.ForAcceptor()
            : ValidationConfig.ForInitiator();

        // No validation mode - return empty validator
        if (config.Mode == ValidationMode.None)
        {
            return CompositeValidator.Empty;
        }

        var validators = new List<IMessageValidator>();

        // Always add protocol validator if any protocol checks are enabled
        if (config.CheckChecksum || config.CheckBodyLength)
        {
            validators.Add(ProtocolValidator.FromConfig(config));
        }

        // Future: Add structure validator for required fields, unknown fields, etc.
        // if (config.CheckRequiredFields || config.AllowUnknownFields == false)
        // {
        //     validators.Add(StructureValidator.FromConfig(config));
        // }

        // Future: Add type validator for enum values, type coercion, etc.
        // if (!config.AllowEnumOutOfRange || config.CoerceFloatToInt)
        // {
        //     validators.Add(TypeValidator.FromConfig(config));
        // }

        return validators.Count switch
        {
            0 => CompositeValidator.Empty,
            1 => validators[0],
            _ => new CompositeValidator(validators)
        };
    }

    /// <summary>
    /// Creates a validator for initiator (client) sessions with default lenient settings.
    /// </summary>
    public static IMessageValidator CreateForInitiator(ValidationConfig? config = null)
    {
        return Create(config, isAcceptor: false);
    }

    /// <summary>
    /// Creates a validator for acceptor (server) sessions with default strict settings.
    /// </summary>
    public static IMessageValidator CreateForAcceptor(ValidationConfig? config = null)
    {
        return Create(config, isAcceptor: true);
    }
}
