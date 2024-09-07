using PureFIix.Test.Env;
using PureFix.Types.tag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Buffer.Ascii;

namespace PureFIix.Test.Ascii
{
    public class AsciiTagPosTest
    {
        private TestEntity _testEntity;
        private List<AsciiView> _views;
        public string ReplayPath = Path.Join(Directory.GetCurrentDirectory(), "Data", "examples", "FIX.4.4", "quickfix", "logon","fix.txt");

        private TagPos[] _testTags =
        [
            new(0, 120, 0, 1), // 3
            new(1, 50, 1, 1), // 0
            new(2, 50, 2, 1), // 1
            new(3, 120, 3, 1), // 4
            new(4, 100, 4, 1) // 2
        ];

        private TagPos[] _expected =
        [
            new(1, 50, 1, 1), // 0
            new(2, 50, 2, 1), // 1
            new(4, 100, 4, 1), // 2
            new(0, 120, 0, 1), // 3
            new(3, 120, 3, 1) // 4
        ];

        private readonly TagPos[] _unsortedLogon =
        [
            new(0, 8, 2, 6),
            new(1, 9, 11, 7),
            new(2, 35, 22, 1),
            new(3, 49, 27, 9),
            new(4, 56, 40, 9),
            new(5, 34, 53, 1),
            new(6, 57, 58, 5),
            new(7, 52, 67, 21),
            new(8, 98, 92, 1),
            new(9, 108, 98, 5),
            new(10, 95, 107, 2),
            new(11, 96, 113, 20),
            new(12, 141, 138, 1),
            new(13, 789, 144, 4),
            new(14, 383, 153, 2),
            new(15, 384, 160, 1),
            new(16, 372, 166, 5),
            new(17, 385, 176, 1),
            new(18, 464, 182, 1),
            new(19, 553, 188, 3),
            new(20, 554, 196, 11)
        ];

        [OneTimeSetUp]
        public async Task OnceSetup()
        {
            _testEntity = new TestEntity();
            _testEntity.Prepare();
            _views = await _testEntity.Replay(ReplayPath);
        }

        [SetUp]
        public void Setup()
        {
            _testEntity.Prepare();
        }

        [Test]
        public void Logon_Tags_Parsed_Fully_Test()
        {
            Assert.That(_views, Is.Not.Null);
            Assert.That(_views.Count, Is.EqualTo(1));
            var t0 = _views[0].Structure?.Tags;
            Assert.That(t0, Is.Not.Null);
            var tp = t0[..(_views[0].Segment?.EndPosition ?? 0)];
            Assert.That(tp, Is.EqualTo(_unsortedLogon));
        }
    }
}
