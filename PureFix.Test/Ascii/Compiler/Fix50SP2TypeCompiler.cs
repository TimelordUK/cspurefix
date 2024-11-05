using PureFix.Dictionary.Compiler;
using PureFix.Test.Env;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Test.Ascii.Compiler
{
    public class Fix50SP2TypeCompiler
    {
        private TestEntity _testEntity;

        [OneTimeSetUp]
        public void OnceSetup()
        {
            _testEntity = new TestEntity("FIX50SP2.xml");
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
            //generator.Process();
        }
    }
}
