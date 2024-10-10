
using PureFix.Buffer.Ascii;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Dictionary.Compiler;
using PureFIix.Test.Env;

namespace PureFix.Test.Ascii.Compiler
{
    public class Fix42TypeCompiler
    {
        private TestEntity _testEntity;

        [OneTimeSetUp]
        public void OnceSetup()
        {
            _testEntity = new TestEntity("FIX42.xml");
        }

        [SetUp]
        public void Setup()
        {
            _testEntity.Prepare();
        }

        [Test]
        public void MessageGenerator()
        {
            var generator = new MessageGenerator(null, _testEntity.Definitions, Options.FromVersion(_testEntity.Definitions));
            generator.Process();
        }
    }
}
