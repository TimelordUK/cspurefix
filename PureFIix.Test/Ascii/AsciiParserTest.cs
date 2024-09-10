using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using PureFIix.Test.Env;
using PureFix.Buffer.Ascii;
using PureFix.Dictionary.Definition;
using PureFix.Tag;

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
            Assert.Multiple(() =>
            {
                Assert.That(ap.Locations, Has.Count.EqualTo(1));
                Assert.That(ap.Locations?[0], Is.EqualTo(_expectedTagPos[0]));
                // we would not expect a message from this single field
                Assert.That(duplex.TryPeek(out _), Is.EqualTo(false));
            });
        }

        [Test]
        public void Begin_String_Incorectly_Placed_Test()
        {
            var duplex = _testEntity.Duplex;
            var ex = Assert.Throws<InvalidDataException>(() => _testEntity.ParseTest("8=FIX4.4|8=FIX4.4|"));
            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex.Message, Is.EqualTo("BeginString: not expected at position [2]"));
                // we would not expect a message from illegal message
                Assert.That(duplex.TryPeek(out _), Is.EqualTo(false));
            });
        }

        [Test]
        public void Begin_Length_Missing_Pos_3_Test()
        {
            var duplex = _testEntity.Duplex;
            var ex = Assert.Throws<InvalidDataException>(() => _testEntity.ParseTest("8=FIX4.4|9=101|9=101|"));
            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex.Message, Is.EqualTo("BodyLengthTag: not expected at position [3]"));
                // we would not expect a message from illegal message
                Assert.That(duplex.TryPeek(out _), Is.EqualTo(false));
            });
        }

        [Test]
        public void Msg_Type_Incorectly_Placed_Test()
        {
            var duplex = _testEntity.Duplex;
            var ex = Assert.Throws<InvalidDataException>(() => _testEntity.ParseTest("8=FIX4.4|9=101|35=A|35=A|"));
            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex.Message, Is.EqualTo("MsgTag: not expected at position [4]"));
                // we would not expect a message from illegal message
                Assert.That(duplex.TryPeek(out _), Is.EqualTo(false));
            });
        }

        [Test]
        public void Do_Not_Start_With_8_Test()
        {
            var duplex = _testEntity.Duplex;
            var ex = Assert.Throws<InvalidDataException>(() => _testEntity.ParseTest("59=FIX4.4|"));
            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex.Message, Is.EqualTo("position 1 [59] must be BeginString: 8="));
                // we would not expect a message from illegal message
                Assert.That(duplex.TryPeek(out _), Is.EqualTo(false));
            });
        }

        [Test]
        public void Body_Length_Incorrectl_Placed_Test()
        {
            var duplex = _testEntity.Duplex;
            var ex = Assert.Throws<InvalidDataException>(() => _testEntity.ParseTest("8=FIX4.4|59=101|9=101|"));
            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex.Message, Is.EqualTo("position 2 [59] must be BodyLengthTag: 9="));
                // we would not expect a message from illegal message
                Assert.That(duplex.TryPeek(out _), Is.EqualTo(false));
            });
        }

        [Test]
        public void MsgTag_Incorrectl_Placed_Test()
        {
            var duplex = _testEntity.Duplex;
            var ex = Assert.Throws<InvalidDataException>(() => _testEntity.ParseTest("8=FIX4.4|9=101|59=A|"));
            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex.Message, Is.EqualTo("position 3 [59] must be MsgTag: 35="));
                // we would not expect a message from illegal message
                Assert.That(duplex.TryPeek(out _), Is.EqualTo(false));
            });
        }

        [Test]
        public void First_3_Fields_Correctly_Placed_Test()
        {
            var duplex = _testEntity.Duplex;
            _testEntity.ParseTest("8=FIX4.4|9=0000208|35=A|");
            var locs = _testEntity.Parser.Locations;
            Assert.Multiple(() =>
            {
                Assert.That(duplex.TryPeek(out _), Is.EqualTo(false));
                Assert.That(locs, Has.Count.EqualTo(3));
                Assert.That(locs, Is.EqualTo(_expectedTagPos[..3]));
            });
        }

        [Test]
        public void Logon_Parsers_Correct_Tag_Set_Test()
        {
            _testEntity.ParseTest(Logon);
            var duplex = _testEntity.Duplex;
            Assert.Multiple(async () =>
            {
                Assert.That(duplex.TryPeek(out _), Is.EqualTo(true));
                var msg = await duplex.ReadAsync();
                Assert.That(msg.Segment?.Name, Is.EqualTo("Logon"));
                var md = msg.Segment.Set as MessageDefinition;
                Assert.That(md, Is.Not.Null);
                Assert.That(md.MsgType, Is.EqualTo("A"));
                Assert.That(msg.Tags, Is.EqualTo(_expectedTagPos));
            });
        }


        [Test]
        public void Logon_Chunks_Parsers_Correct_Tag_Set_Test()
        {
            _testEntity.ParseTestHunks(Logon);
            var duplex = _testEntity.Duplex;
            Assert.Multiple(async () =>
            {
                Assert.That(duplex.TryPeek(out _), Is.EqualTo(true));
                var msg = await duplex.ReadAsync();
                Assert.That(msg.Segment?.Name, Is.EqualTo("Logon"));
                var md = msg.Segment.Set as MessageDefinition;
                Assert.That(md, Is.Not.Null);
                Assert.That(md.MsgType, Is.EqualTo("A"));
                Assert.That(msg.Tags, Is.EqualTo(_expectedTagPos));
            });
        }
        
        [Test]
        public void Tags_Other_10_Past_Body_Length_Test()
        {
            var duplex = _testEntity.Duplex;
            const string begin = "8=FIX4.4|9=0000208|";
            var changed = Logon.Replace("10=49|", "555=you know nothin|10=49");
            var ex = Assert.Throws<InvalidDataException>(() => _testEntity.ParseTest(changed));
            Assert.Multiple( () =>
            {
                Assert.That(duplex.TryPeek(out _), Is.EqualTo(false));
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex.Message, Is.EqualTo($"Tag: [555] cant be after {208 + begin.Length - 1}"));
            });
        }

        [Test]
        public void Unknown_Message_Type_Test()
        {
            var duplex = _testEntity.Duplex;
            var changed = Logon.Replace("35=A", "35=ZZ");
            var ex = Assert.Throws<InvalidDataException>(() => _testEntity.ParseTest(changed));
            Assert.Multiple(() =>
            {
                Assert.That(duplex.TryPeek(out _), Is.EqualTo(false));
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex.Message, Is.EqualTo("MsgType: [ZZ] not in definitions."));
            });
        }

        [Test]
        public void Complete_Msg_Parsed_Test()
        {
            _testEntity.ParseTest(Logon);
            var duplex = _testEntity.Duplex;
            Assert.That(duplex.TryPeek(out _), Is.EqualTo(true));
        }

        [Test]
        public void Complete_Msg_Parsed_In_Chunks_Test()
        {
            _testEntity.ParseTestHunks(Logon);
            var duplex = _testEntity.Duplex;
            Assert.Multiple(() =>
            {
                Assert.That(duplex.TryPeek(out var m), Is.EqualTo(true));
                Assert.That(m, Is.Not.Null);
                Console.WriteLine(m.ToString());
            });
        }

        [Test]
        public async Task Logon_Segment_Parse_Test()
        {
            _testEntity.ParseTestHunks(Logon);
            var duplex = _testEntity.Duplex;
            Assert.That(duplex.TryPeek(out _), Is.EqualTo(true));
            var view = await duplex.ReadAsync() as AsciiView;
            Assert.Multiple(() =>
            {
                Assert.That(view, Is.Not.Null);
                Assert.That(view.Structure, Is.Not.Null);
                Assert.That(view.Segment, Is.Not.Null);
                Assert.That(view.Definitions, Is.Not.Null);
                Assert.That(view.Tags, Is.Not.Null);
            });
        }

        [Test]
        public void Missing_1_Required_Tag_Test()
        {
            var changed = Logon.Replace("108=62441|", "000=62441|");
            _testEntity.ParseTest(changed);
            var duplex = _testEntity.Duplex;
            Assert.Multiple(async () =>
            {
                Assert.That(duplex.TryPeek(out _), Is.EqualTo(true));
                var view = await duplex.ReadAsync() as AsciiView;
                Assert.That(view, Is.Not.Null);
                var missing = view.Missing();
                Assert.That(missing, Is.EqualTo((int[])[108]));
            });
        }

        [Test]
        public void Missing_2_Required_Tag_Test()
        {
            var changed = Logon.Replace("98=2|108=62441|", "01=2|000=62441|");
            _testEntity.ParseTest(changed);
            var duplex = _testEntity.Duplex;
            Assert.Multiple(async () =>
            {
                Assert.That(duplex.TryPeek(out _), Is.EqualTo(true));
                var view = await duplex.ReadAsync() as AsciiView;
                Assert.That(view, Is.Not.Null);
                var missing = view.Missing();
                Assert.That(missing, Is.EqualTo((int[])[98, 108]));
            });
        }
    }
}
