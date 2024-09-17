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
    public class Fix44TypeCompiler
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
        public void MessageGenerator()
        {
            var generator = new MessageGenerator(null, _testEntity.Definitions,Options.FromVersion(_testEntity.Definitions));
            generator.Process();
        }
    }
}
