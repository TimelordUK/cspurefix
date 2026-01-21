using PureFix.Types.Validation;

namespace PureFix.Test.ModularTypes.Validation;

[TestFixture]
public class ValidationResultTests
{
    [Test]
    public void Empty_Result_Has_No_Warnings()
    {
        var result = new ValidationResult();

        Assert.Multiple(() =>
        {
            Assert.That(result.HasWarnings, Is.False);
            Assert.That(result.WarningCount, Is.EqualTo(0));
            Assert.That(result.Warnings, Is.Empty);
        });
    }

    [Test]
    public void AddWarning_Increases_Count()
    {
        var result = new ValidationResult();
        result.AddWarning(ValidationWarning.ChecksumMismatch(100, 200));

        Assert.Multiple(() =>
        {
            Assert.That(result.HasWarnings, Is.True);
            Assert.That(result.WarningCount, Is.EqualTo(1));
        });
    }

    [Test]
    public void GetWarnings_Filters_By_Type()
    {
        var result = new ValidationResult();
        result.AddWarning(ValidationWarning.ChecksumMismatch(100, 200));
        result.AddWarning(ValidationWarning.RequiredFieldMissing(35));
        result.AddWarning(ValidationWarning.ChecksumMismatch(101, 201));

        var checksumWarnings = result.GetWarnings(ValidationWarningType.ChecksumMismatch).ToList();
        var missingWarnings = result.GetWarnings(ValidationWarningType.RequiredFieldMissing).ToList();

        Assert.Multiple(() =>
        {
            Assert.That(checksumWarnings, Has.Count.EqualTo(2));
            Assert.That(missingWarnings, Has.Count.EqualTo(1));
        });
    }

    [Test]
    public void ShouldReject_None_Mode_Never_Rejects()
    {
        var result = new ValidationResult();
        result.AddWarning(ValidationWarning.ChecksumMismatch(100, 200));
        result.AddWarning(ValidationWarning.RequiredFieldMissing(35));

        Assert.That(result.ShouldReject(ValidationMode.None), Is.False);
    }

    [Test]
    public void ShouldReject_Lenient_Mode_Only_Rejects_Protocol_Errors()
    {
        // Lenient mode should accept non-protocol warnings
        var nonProtocolResult = new ValidationResult();
        nonProtocolResult.AddWarning(ValidationWarning.RequiredFieldMissing(35));
        nonProtocolResult.AddWarning(ValidationWarning.UnknownField(99999, "foo"));

        Assert.That(nonProtocolResult.ShouldReject(ValidationMode.Lenient), Is.False,
            "Lenient mode should not reject for non-protocol warnings");

        // Lenient mode should reject checksum errors
        var checksumResult = new ValidationResult();
        checksumResult.AddWarning(ValidationWarning.ChecksumMismatch(100, 200));

        Assert.That(checksumResult.ShouldReject(ValidationMode.Lenient), Is.True,
            "Lenient mode should reject for checksum mismatch");

        // Lenient mode should reject body length errors
        var bodyLengthResult = new ValidationResult();
        bodyLengthResult.AddWarning(ValidationWarning.BodyLengthMismatch(100, 200));

        Assert.That(bodyLengthResult.ShouldReject(ValidationMode.Lenient), Is.True,
            "Lenient mode should reject for body length mismatch");
    }

    [Test]
    public void ShouldReject_Strict_Mode_Rejects_Any_Warning()
    {
        var result = new ValidationResult();
        result.AddWarning(ValidationWarning.RequiredFieldMissing(35));

        Assert.That(result.ShouldReject(ValidationMode.Strict), Is.True,
            "Strict mode should reject for any warning");
    }

    [Test]
    public void ShouldReject_No_Warnings_Never_Rejects()
    {
        var result = new ValidationResult();

        Assert.Multiple(() =>
        {
            Assert.That(result.ShouldReject(ValidationMode.None), Is.False);
            Assert.That(result.ShouldReject(ValidationMode.Lenient), Is.False);
            Assert.That(result.ShouldReject(ValidationMode.Strict), Is.False);
        });
    }

    [Test]
    public void Clear_Removes_All_Warnings()
    {
        var result = new ValidationResult();
        result.AddWarning(ValidationWarning.ChecksumMismatch(100, 200));
        result.AddWarning(ValidationWarning.RequiredFieldMissing(35));

        result.Clear();

        Assert.Multiple(() =>
        {
            Assert.That(result.HasWarnings, Is.False);
            Assert.That(result.WarningCount, Is.EqualTo(0));
        });
    }

    [Test]
    public void UnknownFields_Property_Returns_Both_Types()
    {
        var result = new ValidationResult();
        result.AddWarning(ValidationWarning.UnknownField(99999, "foo"));
        result.AddWarning(ValidationWarning.UnknownField(99998, "bar", "Instrument"));
        result.AddWarning(ValidationWarning.RequiredFieldMissing(35));

        var unknowns = result.UnknownFields.ToList();

        Assert.That(unknowns, Has.Count.EqualTo(2));
    }
}
