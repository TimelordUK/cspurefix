using PureFIix.Test.Env;
using PureFix.Buffer.Ascii;
using PureFix.Transport.Session;
using PureFix.Transport.Store;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Test.Ascii
{
    internal class MemoryStoreTest
    {
        private TestEntity _testEntity;
        private List<AsciiView> _views;
        private IFixConfig _config;
        private string SenderCompID => _config?.Description?.SenderCompID;

        [OneTimeSetUp]
        public async Task OnceSetup()
        {
            _testEntity = new TestEntity();
            _views = await _testEntity.Replay(Fix44PathHelper.ReplayTestClientPath);
            _config = _testEntity.GetTestAcceptorConfig();
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

        private async Task<IFixMsgStore> GetStore()
        {
            var store = await _testEntity.MakeMsgStore(_views, SenderCompID);
            return store;
        }

        [Test]
        public async Task Check_Messages_Loaded_Store_Test()
        {
            Assert.That(_config, Is.Not.Null);
            var store = await _testEntity.MakeMsgStore(_views, SenderCompID);
            var state = await store.GetState();
            Assert.That(state.Length, Is.EqualTo(9));
            Assert.That(state.LastSeq, Is.EqualTo(10));
        }

        [Test]
        public async Task Fetch_Sequence_Number_From_Store_Test()
        {
            var store = await GetStore();
            Assert.That(store, Is.Not.Null);
            var res1 = await store.Exists(1);
            Assert.That(res1, Is.False);
            for (var seq = 2; seq <= 10; ++seq)
            {
                var res = await store.Exists(seq);
                Assert.That(res, Is.True);
                var get = await store.Get(seq);
                Assert.That(get, Is.Not.Null);
            }
        }

        [Test]
        public async Task Fetch_From_SeqNum_To_Inferred_End_Test()
        {
            var store = await GetStore();
            Assert.That(store, Is.Not.Null);
            var range1 = await store.GetSeqNumRange(5);
            Assert.Multiple(() =>
            {
                Assert.That(range1, Has.Length.EqualTo(6));
                Assert.That(range1[0].SeqNum, Is.EqualTo(5));
                Assert.That(range1[^1].SeqNum, Is.EqualTo(10));
            });            
        }

        [Test]
        public async Task Fetch_From_SeqNum_To_End_Past_Last_Test()
        {
            var store = await GetStore();
            Assert.That(store, Is.Not.Null);
            var range1 = await store.GetSeqNumRange(5, int.MaxValue);
            Assert.Multiple(() =>
            {
                Assert.That(range1, Has.Length.EqualTo(6));
                Assert.That(range1[0].SeqNum, Is.EqualTo(5));
                Assert.That(range1[^1].SeqNum, Is.EqualTo(10));
            });
        }

        [Test]
        public async Task Fetch_From_SeqNum_To_Equals_Start_Test()
        {
            var store = await GetStore();
            Assert.That(store, Is.Not.Null);
            var range1 = await store.GetSeqNumRange(5, 5);
            Assert.That(range1, Has.Length.EqualTo(1));
            Assert.That(range1[0].SeqNum, Is.EqualTo(5));
        }

        [Test]
        public async Task Fetch_From_SeqNum_Not_In_Store_Test()
        {
            var store = await GetStore();
            Assert.That(store, Is.Not.Null);
            var range1 = await store.GetSeqNumRange(1);
            Assert.That(range1, Has.Length.EqualTo(9));
            Assert.That(range1[^1].SeqNum, Is.EqualTo(10));
        }
    }
}
