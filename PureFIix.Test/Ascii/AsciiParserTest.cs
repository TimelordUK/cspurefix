using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using PureFIix.Test.Env;
using PureFix.Buffer;
using PureFix.Buffer.Ascii;
using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser.QuickFix;
using PureFix.Transport;
using PureFix.Types.tag;

namespace PureFIix.Test.Ascii
{
    public class AsciiParserTest
    {
        public readonly string Logon = "8=FIX4.4|9=0000208|35=A|49=sender-10|56=target-20|34=1|57=sub-a|52=20180610-10:39:01.621|98=2|108=62441|95=20|96=VgfoSqo56NqSVI1fLdlI|141=Y|789=4886|383=20|384=1|372=ipsum|385=R|464=N|553=sit|554=consectetur|10=49|";
        private readonly TagPos[] _expectedTagPos =
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
            new(20, 554, 196, 11),
            new(21, 10, 211, 2)
        ];

        private TestEntity _testEntity;

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
        public void Begin_String_TagPos_Test()
        {
            var s = "8=FIX4.4|";
            var b = Encoding.UTF8.GetBytes(s);
            var ap = _testEntity.Parser;
            var duplex = _testEntity.Duplex;
            ap.ParseFrom(b);
            Assert.That(ap.Locations.Count(), Is.EqualTo(1));
            Assert.That(ap.Locations[0], Is.EqualTo(_expectedTagPos[0]));
            // we would not expect a message from this single field
            Assert.That(duplex.Reader.TryPeek(out var msg), Is.EqualTo(false));
        }

        [Test]
        public void Begin_String_Incorectly_Placed_Test()
        {
            var duplex = _testEntity.Duplex;
            var ex = Assert.Throws<InvalidDataException>(() => _testEntity.ParseTest("8=FIX4.4|8=FIX4.4|"));
            Assert.That(ex, Is.Not.Null);
            Assert.That(ex.Message, Is.EqualTo("BeginString: not expected at position [2]"));
            // we would not expect a message from illegal message
            Assert.That(duplex.Reader.TryPeek(out var msg), Is.EqualTo(false));
        }

        [Test]
        public void Begin_Length_Incorectly_Placed_Test()
        {
            var duplex = _testEntity.Duplex;
            var ex = Assert.Throws<InvalidDataException>(() => _testEntity.ParseTest("8=FIX4.4|9=101|9=101|"));
            Assert.That(ex, Is.Not.Null);
            Assert.That(ex.Message, Is.EqualTo("BodyLengthTag: not expected at position [3]"));
            // we would not expect a message from illegal message
            Assert.That(duplex.Reader.TryPeek(out var msg), Is.EqualTo(false));
        }

        [Test]
        public void Msg_Type_Incorectly_Placed_Test()
        {
            var duplex = _testEntity.Duplex;
            var ex = Assert.Throws<InvalidDataException>(() => _testEntity.ParseTest("8=FIX4.4|9=101|35=A|35=A|"));
            Assert.That(ex, Is.Not.Null);
            Assert.That(ex.Message, Is.EqualTo("MsgTag: not expected at position [4]"));
            // we would not expect a message from illegal message
            Assert.That(duplex.Reader.TryPeek(out var msg), Is.EqualTo(false));
        }

        [Test]
        public void Do_Not_Start_With_8_Test()
        {
            var duplex = _testEntity.Duplex;
            var ex = Assert.Throws<InvalidDataException>(() => _testEntity.ParseTest("59=FIX4.4|"));
            Assert.That(ex, Is.Not.Null);
            Assert.That(ex.Message, Is.EqualTo("position 1 [59] must be BeginString: 8="));
            // we would not expect a message from illegal message
            Assert.That(duplex.Reader.TryPeek(out var msg), Is.EqualTo(false));
        }

        [Test]
        public async Task Logon_Parsers_Correct_Tag_Set_Test()
        {
            var b = Encoding.UTF8.GetBytes(Logon);
            var ap = _testEntity.Parser;
            var duplex = _testEntity.Duplex;
            ap.ParseFrom(b);
            Assert.That(duplex.Reader.TryPeek(out var m), Is.EqualTo(true));
            var msg = await duplex.Reader.ReadAsync();
            Assert.That(msg.Tags, Is.EqualTo(_expectedTagPos));
        }

        [Test]
        public async Task Logon_Segment_Parse_Test()
        {
            var b = Encoding.UTF8.GetBytes(Logon);
            var ap = _testEntity.Parser;
            var duplex = _testEntity.Duplex;
            ap.ParseFrom(b);
            Assert.That(duplex.Reader.TryPeek(out var m), Is.EqualTo(true));
            var view = await duplex.Reader.ReadAsync() as AsciiView;
            Assert.That(view, Is.Not.Null);
            Assert.That(view.Structure, Is.Not.Null);
            Assert.That(view.Segment, Is.Not.Null);
            Assert.That(view.Definitions, Is.Not.Null);
            Assert.That(view.Tags, Is.Not.Null);
        }
    }
}
