using PureFIix.Test.Env;
using PureFix.Buffer.Ascii;
using PureFix.Types;
using PureFix.Types.FIX44.QuickFix;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            ((IFixParser)hb).Parse(mv);

            Assert.That(QuickLookup.On(hb)["StandardHeader"]["BeginString"].As<string>(), Is.EqualTo("FIX.4.4"));
            Assert.That(QuickLookup.On(hb)["StandardHeader"]["MsgType"].As<string>(), Is.EqualTo("0"));
            Assert.That(QuickLookup.On(hb)["StandardHeader"]["TargetCompID"].As<string>(), Is.EqualTo("accept-comp"));
            Assert.That(QuickLookup.On(hb)["TestReqID"].As<string>(), Is.EqualTo("Sun, 01 Jan 2023 14:14:20 GMT"));
        }

        [Test]
        public async Task Replay_Again_Check_Stats_Test()
        {
            // a new parser is constructed for each test, here we parse twice on same instance
            var text = await TestEntity.GetTextAsync(Fix44PathHelper.HeartbeatReplayPath);
            var views1 = _testEntity.ParseText(text);
            Assert.That(views1, Has.Count.GreaterThan(0));
            var views2 = _testEntity.ParseText(text);
            Assert.That(views2, Has.Count.GreaterThan(0));
            var stats = _testEntity.Parser.ParserStats;
            // we will have taken 2 buffers, returned 2 for views above and we are waiting to parse into
            // a new buffer
            Assert.Multiple(() =>
            {
                Assert.That(stats.Rents, Is.EqualTo(3));
                Assert.That(stats.Returns, Is.EqualTo(0));
                Assert.That(stats.ParsedMessages, Is.EqualTo(2));
                Assert.That(stats.ReceivedBytes, Is.EqualTo(2 * text.Length));
            });
        }

        [Test]
        public void Get_Heartbeat_View_Ascii_Parser_Test()
        {
            _testEntity.TimeParsePath("Get_Heartbeat_View_Ascii_Parser_Test", Fix44PathHelper.HeartbeatReplayPath, 1000);
            _testEntity.TimeParsePath("Get_Heartbeat_View_Ascii_Parser_Test", Fix44PathHelper.HeartbeatReplayPath, 20000);
        }
    }
}
