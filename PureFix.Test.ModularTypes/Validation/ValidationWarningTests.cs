using PureFix.Types.Validation;

namespace PureFix.Test.ModularTypes.Validation;

[TestFixture]
public class ValidationWarningTests
{
    [Test]
    public void UnknownField_Without_Context_Uses_UnknownField_Type()
    {
        var warning = ValidationWarning.UnknownField(99999, "foo");

        Assert.Multiple(() =>
        {
            Assert.That(warning.Type, Is.EqualTo(ValidationWarningType.UnknownField));
            Assert.That(warning.Tag, Is.EqualTo(99999));
            Assert.That(warning.Value, Is.EqualTo("foo"));
            Assert.That(warning.Context, Is.Null);
            Assert.That(warning.Message, Does.Contain("99999"));
        });
    }

    [Test]
    public void UnknownField_With_Context_Uses_UnknownFieldInComponent_Type()
    {
        var warning = ValidationWarning.UnknownField(99999, "foo", "Instrument");

        Assert.Multiple(() =>
        {
            Assert.That(warning.Type, Is.EqualTo(ValidationWarningType.UnknownFieldInComponent));
            Assert.That(warning.Tag, Is.EqualTo(99999));
            Assert.That(warning.Value, Is.EqualTo("foo"));
            Assert.That(warning.Context, Is.EqualTo("Instrument"));
            Assert.That(warning.Message, Does.Contain("Instrument"));
        });
    }

    [Test]
    public void EnumOutOfRange_Contains_Value_In_Message()
    {
        var warning = ValidationWarning.EnumOutOfRange(39, "Z");

        Assert.Multiple(() =>
        {
            Assert.That(warning.Type, Is.EqualTo(ValidationWarningType.EnumOutOfRange));
            Assert.That(warning.Tag, Is.EqualTo(39));
            Assert.That(warning.Value, Is.EqualTo("Z"));
            Assert.That(warning.Message, Does.Contain("Z"));
            Assert.That(warning.Message, Does.Contain("39"));
        });
    }

    [Test]
    public void RequiredFieldMissing_Contains_Tag_In_Message()
    {
        var warning = ValidationWarning.RequiredFieldMissing(35);

        Assert.Multiple(() =>
        {
            Assert.That(warning.Type, Is.EqualTo(ValidationWarningType.RequiredFieldMissing));
            Assert.That(warning.Tag, Is.EqualTo(35));
            Assert.That(warning.Value, Is.Null);
            Assert.That(warning.Message, Does.Contain("35"));
        });
    }

    [Test]
    public void RequiredFieldMissing_With_Context_Includes_Context()
    {
        var warning = ValidationWarning.RequiredFieldMissing(55, "Instrument");

        Assert.Multiple(() =>
        {
            Assert.That(warning.Context, Is.EqualTo("Instrument"));
            Assert.That(warning.Message, Does.Contain("Instrument"));
        });
    }

    [Test]
    public void ChecksumMismatch_Contains_Both_Values()
    {
        var warning = ValidationWarning.ChecksumMismatch(100, 200);

        Assert.Multiple(() =>
        {
            Assert.That(warning.Type, Is.EqualTo(ValidationWarningType.ChecksumMismatch));
            Assert.That(warning.Tag, Is.EqualTo(10));
            Assert.That(warning.Value, Is.EqualTo("100"));
            Assert.That(warning.Message, Does.Contain("100"));
            Assert.That(warning.Message, Does.Contain("200"));
        });
    }

    [Test]
    public void BodyLengthMismatch_Contains_Both_Values()
    {
        var warning = ValidationWarning.BodyLengthMismatch(100, 150);

        Assert.Multiple(() =>
        {
            Assert.That(warning.Type, Is.EqualTo(ValidationWarningType.BodyLengthMismatch));
            Assert.That(warning.Tag, Is.EqualTo(9));
            Assert.That(warning.Value, Is.EqualTo("100"));
            Assert.That(warning.Message, Does.Contain("100"));
            Assert.That(warning.Message, Does.Contain("150"));
        });
    }

    [Test]
    public void GroupCountMismatch_Contains_All_Info()
    {
        var warning = ValidationWarning.GroupCountMismatch(453, 3, 2, "NoPartyIDs");

        Assert.Multiple(() =>
        {
            Assert.That(warning.Type, Is.EqualTo(ValidationWarningType.GroupCountMismatch));
            Assert.That(warning.Tag, Is.EqualTo(453));
            Assert.That(warning.Context, Is.EqualTo("NoPartyIDs"));
            Assert.That(warning.Message, Does.Contain("453"));
            Assert.That(warning.Message, Does.Contain("3"));
            Assert.That(warning.Message, Does.Contain("2"));
        });
    }

    [Test]
    public void ToString_Formats_Correctly()
    {
        var warning = ValidationWarning.UnknownField(99999, "foo", "Instrument", position: 42);

        var str = warning.ToString();

        Assert.Multiple(() =>
        {
            Assert.That(str, Does.Contain("UnknownFieldInComponent"));
            Assert.That(str, Does.Contain("99999"));
            Assert.That(str, Does.Contain("Instrument"));
            Assert.That(str, Does.Contain("@42"));
        });
    }
}
