using PureFix.Dictionary.Compiler;
using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser.QuickFix;
using System.IO;

namespace PureFix.Test.Dictionary
{
    [TestFixture]
    public class ModularGeneratorTests
    {
        private const string TestDictPath = "../../../../test-dictionaries/FIX44-Core.xml";
        private const string OutputPath = "../../../../test-generated-modular";

        [SetUp]
        public void Setup()
        {
            // Clean output directory before each test
            if (Directory.Exists(OutputPath))
            {
                Directory.Delete(OutputPath, recursive: true);
            }
            Directory.CreateDirectory(OutputPath);
        }

        [Test]
        public void Can_Generate_Messages_From_FIX44_Core()
        {
            // Arrange
            var definitions = new FixDefinitions();
            var parser = new QuickFixXmlFileParser(definitions);
            parser.Parse(TestDictPath);

            var options = ModularGeneratorOptions.FromDictionaryPath(
                TestDictPath,
                OutputPath,
                definitions);

            // Act
            var generator = new ModularGenerator(definitions, options);
            generator.Process();

            // Assert
            var assemblyDir = Path.Join(OutputPath, options.AssemblyName);
            Assert.That(Directory.Exists(assemblyDir), Is.True, "Assembly directory not created");

            // Check .csproj was created
            var csprojPath = Path.Join(assemblyDir, $"{options.AssemblyName}.csproj");
            Assert.That(File.Exists(csprojPath), Is.True, ".csproj file not created");

            // Check Heartbeat.cs was created (simplest message)
            var heartbeatPath = Path.Join(options.BaseOptions.BackingTypeOutputPath, "Heartbeat.cs");
            Assert.That(File.Exists(heartbeatPath), Is.True, "Heartbeat.cs not created");

            // Check FixMessageFactory.cs was created
            var factoryPath = Path.Join(options.BaseOptions.BackingTypeOutputPath, "FixMessageFactory.cs");
            Assert.That(File.Exists(factoryPath), Is.True, "FixMessageFactory.cs not created");

            // Verify the generated Heartbeat.cs contains expected content
            var heartbeatContent = File.ReadAllText(heartbeatPath);
            Assert.That(heartbeatContent, Does.Contain("class Heartbeat"), "Heartbeat class not found");
            Assert.That(heartbeatContent, Does.Contain("IFixMessage"), "IFixMessage interface not found");
            Assert.That(heartbeatContent, Does.Contain("TestReqID"), "TestReqID field not found");
            Assert.That(heartbeatContent, Does.Contain("using PureFix.Types;"), "Core namespace not referenced");

            // Print the generated file for manual inspection
            TestContext.WriteLine("Generated Heartbeat.cs:");
            TestContext.WriteLine(heartbeatContent);
        }

        [Test]
        public void Generated_Assembly_Namespace_Is_Sanitized()
        {
            // Arrange
            var definitions = new FixDefinitions();
            var parser = new QuickFixXmlFileParser(definitions);
            parser.Parse(TestDictPath);

            var options = ModularGeneratorOptions.FromDictionaryPath(
                TestDictPath,
                OutputPath,
                definitions);

            // Assert
            // "FIX44-Core.xml" should become "PureFix.Types.FIX44Core"
            Assert.That(options.AssemblyName, Is.EqualTo("PureFix.Types.FIX44Core"));
            Assert.That(options.DictionaryName, Is.EqualTo("FIX44Core"));
            Assert.That(options.BaseOptions.BackingTypeNamespace, Is.EqualTo("PureFix.Types.FIX44Core"));
        }

        [Test]
        public void Can_Generate_All_FIX44_Core_Messages()
        {
            // Arrange
            var definitions = new FixDefinitions();
            var parser = new QuickFixXmlFileParser(definitions);
            parser.Parse(TestDictPath);

            var options = ModularGeneratorOptions.FromDictionaryPath(
                TestDictPath,
                OutputPath,
                definitions);

            // Act
            var generator = new ModularGenerator(definitions, options);
            generator.Process();

            // Assert - should have 10 messages
            var messageFiles = Directory.GetFiles(options.BaseOptions.BackingTypeOutputPath, "*.cs")
                .Where(f => !f.EndsWith("FixMessageFactory.cs"))
                .ToList();

            TestContext.WriteLine($"Generated {messageFiles.Count} message files:");
            foreach (var file in messageFiles)
            {
                TestContext.WriteLine($"  - {Path.GetFileName(file)}");
            }

            // FIX44-Core has: Heartbeat, Logon, Logout, TestRequest, ResendRequest,
            // Reject, SequenceReset, ExecutionReport, AllocationInstruction, TradeCaptureReport
            Assert.That(messageFiles.Count, Is.GreaterThanOrEqualTo(10), "Expected at least 10 message files");

            // Verify factory was created
            var factoryPath = Path.Join(options.BaseOptions.BackingTypeOutputPath, "FixMessageFactory.cs");
            Assert.That(File.Exists(factoryPath), Is.True, "Factory not created");
        }

        [TearDown]
        public void TearDown()
        {
            // Keep generated files for inspection
            TestContext.WriteLine($"Generated files in: {OutputPath}");
        }
    }
}
