using PureFIix.Test.Env;
using PureFix.Dictionary.Compiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Test.Ascii.Compiler
{
    public class FixT11TypeCompiler
    {
        private TestEntity _testEntity;

        [OneTimeSetUp]
        public void OnceSetup()
        {
            _testEntity = new TestEntity("FIXT11.xml");
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
