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
using PureFix.Dictionary.Contained;

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
            var msg = structure?.Msg();
            Assert.That(msg, Is.Not.Null);
            var parties = structure?.FirstContainedWithin("Parties", msg);
            var noPartyIDs = structure?.FirstContainedWithin("NoPartyIDs", msg);
            Assert.Multiple(() =>
            {
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
            });
        }

        [Test]
        public void Parties_PartySubIDType_Structure_Test()
        {
            var structure = _views[0].Structure;
            var msg = structure?.Msg();
            Assert.That(msg, Is.Not.Null);
            var ptysSubGrp = structure?.GetInstances("PtysSubGrp");
            Assert.Multiple(() =>
            {
                Assert.That(ptysSubGrp, Is.Not.Null);
                Assert.That(ptysSubGrp, Has.Count.EqualTo(3));
                Assert.That(ptysSubGrp[0].StartPosition, Is.EqualTo(24));
                Assert.That(ptysSubGrp[0].Type, Is.EqualTo(SegmentType.Component));
                Assert.That(ptysSubGrp[1].StartPosition, Is.EqualTo(32));
                Assert.That(ptysSubGrp[1].Type, Is.EqualTo(SegmentType.Component));
                Assert.That(ptysSubGrp[2].StartPosition, Is.EqualTo(42));
                Assert.That(ptysSubGrp[2].Type, Is.EqualTo(SegmentType.Component));

                var noPartySubIDs = structure?.GetInstances("NoPartySubIDs");
                Assert.That(noPartySubIDs, Is.Not.Null);
                Assert.That(noPartySubIDs, Has.Count.EqualTo(3));

                Assert.That(noPartySubIDs[0].DelimiterTag, Is.EqualTo(523));
                Assert.That(noPartySubIDs[0].Type, Is.EqualTo(SegmentType.Group));
                Assert.That(noPartySubIDs[0].DelimiterPositions, Is.EqualTo(new List<int>([25,27])));
                Assert.That(noPartySubIDs[0].Depth, Is.EqualTo(4));

                Assert.That(noPartySubIDs[1].DelimiterTag, Is.EqualTo(523));
                Assert.That(noPartySubIDs[1].Type, Is.EqualTo(SegmentType.Group));
                Assert.That(noPartySubIDs[1].DelimiterPositions, Is.EqualTo(new List<int>([33, 35, 37])));
                Assert.That(noPartySubIDs[1].Depth, Is.EqualTo(4));

                Assert.That(noPartySubIDs[2].DelimiterTag, Is.EqualTo(523));
                Assert.That(noPartySubIDs[2].Type, Is.EqualTo(SegmentType.Group));
                Assert.That(noPartySubIDs[2].DelimiterPositions, Is.EqualTo(new List<int>([43])));
                Assert.That(noPartySubIDs[2].Depth, Is.EqualTo(4));
            });
        }

        [Test]
        public void ContraGrp_Structure_Test()
        {
            var structure = _views[0].Structure;
            var msg = structure?.Msg();
            Assert.That(msg, Is.Not.Null);
            var contraGrp = structure?.GetInstance("ContraGrp");
            Assert.Multiple(() =>
            {
                Assert.That(contraGrp, Is.Not.Null);
                Assert.That(contraGrp.StartPosition, Is.EqualTo(46));
                Assert.That(contraGrp.EndPosition, Is.EqualTo(61));
                Assert.That(contraGrp.Depth, Is.EqualTo(1));
                Assert.That(contraGrp.Type, Is.EqualTo(SegmentType.Component));

                var noContraBrokers = structure?.GetInstance("NoContraBrokers");
                Assert.That(noContraBrokers, Is.Not.Null);
                Assert.That(noContraBrokers.Type, Is.EqualTo(SegmentType.Group));
                Assert.That(noContraBrokers.Depth, Is.EqualTo(2));
                Assert.That(noContraBrokers.DelimiterTag, Is.EqualTo(375));
                Assert.That(noContraBrokers.DelimiterPositions, Is.EqualTo(new List<int>([47, 52, 57])));
            });
        }

        [Test]
        public void Instrument_Structure_Test()
        {
            var i = _testEntity.Definitions.Component.GetValueOrDefault("Instrument");
            Assert.That(i, Is.Not.Null);
            var structure = _views[0].Structure;
            var msg = structure?.Msg();
            Assert.That(msg, Is.Not.Null);
            var instrument = structure?.GetInstance("Instrument");
            Assert.Multiple(() =>
            {
                Assert.That(instrument, Is.Not.Null);
                Assert.That(instrument.StartPosition, Is.EqualTo(83));
                Assert.That(instrument.StartTag, Is.EqualTo(55));
                Assert.That(instrument.EndPosition, Is.EqualTo(133));
                Assert.That(instrument.Depth, Is.EqualTo(1));
                Assert.That(instrument.EndTag, Is.EqualTo(874));
                Assert.That(instrument.Type, Is.EqualTo(SegmentType.Component));   
            });
        }

        [Test]
        public void FinancingDetails_Structure_Test()
        {
            var structure = _views[0].Structure;
            var msg = structure?.Msg();
            Assert.That(msg, Is.Not.Null);
            var financingDetails = structure?.GetInstance("FinancingDetails");
            Assert.Multiple(() =>
            {
                Assert.That(financingDetails, Is.Not.Null);
                Assert.That(financingDetails.StartPosition, Is.EqualTo(134));
                Assert.That(financingDetails.StartTag, Is.EqualTo(913));
                Assert.That(financingDetails.EndPosition, Is.EqualTo(142));
                Assert.That(financingDetails.Depth, Is.EqualTo(1));
                Assert.That(financingDetails.EndTag, Is.EqualTo(898));
                Assert.That(financingDetails.Type, Is.EqualTo(SegmentType.Component));
            });
        }

        [Test]
        public void View_To_Execution_Report_Test()
        {
            Assert.That(_views, Is.Not.Null);
            Assert.That(_views, Has.Count.EqualTo(1));
            var mv = _views[0];
            var er = new ExecutionReport();
            er.Parse(mv);
        }
    }
}
