using PureFIix.Test.Env;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Buffer.Ascii;
using PureFix.Types;

namespace PureFix.Test.Ascii
{
    public class AsciiTagPosTest
    {
        private TestEntity _testEntity;
        private List<AsciiView> _views;
        
        private readonly TagPos[] _testTags =
        [
            new(0, 120, 0, 1), // 3
            new(1, 50, 1, 1), // 0
            new(2, 50, 2, 1), // 1
            new(3, 120, 3, 1), // 4
            new(4, 100, 4, 1) // 2
        ];

        private readonly TagPos[] _expected =
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
            _views = await _testEntity.Replay(Fix44PathHelper.LogonReplayPath);
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

        List<TagPos> GetSorted()
        {
            var sorted = _testTags.ToList()[.._testTags.Length];
            sorted.Sort(TagPos.Compare);
            return sorted;
        }

        [Test]
        public void Expect_Tags_Sort_In_Tag_First_Order_Test()
        {
            var sorted = GetSorted();
            var tags = sorted.Select(t => t.Tag).ToArray();
            var expected = new[] { 50, 50, 100, 120, 120 };
            Assert.That(tags, Is.EqualTo(expected));
        }

        [Test]
        public void Expect_Tags_Sort_In_Tag_Then_Start_First_Order_Test()
        {
            var sorted = GetSorted();
            Assert.That(sorted[0].Tag, Is.EqualTo(sorted[1].Tag));
            Assert.That(sorted[0].Start, Is.LessThan(sorted[1].Start));
        }

        [Test]
        public void Expect_Start_To_Carry_With_Its_Tag_Test()
        {
            var sorted = GetSorted();
            Assert.That(sorted, Is.EqualTo(_expected));
        }

        [Test]
        public void Binary_Search_On_Sorted_Test()
        {
            var sorted = GetSorted();
            Assert.That(TagPos.BinarySearch(sorted, 100), Is.EqualTo(2));
        }

        [Test]
        public void Binary_Search_Non_Existing_Tag_Test()
        {
            var sorted = GetSorted();
            Assert.That(TagPos.BinarySearch(sorted, 1000), Is.LessThan(0));
        }

        [Test]
        public void Binary_Search_Duplicate_Tag_Test()
        {
            var sorted = GetSorted();
            Assert.That(TagPos.BinarySearch(sorted, 50), Is.LessThanOrEqualTo(1));
        }

        [Test]
        public void Check_Logon_Test()
        {
            var sorted = _unsortedLogon.ToList()[.._unsortedLogon.Length];
            sorted.Sort(TagPos.Compare);
            Assert.That(sorted[0].Tag, Is.EqualTo(8));
            Assert.That(sorted[^1].Tag, Is.EqualTo(789));
        }
    }
}
