using PureFIix.Test.Env;
using PureFix.Buffer.Ascii;
using PureFix.Tag;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.ParserFormat;
using PureFix.Types.FIX4._4.quickfix.set;

namespace PureFIix.Test.Ascii
{
    public class AsciiViewHeaderParseTest
    {
        private TestEntity _testEntity;
        private List<AsciiView> _views;

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
        public void Get_Header_View_Test()
        {
            Assert.That(_views, Is.Not.Null);
            Assert.That(_views, Has.Count.EqualTo(1));
            var mv = _views[0];
            Assert.That(mv, Is.Not.Null);
            var sh = mv.GetView("StandardHeader");
            Assert.That(sh, Is.Not.Null);
        }

        [Test]
        public void Parse_Header_View_Timing_Test()
        {
            Assert.That(_views, Is.Not.Null);
            Assert.That(_views, Has.Count.EqualTo(1));
            var mv = _views[0];
            Assert.That(mv, Is.Not.Null);
            var sh = mv.GetView("StandardHeader");
            Assert.That(sh, Is.Not.Null);
            var instance = new StandardHeader();
            var sw = new Stopwatch();
            sw.Start();
            const int count = 100000;
            for (var i = 1; i < count; ++i)
            {
                instance.Parse(sh);
            }
            sw.Stop();
            Console.WriteLine($"{sw.ElapsedMilliseconds} {(decimal)sw.ElapsedMilliseconds / count * 1000}  micro/msg");
        }

        [Test]
        public void Parse_Header_View_Test()
        {
            Assert.That(_views, Is.Not.Null);
            Assert.That(_views, Has.Count.EqualTo(1));
            var mv = _views[0];
            Assert.That(mv, Is.Not.Null);
            var sh = mv.GetView("StandardHeader");
            Assert.That(sh, Is.Not.Null);
            /*
            [0] 8 (BeginString) = FIX4.4, [1] 9 (BodyLength) = 0000208
            [2] 35 (MsgType) = A [Logon], [3] 49 (SenderCompID) = sender-10
            [4] 56 (TargetCompID) = target-20, [5] 34 (MsgSeqNum) = 1
            [6] 57 (TargetSubID) = sub-a, [7] 52 (SendingTime) = 20180610-10:39:01.621
             */
            var instance = new StandardHeader();
            instance.Parse(sh);
            Assert.That(instance.BeginString, Is.EqualTo("FIX4.4"));
            Assert.That(instance.BodyLength, Is.EqualTo(208));
            Assert.That(instance.MsgType, Is.EqualTo("A"));
            Assert.That(instance.SenderCompID, Is.EqualTo("sender-10"));
            Assert.That(instance.TargetCompID, Is.EqualTo("target-20"));
            Assert.That(instance.MsgSeqNum, Is.EqualTo(1));
            Assert.That(instance.TargetSubID, Is.EqualTo("sub-a"));
        }
    }
}
