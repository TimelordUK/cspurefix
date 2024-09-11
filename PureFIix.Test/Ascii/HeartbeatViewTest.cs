using PureFIix.Test.Env;
using PureFix.Buffer.Ascii;
using PureFix.ParserFormat;
using PureFix.Types.FIX4._4.quickfix;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFIix.Test.Ascii
{
    internal class HeartbeatMsgViewTest
    {
        private TestEntity _testEntity;
        private List<AsciiView> _views;

        [OneTimeSetUp]
        public async Task OnceSetup()
        {
            _testEntity = new TestEntity();
            _views = await _testEntity.Replay(Fix44PathHelper.HeartbeatReplayPath);
        }

        [SetUp]
        public void Setup()
        {
            _testEntity.Prepare();
        }

        [Test]
        public void Get_View_Test()
        {
            Assert.That(_views, Is.Not.Null);
            Assert.That(_views, Has.Count.EqualTo(1));
            var mv = _views[0];
            Assert.That(mv, Is.Not.Null);
        }

        [Test]
        public void Parse_Header_View_Test()
        {
            Assert.That(_views, Is.Not.Null);
            Assert.That(_views, Has.Count.EqualTo(1));
            var mv = _views[0];
            Assert.That(mv, Is.Not.Null);
            var hb = new Heartbeat();
            hb.Parse(mv);
        }

        [Test]
        public async Task Replay_Again_Check_Stats_Test()
        {
            // a new parser is constructed for each test, here we parse twice on same instance
            var text = await TestEntity.GetText(Fix44PathHelper.HeartbeatReplayPath);
            var views1 = _testEntity.ParseText(text);
            var views2 = _testEntity.ParseText(text);
            var stats = _testEntity.Parser.ParserStats;
            // we will have taken 2 buffers, returned 2 for views above and we are waiting to parse into
            // a new buffer
            Assert.Multiple(() =>
            {
                Assert.That(stats.Rents, Is.EqualTo(3));
                Assert.That(stats.Returns, Is.EqualTo(2));
                Assert.That(stats.ParsedMessages, Is.EqualTo(2));
                Assert.That(stats.ReceivedBytes, Is.EqualTo(2 * text.Length));
            });
        }

        [Test]
        public async Task Get_Heartbeat_View_Ascii_Parser_Test()
        {
            const int count = 10000;
            var sw = new Stopwatch();
            var one = await TestEntity.GetText(Fix44PathHelper.HeartbeatReplayPath);
            var all = string.Concat(Enumerable.Repeat(one, count));
            var b = Encoding.UTF8.GetBytes(all);
            var msgs = new List<AsciiView>(count);
            sw.Start();
            _testEntity.Parser.ParseFrom(b, (i, view) => msgs.Add(view));
            //_testEntity.Parser.ParseFrom(b, null);
            sw.Stop();
            Assert.That(msgs, Has.Count.EqualTo(count));
            Console.WriteLine($"{sw.Elapsed.Milliseconds} {(decimal)sw.Elapsed.Microseconds / count}  micro/msg");
        }
    }
}
