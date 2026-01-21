using PureFix.Buffer.Validation;
using PureFix.Types.Validation;

namespace PureFix.Test.ModularTypes.Validation;

[TestFixture]
public class ValidationConfigTests
{
    [Test]
    public void ForInitiator_Returns_Lenient_Config()
    {
        var config = ValidationConfig.ForInitiator();

        Assert.Multiple(() =>
        {
            Assert.That(config.Mode, Is.EqualTo(ValidationMode.Lenient));
            Assert.That(config.AllowUnknownFields, Is.True);
            Assert.That(config.CheckChecksum, Is.True);
            Assert.That(config.CheckBodyLength, Is.True);
        });
    }

    [Test]
    public void ForAcceptor_Returns_Strict_Config()
    {
        var config = ValidationConfig.ForAcceptor();

        Assert.Multiple(() =>
        {
            Assert.That(config.Mode, Is.EqualTo(ValidationMode.Strict));
            Assert.That(config.AllowUnknownFields, Is.False);
            Assert.That(config.CheckChecksum, Is.True);
            Assert.That(config.CheckBodyLength, Is.True);
        });
    }

    [Test]
    public void NoValidation_Disables_All_Checks()
    {
        var config = ValidationConfig.NoValidation();

        Assert.Multiple(() =>
        {
            Assert.That(config.Mode, Is.EqualTo(ValidationMode.None));
            Assert.That(config.CheckChecksum, Is.False);
            Assert.That(config.CheckBodyLength, Is.False);
            Assert.That(config.CheckRequiredFields, Is.False);
            Assert.That(config.CheckGroupCounts, Is.False);
            Assert.That(config.AllowUnknownFields, Is.True);
            Assert.That(config.AllowEnumOutOfRange, Is.True);
        });
    }

    [Test]
    public void Default_Config_Has_Sensible_Defaults()
    {
        var config = new ValidationConfig();

        Assert.Multiple(() =>
        {
            Assert.That(config.Mode, Is.EqualTo(ValidationMode.Lenient));
            Assert.That(config.CheckChecksum, Is.True);
            Assert.That(config.CheckBodyLength, Is.True);
            Assert.That(config.AllowUnknownFields, Is.True);
            Assert.That(config.AllowEnumOutOfRange, Is.False);
            Assert.That(config.CoerceFloatToInt, Is.True);
            Assert.That(config.CheckRequiredFields, Is.True);
            Assert.That(config.CheckGroupCounts, Is.True);
        });
    }
}

[TestFixture]
public class MessageValidatorFactoryTests
{
    [Test]
    public void Create_With_None_Mode_Returns_Empty_Validator()
    {
        var config = ValidationConfig.NoValidation();
        var validator = MessageValidatorFactory.Create(config);

        Assert.That(validator, Is.SameAs(CompositeValidator.Empty));
    }

    [Test]
    public void Create_With_Checksum_Enabled_Returns_ProtocolValidator()
    {
        var config = new ValidationConfig
        {
            Mode = ValidationMode.Strict,
            CheckChecksum = true,
            CheckBodyLength = false
        };

        var validator = MessageValidatorFactory.Create(config);

        Assert.That(validator, Is.InstanceOf<ProtocolValidator>());
    }

    [Test]
    public void Create_With_Null_Config_Uses_Role_Based_Defaults()
    {
        var initiatorValidator = MessageValidatorFactory.Create(null, isAcceptor: false);
        var acceptorValidator = MessageValidatorFactory.Create(null, isAcceptor: true);

        // Both should return validators (not empty)
        Assert.Multiple(() =>
        {
            Assert.That(initiatorValidator, Is.Not.SameAs(CompositeValidator.Empty));
            Assert.That(acceptorValidator, Is.Not.SameAs(CompositeValidator.Empty));
        });
    }

    [Test]
    public void CreateForInitiator_Uses_Lenient_Defaults()
    {
        var validator = MessageValidatorFactory.CreateForInitiator();

        // Should create a validator (not null or empty when checks are enabled)
        Assert.That(validator, Is.Not.Null);
    }

    [Test]
    public void CreateForAcceptor_Uses_Strict_Defaults()
    {
        var validator = MessageValidatorFactory.CreateForAcceptor();

        // Should create a validator (not null or empty when checks are enabled)
        Assert.That(validator, Is.Not.Null);
    }

    [Test]
    public void Create_With_All_Checks_Disabled_Returns_Empty()
    {
        var config = new ValidationConfig
        {
            Mode = ValidationMode.Strict,
            CheckChecksum = false,
            CheckBodyLength = false
            // Future validators like StructureValidator would need to be disabled too
        };

        var validator = MessageValidatorFactory.Create(config);

        // With only protocol checks available and both disabled, should be empty
        Assert.That(validator, Is.SameAs(CompositeValidator.Empty));
    }
}
