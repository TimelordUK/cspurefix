using PureFix.Buffer.Ascii;
using PureFix.Test.Env;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Test.Ascii
{
    public class FixSimTest
    {
        private TestEntity _testEntity;      
        private List<AsciiView> _views;

        [OneTimeSetUp]
        public async Task OnceSetup()
        {
            _testEntity = new TestEntity();         
            var sw = new Stopwatch();
            sw.Start();
            _views = await _testEntity.Replay(Fix44PathHelper.FixSimExamplePath);
        }

        [SetUp]
        public void Setup()
        {
            _testEntity.Prepare();
        }

        [Test]
        public void Check_Replay_View_Count_Test()
        {
            Assert.That(_views, Has.Count.EqualTo(46));
        }
    }
}
