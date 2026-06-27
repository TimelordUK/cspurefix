using System.Text;
using PureFix.Buffer.Ascii;
using PureFix.Test.ModularTypes.Helpers;
using PureFix.Types;
using PureFix.Types.Core;

namespace PureFix.Test.ModularTypes
{
    /// <summary>
    /// Port of jspurefix issue #143 / PR #144.
    ///
    /// The tag store grew with no upper bound, so a misaligned or oversized stream
    /// that never reaches a frame boundary could drive an unbounded TagPos[N]
    /// allocation and crash the process with a heap OOM. Growth is now capped and
    /// an over-long message is rejected with a thrown error instead.
    /// </summary>
    public class Issue143UnboundedTagsTest
    {
        // a complete, well-formed logon used to confirm normal messages still parse
        private const string Logon =
            "8=FIX4.4|9=0000208|35=A|49=sender-10|56=target-20|34=1|57=sub-a|52=20180610-10:39:01.621|98=2|108=62441|95=20|96=VgfoSqo56NqSVI1fLdlI|141=Y|789=4886|383=20|384=1|372=ipsum|385=R|464=N|553=sit|554=consectetur|10=49|";

        private TestEntity _testEntity = null!;

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
        public void Tag_Store_Throws_Once_Growth_Would_Exceed_Maximum()
        {
            var tags = new Tags(2, 4);
            for (var i = 0; i < 4; ++i)
            {
                tags.Store(0, 1, i);
            }

            var ex = Assert.Throws<InvalidOperationException>(() => tags.Store(0, 1, 4));
            Assert.That(ex!.Message, Does.Contain("exceeds maximum"));
        }

        [Test]
        public void Initial_Allocation_Is_Clamped_To_The_Maximum()
        {
            var tags = new Tags(1000, 4);
            Assert.That(tags.Capacity, Is.EqualTo(4));
        }

        [Test]
        public void Clone_Preserves_The_Maximum_So_It_Cannot_Grow_Unbounded_Either()
        {
            var tags = new Tags(2, 4);
            for (var i = 0; i < 3; ++i)
            {
                tags.Store(0, 1, i);
            }

            var cloned = tags.Clone();
            Assert.That(cloned.MaxLength, Is.EqualTo(4));
            cloned.Store(0, 1, 3);
            var ex = Assert.Throws<InvalidOperationException>(() => cloned.Store(0, 1, 4));
            Assert.That(ex!.Message, Does.Contain("exceeds maximum"));
        }

        [Test]
        public void Misaligned_Stream_Is_Rejected_Rather_Than_Growing_Tags_Unbounded()
        {
            var stream = new StringBuilder("8=FIX4.4|9=101|35=A|");
            for (var i = 0; i < 64; ++i)
            {
                stream.Append($"{100 + i}=x|");
            }

            var parser = new AsciiParser(_testEntity.Definitions, 16)
            {
                Delimiter = AsciiChars.Pipe,
                WriteDelimiter = AsciiChars.Pipe
            };
            var bytes = Encoding.UTF8.GetBytes(stream.ToString());

            var ex = Assert.Throws<InvalidOperationException>(
                () => parser.ParseFrom(bytes, bytes.Length, null));
            Assert.That(ex!.Message, Does.Contain("exceeds maximum"));
        }

        [Test]
        public void A_Normal_Message_Within_The_Maximum_Still_Parses()
        {
            var views = _testEntity.ParseText(Logon);
            Assert.That(views, Has.Count.EqualTo(1));
        }
    }
}
