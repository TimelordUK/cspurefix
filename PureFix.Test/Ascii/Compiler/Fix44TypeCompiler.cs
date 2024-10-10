using PureFIix.Test.Env;
using PureFix.Buffer.Ascii;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Dictionary.Compiler;
using PureFix.Dictionary.Parser.QuickFix;
using PureFix.Dictionary.Definition;

namespace PureFix.Test.Ascii.Compiler
{
    public class Fix44TypeCompiler
    {
        private TestEntity _testEntity;
        private IFixDefinitions Definitions => _testEntity.Definitions;

        [OneTimeSetUp]
        public void OnceSetup()
        {
            _testEntity = new TestEntity();
        }

        [SetUp]
        public void Setup()
        {
            _testEntity.Prepare();
        }

        [Test]
        public void MessageGenerator()
        {
            var generator = new MessageGenerator(null, Definitions, Options.FromVersion(Definitions));
            generator.Process();
        }

        private IFixDefinitions GetTrimDefinitions(string[] types) {
            var builder = new QuickFixXmlFileBuilder(Definitions);
            var encoded = builder.Write(types);
            var defs = new FixDefinitions();
            var parser = new QuickFixXmlFileParser(defs);
            parser.ParseText(encoded);
            return defs;
        }

        [Test]
        public void Check_Builder()
        {
            string[] msgTypes = ["0", "1", "2", "3", "4", "5", "AE"];
            var newdDefinitions = GetTrimDefinitions(msgTypes);
            Assert.That(newdDefinitions, Is.Not.Null);
            foreach (var type in msgTypes)
            {
                var m = newdDefinitions.Message.GetValueOrDefault(type);
                Assert.That(m, Is.Not.Null);
            }
        }
   
        [Test]
        public void FileGenerator()
        {
            var generator = new MessageGenerator(null, Definitions, Options.FromVersion(Definitions));
            generator.Process();
        }
    }
}
