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
        public string ReplayPath = Path.Join(Directory.GetCurrentDirectory(), "Data", "examples", "FIX.4.4", "fix.txt");
        public string JsonPath = Path.Join(Directory.GetCurrentDirectory(), "Data", "examples", "FIX.4.4", "fix.json");
        private Dictionary<string, int> _expectedCounts;
        private List<AsciiView> _views;

        [OneTimeSetUp]
        public async Task OnceSetup()
        {
            _testEntity = new TestEntity();
            _testEntity.Prepare();
            _expectedCounts = await _testEntity.GetJsonDict(JsonPath);
            _views = await _testEntity.Replay(ReplayPath);
        }

        [SetUp]
        public void Setup()
        {
            _testEntity.Prepare();
        }

        [Test]
        public void Check_Replay_View_Count_Test()
        {
            Assert.That(_views.Count, Is.EqualTo(50));
        }
    }
}
