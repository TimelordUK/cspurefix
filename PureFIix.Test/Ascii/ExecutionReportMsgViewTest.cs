using PureFIix.Test.Env;
using PureFix.Buffer.Ascii;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.ObjectPool;
using PureFix.Types.FIX44.QuickFix;
using PureFix.Buffer.Segment;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFIix.Test.Ascii
{

    internal class ExecutionReportMsgViewTest
    {
        private TestEntity _testEntity;
        private List<AsciiView> _views;

        [OneTimeSetUp]
        public async Task OnceSetup()
        {
            _testEntity = new TestEntity();
            _views = await _testEntity.Replay(Fix44PathHelper.ExecutionReportReplayPath);
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
        public void Get_RepeatedTag_Test()
        {
            Assert.That(_views, Is.Not.Null);
            Assert.That(_views, Has.Count.EqualTo(1));
            var mv = _views[0];
            Assert.That(mv, Is.Not.Null);
            var values = mv.GetStrings(803);
            Assert.That(values, Is.EqualTo((string[])["22", "10", "12", "13", "18", "6"]));
        }

        [Test]
        public void Parse_Header_View_Test()
        {
            Assert.That(_views, Is.Not.Null);
            Assert.That(_views, Has.Count.EqualTo(1));
            var mv = _views[0];
            Assert.That(mv, Is.Not.Null);
            var message = new ExecutionReport();
            message.Parse(mv);
        }

        [Test]
        public void Get_InstrmtLegExecGrp_Test()
        {
            Assert.That(_views, Is.Not.Null);
            Assert.That(_views, Has.Count.EqualTo(1));
            var mv = _views[0];
            Assert.That(mv, Is.Not.Null);
            var v = mv.GetView("InstrmtLegExecGrp");
            Assert.That(v, Is.Not.Null);
            var noLegs = v.GetView("NoLegs");
            Assert.That(noLegs, Is.Not.Null);
            var count = noLegs.GroupCount();
            Assert.That(count, Is.EqualTo(3));
            var leg0 = noLegs.GetGroupInstance(0);
            Assert.That(leg0, Is.Not.Null);
            Assert.That(leg0.GetString("LegSymbol"), Is.EqualTo("posuere"));
            var l0 = mv.GetView("InstrmtLegExecGrp.NoLegs")?[0];
            Assert.That(l0, Is.Not.Null);
            Assert.That(l0.GetString("LegSymbol"), Is.EqualTo("posuere"));
        }

        [Test]
        public void Parties_Structure_Test()
        {
            var structure = _views[0].Structure;
            var parties = structure?.Segments.FirstOrDefault(s => s.Name == "Parties");
            var noPartyIDs = structure?.Segments.FirstOrDefault(s => s.Name == "NoPartyIDs");
            Assert.That(parties, Is.Not.Null);
            Assert.That(parties.StartPosition, Is.EqualTo(20));
            Assert.That(parties.EndPosition, Is.EqualTo(44));
            Assert.That(parties.Depth, Is.EqualTo(1));
            Assert.That(parties.Type, Is.EqualTo(SegmentType.Component));
            Assert.That(noPartyIDs, Is.Not.Null);
            Assert.That(noPartyIDs.StartPosition, Is.EqualTo(20));
            Assert.That(noPartyIDs.EndPosition, Is.EqualTo(44));
            Assert.That(noPartyIDs.Depth, Is.EqualTo(2));
            Assert.That(noPartyIDs.Type, Is.EqualTo(SegmentType.Group));
        }
    }
}
