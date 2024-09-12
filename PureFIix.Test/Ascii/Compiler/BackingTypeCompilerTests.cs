using PureFIix.Test.Env;
using PureFix.Buffer.Ascii;
using PureFix.Types.FIX4._4.quickfix;
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
        public async Task Compiler_Heartbeat_Test()
        {
            var compiler = new MsgCompiler(_testEntity.Definitions);
            await compiler.CreateTypes(new List<string>(["0"]));
        }
    }
}
