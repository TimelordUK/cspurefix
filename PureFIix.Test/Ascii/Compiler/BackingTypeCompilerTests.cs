using PureFIix.Test.Env;
using PureFix.Buffer.Ascii;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Dictionary.Compiler;

namespace PureFIix.Test.Ascii.Compiler
{
    public class BackingTypeCompilerTests
    {
        private TestEntity _testEntity;

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
        public void GenerateTypes()
        {
            var compiler = new MsgCompiler(_testEntity.Definitions);
            compiler.Generate();
        }

        [Test]
        public void GenerateParseFormatTypes()
        {
            var compiler = new ViewParserCompiler(_testEntity.Definitions);
            compiler.Generate();
        }

        [Test]
        public void GenerateFormatFormatTypes()
        {
            var compiler = new TypeFormatCompiler(_testEntity.Definitions);
            compiler.Generate(new List<string>{"0","D"});
        }
    }
}
