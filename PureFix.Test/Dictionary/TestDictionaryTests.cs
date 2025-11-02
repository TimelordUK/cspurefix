using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser.QuickFix;
using System.IO;

namespace PureFix.Test.Dictionary
{
    [TestFixture]
    public class TestDictionaryTests
    {
        private const string TestDictPath = "../../../../test-dictionaries/FIX44-Core.xml";

        [Test]
        public void FIX44_Core_Dictionary_Parses()
        {
            // Arrange
            var definitions = new FixDefinitions();
            var parser = new QuickFixXmlFileParser(definitions);

            // Act
            parser.Parse(TestDictPath);

            // Assert
            Assert.That(definitions, Is.Not.Null);
            Assert.That(definitions.Message, Is.Not.Empty);
        }

        [Test]
        public void FIX44_Core_Has_All_Expected_Messages()
        {
            // Arrange
            var definitions = new FixDefinitions();
            var parser = new QuickFixXmlFileParser(definitions);
            parser.Parse(TestDictPath);

            // Session messages
            Assert.That(definitions.Message.GetValueOrDefault("0"), Is.Not.Null, "Heartbeat missing");
            Assert.That(definitions.Message.GetValueOrDefault("1"), Is.Not.Null, "TestRequest missing");
            Assert.That(definitions.Message.GetValueOrDefault("2"), Is.Not.Null, "ResendRequest missing");
            Assert.That(definitions.Message.GetValueOrDefault("3"), Is.Not.Null, "Reject missing");
            Assert.That(definitions.Message.GetValueOrDefault("4"), Is.Not.Null, "SequenceReset missing");
            Assert.That(definitions.Message.GetValueOrDefault("5"), Is.Not.Null, "Logout missing");

            // Application messages
            Assert.That(definitions.Message.GetValueOrDefault("8"), Is.Not.Null, "ExecutionReport missing");
            Assert.That(definitions.Message.GetValueOrDefault("J"), Is.Not.Null, "AllocationInstruction missing");
            Assert.That(definitions.Message.GetValueOrDefault("AE"), Is.Not.Null, "TradeCaptureReport missing");
        }

        [Test]
        public void FIX44_Core_Has_StandardHeader_And_Trailer()
        {
            // Arrange
            var definitions = new FixDefinitions();
            var parser = new QuickFixXmlFileParser(definitions);
            parser.Parse(TestDictPath);

            // Act
            var heartbeat = definitions.Message.GetValueOrDefault("0");

            // Assert
            Assert.That(heartbeat, Is.Not.Null);
            Assert.That(heartbeat.Fields, Is.Not.Empty);

            // First field should be StandardHeader component
            var firstField = heartbeat.Fields[0];
            Assert.That(firstField.Name, Is.EqualTo("StandardHeader"));

            // Last field should be StandardTrailer component
            var lastField = heartbeat.Fields[heartbeat.Fields.Count - 1];
            Assert.That(lastField.Name, Is.EqualTo("StandardTrailer"));
        }

        [Test]
        public void FIX44_Core_Has_Required_Fields()
        {
            // Arrange
            var definitions = new FixDefinitions();
            var parser = new QuickFixXmlFileParser(definitions);
            parser.Parse(TestDictPath);

            // Assert key fields exist
            Assert.That(definitions.TagToSimple.ContainsKey(8), Is.True, "BeginString (tag 8) missing");
            Assert.That(definitions.TagToSimple.ContainsKey(9), Is.True, "BodyLength (tag 9) missing");
            Assert.That(definitions.TagToSimple.ContainsKey(35), Is.True, "MsgType (tag 35) missing");
            Assert.That(definitions.TagToSimple.ContainsKey(49), Is.True, "SenderCompID (tag 49) missing");
            Assert.That(definitions.TagToSimple.ContainsKey(56), Is.True, "TargetCompID (tag 56) missing");
            Assert.That(definitions.TagToSimple.ContainsKey(10), Is.True, "CheckSum (tag 10) missing");
        }

        [Test]
        public void FIX44_Core_ExecutionReport_Has_Fields()
        {
            // Arrange
            var definitions = new FixDefinitions();
            var parser = new QuickFixXmlFileParser(definitions);
            parser.Parse(TestDictPath);

            // Act
            var execReport = definitions.Message.GetValueOrDefault("8");

            // Assert
            Assert.That(execReport, Is.Not.Null);
            Assert.That(execReport.Name, Is.EqualTo("ExecutionReport"));
            Assert.That(execReport.Fields.Count, Is.GreaterThan(3),
                "ExecutionReport should have header + body + trailer");
        }
    }
}
