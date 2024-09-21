using PureFIix.Test.Env;
using PureFix.Buffer.Ascii;
using PureFix.Transport.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFIix.Test.Ascii
{
    internal class AsciiStoreReplayTest
    {
        private TestEntity _testEntity;
        private List<AsciiView> _views;
        IFixConfig _client_config;
        IFixConfig _server_config;

        [OneTimeSetUp]
        public async Task OnceSetup()
        {
            _testEntity = new TestEntity();
            _views = await _testEntity.Replay(Fix44PathHelper.ReplayTestClientPath);
            _client_config = _testEntity.GetTestInitiatorConfig();
            _server_config = _testEntity.GetTestAcceptorConfig();
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
        public async Task Check_Server_Store_State_Test()
        {
            var store = await _testEntity.MakeMsgStore(_views, _server_config.Description.SenderCompID);
            var state = await store.GetState();
            Assert.That(state.Length, Is.EqualTo(9));
        }


        [Test]
        public async Task Check_Client_Store_State_Test()
        {
            var store = await _testEntity.MakeMsgStore(_views, _client_config.Description.SenderCompID);
            var state = await store.GetState();
            Assert.That(state.Length, Is.EqualTo(1));
        }
    }
}
