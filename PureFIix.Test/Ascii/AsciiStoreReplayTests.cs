using PureFIix.Test.Env;
using PureFix.Buffer.Ascii;
using PureFix.Transport;
using PureFix.Transport.Session;
using PureFix.Transport.Store;
using PureFix.Types.FIX50SP2.QuickFix;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFIix.Test.Ascii
{
    internal class AsciiStoreReplayTests
    {
        private TestEntity _testEntity;
        private List<AsciiView> _views;
        

        [OneTimeSetUp]
        public async Task OnceSetup()
        {
            _testEntity = new TestEntity();
            _views = await _testEntity.Replay(Fix44PathHelper.ReplayTestClientPath);   
        }

        [SetUp]
        public void Setup()
        {
            _testEntity.Prepare();
        }

        [Test]
        public void Check_Replay_View_Count_Test()
        {
            Assert.That(_views, Has.Count.EqualTo(15));
        }

        [Test]
        public async Task Check_Messages_Loaded_Store_Test()
        {
            var config = _testEntity.GetTestInitiatorConfig();            
            Assert.That(config, Is.Not.Null);
            var store = await _testEntity.MakeMsgStore(_views);
            var state = await store.GetState();
            Assert.That(state.Length, Is.EqualTo(9));
            Assert.That(state.LastSeq, Is.EqualTo(10));

        }
    }
}
