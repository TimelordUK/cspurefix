using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Constraints;
using PureFix.Buffer;
using PureFix.Buffer.Ascii;
using PureFix.Dictionary.Definition;
using PureFix.Test.ModularTypes.Helpers;
using PureFix.Transport;
using PureFix.Types;
using PureFix.Types.Config;
using PureFix.Types.Core;
using PureFix.Types.FIX44;
using PureFix.Types.FIX44.Components;
using JsonHelper = PureFix.Types.JsonHelper;

namespace PureFix.Test.ModularTypes
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
            const string s = "8=FIX4.4|";
            var b = Encoding.UTF8.GetBytes(s);
            var ap = _testEntity.Parser;
            var q = new Queue<AsciiView>();
            ap.ParseFrom(b, b.Length, (_, view) => q.Enqueue((AsciiView)view));
            Assert.Multiple(() =>
            {
                Assert.That(ap.Locations, Has.Count.EqualTo(1));
                Assert.That(ap.Locations?[0], Is.EqualTo(_expectedTagPos[0]));
                // we would not expect a message from this single field
                Assert.That(q.TryPeek(out _), Is.EqualTo(false));
            });
        }

        [Test]
        public void Begin_String_Incorectly_Placed_Test()
        {
            var ex = Assert.Throws<InvalidDataException>(() => _testEntity.ParseText("8=FIX4.4|8=FIX4.4|"));
            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex.Message, Does.StartWith("BeginString: not expected at position [2]"));
            });
        }

        [Test]
        public void Begin_Length_Missing_Pos_3_Test()
        {
            var ex = Assert.Throws<InvalidDataException>(() => _testEntity.ParseText("8=FIX4.4|9=101|9=101|"));
            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex.Message, Does.StartWith("BodyLengthTag: not expected at position [3]"));
            });
        }

        [Test]
        public void Msg_Type_Incorectly_Placed_Test()
        {
            var ex = Assert.Throws<InvalidDataException>(() => _testEntity.ParseText("8=FIX4.4|9=101|35=A|35=A|"));
            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex.Message, Does.StartWith("MsgTag: not expected at position [4]"));
            });
        }

        [Test]
        public void Do_Not_Start_With_8_Test()
        {
            var ex = Assert.Throws<InvalidDataException>(() => _testEntity.ParseText("59=FIX4.4|"));
            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex.Message, Does.StartWith("position 1 [59] must be BeginString: 8="));
            });
        }

        [Test]
        public void Body_Length_Incorrectl_Placed_Test()
        {
            var ex = Assert.Throws<InvalidDataException>(() => _testEntity.ParseText("8=FIX4.4|59=101|9=101|"));
            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex.Message, Does.StartWith("position 2 [59] must be BodyLengthTag: 9="));
            });
        }

        [Test]
        public void MsgTag_Incorrectl_Placed_Test()
        {
            var ex = Assert.Throws<InvalidDataException>(() => _testEntity.ParseText("8=FIX4.4|9=101|59=A|"));
            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex.Message, Does.StartWith("position 3 [59] must be MsgTag: 35="));
            });
        }

        [Test]
        public void First_3_Fields_Correctly_Placed_Test()
        {
            var res = _testEntity.ParseText("8=FIX4.4|9=0000208|35=A|");
            var locs = _testEntity.Parser.Locations;
            Assert.Multiple(() =>
            {
                Assert.That(res, Has.Count.EqualTo(0));
                Assert.That(locs, Has.Count.EqualTo(3));
                Assert.That(locs?.ToArray(), Is.EqualTo(_expectedTagPos[..3]));
            });
        }

        [Test]
        public void Logon_Parsers_Correct_Tag_Set_Test()
        {
            var msgs = _testEntity.ParseText(Logon);
            Assert.Multiple(() =>
            {
                Assert.That(msgs, Has.Count.AtLeast(1));
                var msg = msgs[0];
                Assert.That(msg.Segment?.Name, Is.EqualTo("Logon"));
                var md = msg.Segment.Set as MessageDefinition;
                Assert.That(md, Is.Not.Null);
                Assert.That(md.MsgType, Is.EqualTo("A"));
                Assert.That(msg.Tags?.ToArray(), Is.EqualTo(_expectedTagPos));
            });
        }


        [Test]
        public void Logon_Chunks_Parsers_Correct_Tag_Set_Test()
        {
            var msgs = _testEntity.ParseTestHunks(Logon);
            Assert.Multiple(() =>
            {
                Assert.That(msgs, Has.Count.EqualTo(1));
                var msg = msgs[0];
                Assert.That(msg.Segment?.Name, Is.EqualTo("Logon"));
                var md = msg.Segment.Set as MessageDefinition;
                Assert.That(md, Is.Not.Null);
                Assert.That(md.MsgType, Is.EqualTo("A"));
                Assert.That(msg.Tags?.ToArray(), Is.EqualTo(_expectedTagPos));
            });
        }

        [Test]
        public void Logon_Dispatch_Reuse_View_Test()
        {
            var msgs = _testEntity.ParseTestHunks(Logon);
            Assert.That(msgs, Has.Count.EqualTo(1));
            msgs = _testEntity.ParseTestHunks(Logon);
            Assert.That(msgs, Has.Count.EqualTo(1));
        }

        [Test]
        public void Tags_Other_10_Past_Body_Length_Test()
        {
            const string begin = "8=FIX4.4|9=0000208|";
            var changed = Logon.Replace("10=49|", "555=you know nothin|10=49");
             _testEntity.ParseText(changed);
        }

        [Test]
        public void Unknown_Message_Type_Test()
        {
            var changed = Logon.Replace("35=A", "35=ZZ");
            var ex = Assert.Throws<InvalidDataException>(() => _testEntity.ParseText(changed));
            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex.Message, Does.StartWith("MsgType: [ZZ] not in definitions."));
            });
        }

        [Test]
        public void Complete_Msg_Parsed_Test()
        {
            var res = _testEntity.ParseText(Logon);
            Assert.That(res, Has.Count.EqualTo(1));
        }

        [Test]
        public void Complete_Msg_Parsed_In_Chunks_Test()
        {
            var res = _testEntity.ParseTestHunks(Logon);
            Assert.Multiple(() =>
            {
                var m = res[0];
                Assert.That(m, Is.Not.Null);
                Console.WriteLine(m.ToString());
            });
        }

        [Test]
        public void Logon_Segment_Parse_Test()
        {
            var res = _testEntity.ParseTestHunks(Logon);
            var view = res[0];
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
            var res = _testEntity.ParseText(changed);
            Assert.Multiple(() =>
            {
                var view = res[0];
                Assert.That(view, Is.Not.Null);
                var missing = view.Missing();
                Assert.That(missing, Is.EqualTo((int[])[108]));
            });
        }

        [Test]
        public void Missing_2_Required_Tag_Test()
        {
            var changed = Logon.Replace("98=2|108=62441|", "01=2|000=62441|");
            var res = _testEntity.ParseText(changed);
            Assert.Multiple(() =>
            {
                var view = res[0];
                Assert.That(view, Is.Not.Null);
                var missing = view.Missing();
                Assert.That(missing, Is.EqualTo((int[])[98, 108]));
            });
        }

        private void Check_Views(string s)
        {
            var ap = _testEntity.Parser;
            var views = new List<AsciiView>();
            var b = Encoding.UTF8.GetBytes(s);
            ap.ParseFrom(b, b.Length, (_, v) => views.Add((AsciiView)v));
            Assert.That(views, Has.Count.EqualTo(1));
        }

        [Test]
        public void Parse_Heartbeat_BodyLen_Test()
        {
            const string s = "8=FIX.4.4|9=0000105|35=0|49=accept-comp|56=init-comp|34=12|57=fix|52=20241011-19:23:54.012|112=heartbeat-10/11/2024 19:23:54|10=027|";
            var bodyLenCalc = s.Replace("8=FIX.4.4|9=0000105|", string.Empty).Replace("10=027|", string.Empty);
            Assert.That(bodyLenCalc, Has.Length.EqualTo(105));
            Check_Views(s);
        }

        [Test]
        public void Parse_Trade_Capture_BodyLen_Test()
        {
            const string s = "8=FIX.5.0SP2|9=0000188|35=AE|49=accept-comp|56=init-comp|34=103|57=fix|52=20241012-15:42:29.629|571=100099|487=0|856=0|828=0|17=600099|570=N|55=Steel|48=Steel|32=1000|31=100|75=20241012|60=20241012-15:42:29.628|10=094|";
            var bodyLenCalc = s.Replace("8=FIX.5.0SP2|9=0000188|", string.Empty).Replace("10=094|", string.Empty);
            Assert.That(bodyLenCalc, Has.Length.EqualTo(188));
            Check_Views(s);
        }

        [Test]
        public void Parse_Log_With_Preamble_Test()
        {
            var lp = new FixLogParser(Fix44PathHelper.DataDictPath);
            var views = new List<AsciiView>();
            lp.OnView = view => views.Add((AsciiView)view);
            lp.Snapshot(Fix44PathHelper.ReplayPreamblePath);
            Assert.That(views, Has.Count.EqualTo(1));
            var v = views[0];
            Assert.That(v, Is.Not.Null);
            Assert.That(v.MsgType(), Is.EqualTo(MsgTypeValues.Logon));
        }
    }
}
