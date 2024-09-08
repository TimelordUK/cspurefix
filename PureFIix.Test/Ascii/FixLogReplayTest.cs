using PureFIix.Test.Env;
using PureFix.Buffer.Ascii;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFIix.Test.Ascii
{
    public class FixLogReplayTest
    {
        private TestEntity _testEntity;
        private Dictionary<string, int> _expectedCounts;
        private List<AsciiView> _views;

        [OneTimeSetUp]
        public async Task OnceSetup()
        {
            _testEntity = new TestEntity();
            _expectedCounts = await TestEntity.GetJsonDict(Fix44PathHelper.JsonPath);
            _views = await _testEntity.Replay(Fix44PathHelper.ReplayPath);
        }

        [SetUp]
        public void Setup()
        {
            _testEntity.Prepare();
        }

        [Test]
        public void Check_Replay_View_Count_Test()
        {
            Assert.That(_views, Has.Count.EqualTo(50));
        }

        /*
         * using a generated fix file ensure it is parsed into expected composition of messages.
         */
        [Test]
        public void Check_Replay_Msg_Count_Test()
        {
            var defs = _testEntity.Definitions.Message;
            var fileCounts = new Dictionary<string,int>();
            _views.ForEach(v =>
            {
                if (!defs.TryGetValue(v.Structure?.Msg()?.Name ?? string.Empty, out var msg)) return;
                if (!fileCounts.TryGetValue(msg.MsgType, out var c))
                {
                    fileCounts[msg.MsgType] = c = 0;
                }
                fileCounts[msg.MsgType] = c + 1;
            });
            Assert.That(fileCounts, Is.EqualTo(_expectedCounts));
        }
    }
}
