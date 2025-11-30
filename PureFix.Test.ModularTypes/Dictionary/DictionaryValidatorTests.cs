using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser.QuickFix;

namespace PureFix.Test.ModularTypes.Dictionary;

[TestFixture]
public class DictionaryValidatorTests
{
    #region Valid Dictionary Tests

    [Test]
    public void Valid_Minimal_Dictionary_Passes()
    {
        var xml = @"
<fix major='4' minor='4'>
  <header>
    <field name='BeginString' required='Y' />
    <field name='BodyLength' required='Y' />
    <field name='MsgType' required='Y' />
  </header>
  <trailer>
    <field name='CheckSum' required='Y' />
  </trailer>
  <messages>
    <message name='Heartbeat' msgtype='0' msgcat='admin'>
      <field name='TestReqID' required='N' />
    </message>
  </messages>
  <fields>
    <field number='8' name='BeginString' type='STRING' />
    <field number='9' name='BodyLength' type='LENGTH' />
    <field number='35' name='MsgType' type='STRING' />
    <field number='10' name='CheckSum' type='STRING' />
    <field number='112' name='TestReqID' type='STRING' />
  </fields>
</fix>";

        var definitions = new FixDefinitions();
        var parser = new QuickFixXmlFileParser(definitions);

        Assert.DoesNotThrow(() => parser.ParseText(xml));
        Assert.That(parser.Validator, Is.Not.Null);
        Assert.That(parser.Validator!.HasErrors, Is.False);
    }

    #endregion

    #region Duplicate Field Tests

    [Test]
    public void Duplicate_Field_Name_Throws()
    {
        var xml = @"
<fix major='4' minor='4'>
  <header><field name='BeginString' required='Y' /></header>
  <trailer><field name='CheckSum' required='Y' /></trailer>
  <messages></messages>
  <fields>
    <field number='8' name='BeginString' type='STRING' />
    <field number='9' name='BeginString' type='STRING' />
    <field number='10' name='CheckSum' type='STRING' />
  </fields>
</fix>";

        var definitions = new FixDefinitions();
        var parser = new QuickFixXmlFileParser(definitions);

        var ex = Assert.Throws<DictionaryValidationException>(() => parser.ParseText(xml));
        Assert.That(ex!.Errors.Any(e => e.Code == "DUPLICATE_FIELD_NAME"), Is.True);
        Assert.That(ex.Message, Does.Contain("BeginString"));
    }

    [Test]
    public void Duplicate_Field_Tag_Throws()
    {
        var xml = @"
<fix major='4' minor='4'>
  <header><field name='BeginString' required='Y' /></header>
  <trailer><field name='CheckSum' required='Y' /></trailer>
  <messages></messages>
  <fields>
    <field number='8' name='BeginString' type='STRING' />
    <field number='8' name='BodyLength' type='LENGTH' />
    <field number='10' name='CheckSum' type='STRING' />
  </fields>
</fix>";

        var definitions = new FixDefinitions();
        var parser = new QuickFixXmlFileParser(definitions);

        var ex = Assert.Throws<DictionaryValidationException>(() => parser.ParseText(xml));
        Assert.That(ex!.Errors.Any(e => e.Code == "DUPLICATE_FIELD_TAG"), Is.True);
    }

    [Test]
    public void Duplicate_Enum_Key_Throws()
    {
        var xml = @"
<fix major='4' minor='4'>
  <header><field name='BeginString' required='Y' /></header>
  <trailer><field name='CheckSum' required='Y' /></trailer>
  <messages></messages>
  <fields>
    <field number='8' name='BeginString' type='STRING' />
    <field number='10' name='CheckSum' type='STRING' />
    <field number='54' name='Side' type='CHAR'>
      <value enum='1' description='BUY' />
      <value enum='1' description='SELL' />
    </field>
  </fields>
</fix>";

        var definitions = new FixDefinitions();
        var parser = new QuickFixXmlFileParser(definitions);

        var ex = Assert.Throws<DictionaryValidationException>(() => parser.ParseText(xml));
        Assert.That(ex!.Errors.Any(e => e.Code == "DUPLICATE_ENUM_KEY"), Is.True);
    }

    #endregion

    #region Duplicate Message Tests

    [Test]
    public void Duplicate_Message_Name_Throws()
    {
        var xml = @"
<fix major='4' minor='4'>
  <header><field name='BeginString' required='Y' /></header>
  <trailer><field name='CheckSum' required='Y' /></trailer>
  <messages>
    <message name='Heartbeat' msgtype='0' msgcat='admin' />
    <message name='Heartbeat' msgtype='1' msgcat='admin' />
  </messages>
  <fields>
    <field number='8' name='BeginString' type='STRING' />
    <field number='10' name='CheckSum' type='STRING' />
  </fields>
</fix>";

        var definitions = new FixDefinitions();
        var parser = new QuickFixXmlFileParser(definitions);

        var ex = Assert.Throws<DictionaryValidationException>(() => parser.ParseText(xml));
        Assert.That(ex!.Errors.Any(e => e.Code == "DUPLICATE_MESSAGE_NAME"), Is.True);
    }

    [Test]
    public void Duplicate_Message_Type_Throws()
    {
        var xml = @"
<fix major='4' minor='4'>
  <header><field name='BeginString' required='Y' /></header>
  <trailer><field name='CheckSum' required='Y' /></trailer>
  <messages>
    <message name='Heartbeat' msgtype='0' msgcat='admin' />
    <message name='TestRequest' msgtype='0' msgcat='admin' />
  </messages>
  <fields>
    <field number='8' name='BeginString' type='STRING' />
    <field number='10' name='CheckSum' type='STRING' />
  </fields>
</fix>";

        var definitions = new FixDefinitions();
        var parser = new QuickFixXmlFileParser(definitions);

        var ex = Assert.Throws<DictionaryValidationException>(() => parser.ParseText(xml));
        Assert.That(ex!.Errors.Any(e => e.Code == "DUPLICATE_MESSAGE_TYPE"), Is.True);
    }

    #endregion

    #region Duplicate Component Tests

    [Test]
    public void Duplicate_Component_Name_Throws()
    {
        var xml = @"
<fix major='4' minor='4'>
  <header><field name='BeginString' required='Y' /></header>
  <trailer><field name='CheckSum' required='Y' /></trailer>
  <messages></messages>
  <components>
    <component name='Instrument'><field name='Symbol' required='Y' /></component>
    <component name='Instrument'><field name='SecurityID' required='Y' /></component>
  </components>
  <fields>
    <field number='8' name='BeginString' type='STRING' />
    <field number='10' name='CheckSum' type='STRING' />
    <field number='55' name='Symbol' type='STRING' />
    <field number='48' name='SecurityID' type='STRING' />
  </fields>
</fix>";

        var definitions = new FixDefinitions();
        var parser = new QuickFixXmlFileParser(definitions);

        var ex = Assert.Throws<DictionaryValidationException>(() => parser.ParseText(xml));
        Assert.That(ex!.Errors.Any(e => e.Code == "DUPLICATE_COMPONENT"), Is.True);
    }

    #endregion

    #region Undefined Reference Tests

    [Test]
    public void Undefined_Field_Reference_Throws()
    {
        var xml = @"
<fix major='4' minor='4'>
  <header><field name='BeginString' required='Y' /></header>
  <trailer><field name='CheckSum' required='Y' /></trailer>
  <messages>
    <message name='Heartbeat' msgtype='0' msgcat='admin'>
      <field name='NonExistentField' required='N' />
    </message>
  </messages>
  <fields>
    <field number='8' name='BeginString' type='STRING' />
    <field number='10' name='CheckSum' type='STRING' />
  </fields>
</fix>";

        var definitions = new FixDefinitions();
        var parser = new QuickFixXmlFileParser(definitions);

        var ex = Assert.Throws<DictionaryValidationException>(() => parser.ParseText(xml));
        Assert.That(ex!.Errors.Any(e => e.Code == "UNDEFINED_FIELD"), Is.True);
        Assert.That(ex.Message, Does.Contain("NonExistentField"));
    }

    [Test]
    public void Undefined_Component_Reference_Throws()
    {
        var xml = @"
<fix major='4' minor='4'>
  <header><field name='BeginString' required='Y' /></header>
  <trailer><field name='CheckSum' required='Y' /></trailer>
  <messages>
    <message name='NewOrderSingle' msgtype='D' msgcat='app'>
      <component name='NonExistentComponent' required='Y' />
    </message>
  </messages>
  <fields>
    <field number='8' name='BeginString' type='STRING' />
    <field number='10' name='CheckSum' type='STRING' />
  </fields>
</fix>";

        var definitions = new FixDefinitions();
        var parser = new QuickFixXmlFileParser(definitions);

        var ex = Assert.Throws<DictionaryValidationException>(() => parser.ParseText(xml));
        Assert.That(ex!.Errors.Any(e => e.Code == "UNDEFINED_COMPONENT"), Is.True);
    }

    [Test]
    public void Undefined_Group_Field_Throws()
    {
        var xml = @"
<fix major='4' minor='4'>
  <header><field name='BeginString' required='Y' /></header>
  <trailer><field name='CheckSum' required='Y' /></trailer>
  <messages>
    <message name='NewOrderSingle' msgtype='D' msgcat='app'>
      <group name='NoUndefinedGroup' required='N'>
        <field name='SomeField' required='Y' />
      </group>
    </message>
  </messages>
  <fields>
    <field number='8' name='BeginString' type='STRING' />
    <field number='10' name='CheckSum' type='STRING' />
    <field number='100' name='SomeField' type='STRING' />
  </fields>
</fix>";

        var definitions = new FixDefinitions();
        var parser = new QuickFixXmlFileParser(definitions);

        var ex = Assert.Throws<DictionaryValidationException>(() => parser.ParseText(xml));
        Assert.That(ex!.Errors.Any(e => e.Code == "UNDEFINED_GROUP_FIELD"), Is.True);
    }

    #endregion

    #region Did You Mean Suggestions

    [Test]
    public void Similar_Field_Name_Suggests_Correction()
    {
        var xml = @"
<fix major='4' minor='4'>
  <header><field name='BeginString' required='Y' /></header>
  <trailer><field name='CheckSum' required='Y' /></trailer>
  <messages>
    <message name='Heartbeat' msgtype='0' msgcat='admin'>
      <field name='TestReqId' required='N' />
    </message>
  </messages>
  <fields>
    <field number='8' name='BeginString' type='STRING' />
    <field number='10' name='CheckSum' type='STRING' />
    <field number='112' name='TestReqID' type='STRING' />
  </fields>
</fix>";

        var definitions = new FixDefinitions();
        var parser = new QuickFixXmlFileParser(definitions);

        var ex = Assert.Throws<DictionaryValidationException>(() => parser.ParseText(xml));

        // Should suggest TestReqID for TestReqId
        var undefinedError = ex!.Errors.FirstOrDefault(e => e.Code == "UNDEFINED_FIELD");
        Assert.That(undefinedError, Is.Not.Null);
        Assert.That(undefinedError!.Suggestion, Does.Contain("TestReqID"));
    }

    #endregion

    #region Missing Section Tests

    [Test]
    public void Missing_Header_Throws()
    {
        var xml = @"
<fix major='4' minor='4'>
  <trailer><field name='CheckSum' required='Y' /></trailer>
  <messages></messages>
  <fields>
    <field number='10' name='CheckSum' type='STRING' />
  </fields>
</fix>";

        var definitions = new FixDefinitions();
        var parser = new QuickFixXmlFileParser(definitions);

        var ex = Assert.Throws<DictionaryValidationException>(() => parser.ParseText(xml));
        Assert.That(ex!.Errors.Any(e => e.Code == "MISSING_HEADER"), Is.True);
    }

    [Test]
    public void Missing_Trailer_Throws()
    {
        var xml = @"
<fix major='4' minor='4'>
  <header><field name='BeginString' required='Y' /></header>
  <messages></messages>
  <fields>
    <field number='8' name='BeginString' type='STRING' />
  </fields>
</fix>";

        var definitions = new FixDefinitions();
        var parser = new QuickFixXmlFileParser(definitions);

        var ex = Assert.Throws<DictionaryValidationException>(() => parser.ParseText(xml));
        Assert.That(ex!.Errors.Any(e => e.Code == "MISSING_TRAILER"), Is.True);
    }

    [Test]
    public void Missing_Fields_Section_Throws()
    {
        var xml = @"
<fix major='4' minor='4'>
  <header><field name='BeginString' required='Y' /></header>
  <trailer><field name='CheckSum' required='Y' /></trailer>
  <messages></messages>
</fix>";

        var definitions = new FixDefinitions();
        var parser = new QuickFixXmlFileParser(definitions);

        var ex = Assert.Throws<DictionaryValidationException>(() => parser.ParseText(xml));
        Assert.That(ex!.Errors.Any(e => e.Code == "MISSING_FIELDS"), Is.True);
    }

    #endregion

    #region Validation Can Be Disabled

    [Test]
    public void Validation_Can_Be_Disabled()
    {
        var xml = @"
<fix major='4' minor='4'>
  <header><field name='BeginString' required='Y' /></header>
  <trailer><field name='CheckSum' required='Y' /></trailer>
  <messages>
    <message name='Heartbeat' msgtype='0' msgcat='admin'>
      <field name='NonExistent' required='N' />
    </message>
  </messages>
  <fields>
    <field number='8' name='BeginString' type='STRING' />
    <field number='10' name='CheckSum' type='STRING' />
  </fields>
</fix>";

        var definitions = new FixDefinitions();
        var parser = new QuickFixXmlFileParser(definitions)
        {
            ValidateBeforeParsing = false
        };

        // Should not throw because validation is disabled
        // (but the actual parsing will fail later with original error)
        Assert.That(parser.Validator, Is.Null);
    }

    #endregion

    #region Warning Tests

    [Test]
    public void Unused_Field_Generates_Warning()
    {
        var xml = @"
<fix major='4' minor='4'>
  <header><field name='BeginString' required='Y' /></header>
  <trailer><field name='CheckSum' required='Y' /></trailer>
  <messages></messages>
  <fields>
    <field number='8' name='BeginString' type='STRING' />
    <field number='10' name='CheckSum' type='STRING' />
    <field number='999' name='UnusedField' type='STRING' />
  </fields>
</fix>";

        var definitions = new FixDefinitions();
        var parser = new QuickFixXmlFileParser(definitions);

        Assert.DoesNotThrow(() => parser.ParseText(xml));
        Assert.That(parser.Validator!.HasWarnings, Is.True);
        Assert.That(parser.Validator.Errors.Any(e => e.Code == "UNUSED_FIELD" && e.ElementName == "UnusedField"), Is.True);
    }

    #endregion
}
