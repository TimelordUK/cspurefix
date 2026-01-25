using PureFix.Buffer.Ascii;
using PureFix.Test.ModularTypes.Helpers;
using PureFix.Transport.Session;
using PureFix.Transport.Store;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Test.ModularTypes
{
    internal class MemoryStoreTest
    {
        private TestEntity _testEntity;
        private List<AsciiView> _views;
        private IFixConfig _config;
        private string? SenderCompID => _config?.Description?.SenderCompID;

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

        private async Task<IFixSessionStore> GetStore()
        {
            var store = await _testEntity.MakeMsgStore(_views, SenderCompID);
            return store;
        }

        [Test]
        public async Task Check_Messages_Loaded_Store_Test()
        {
            Assert.That(_config, Is.Not.Null);
            var store = await _testEntity.MakeMsgStore(_views, SenderCompID);
            var records = await store.GetRange(1, 1000);
            Assert.That(records.Count, Is.EqualTo(9));
            // Check the last record has seqnum 10
            Assert.That(records[^1].SeqNum, Is.EqualTo(10));
        }

        [Test]
        public async Task Fetch_Sequence_Number_From_Store_Test()
        {
            var store = await GetStore();
            Assert.That(store, Is.Not.Null);
            var res1 = await store.Get(1);
            Assert.That(res1, Is.Null); // Seq 1 not in store
            for (var seq = 2; seq <= 10; ++seq)
            {
                var get = await store.Get(seq);
                Assert.That(get, Is.Not.Null);
            }
        }

        [Test]
        public async Task Fetch_From_SeqNum_To_Inferred_End_Test()
        {
            var store = await GetStore();
            Assert.That(store, Is.Not.Null);
            var range1 = await store.GetRange(5, 1000);
            Assert.Multiple(() =>
            {
                Assert.That(range1, Has.Count.EqualTo(6));
                Assert.That(range1[0].SeqNum, Is.EqualTo(5));
                Assert.That(range1[^1].SeqNum, Is.EqualTo(10));
            });
        }

        [Test]
        public async Task Fetch_From_SeqNum_To_End_Past_Last_Test()
        {
            var store = await GetStore();
            Assert.That(store, Is.Not.Null);
            var range1 = await store.GetRange(5, int.MaxValue);
            Assert.Multiple(() =>
            {
                Assert.That(range1, Has.Count.EqualTo(6));
                Assert.That(range1[0].SeqNum, Is.EqualTo(5));
                Assert.That(range1[^1].SeqNum, Is.EqualTo(10));
            });
        }

        [Test]
        public async Task Fetch_From_SeqNum_To_Equals_Start_Test()
        {
            var store = await GetStore();
            Assert.That(store, Is.Not.Null);
            var range1 = await store.GetRange(5, 5);
            Assert.That(range1, Has.Count.EqualTo(1));
            Assert.That(range1[0].SeqNum, Is.EqualTo(5));
        }

        [Test]
        public async Task Fetch_From_SeqNum_Not_In_Store_Test()
        {
            var store = await GetStore();
            Assert.That(store, Is.Not.Null);
            var range1 = await store.GetRange(1, 1000);
            Assert.That(range1, Has.Count.EqualTo(9));
            Assert.That(range1[^1].SeqNum, Is.EqualTo(10));
        }
    }
}
