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


        /*
[46] 382 (NoContraBrokers) = 3, [47] 375 (ContraBroker) = Quisque
[48] 337 (ContraTrader) = tincidunt, [49] 437 (ContraTradeQty) = 18171
[50] 438 (ContraTradeTime) = 20180528-16:38:03.972, [51] 655 (ContraLegRefID) = Class
[52] 375 (ContraBroker) = taciti, [53] 337 (ContraTrader) = ad
[54] 437 (ContraTradeQty) = 91261, [55] 438 (ContraTradeTime) = 20180528-16:38:03.972
[56] 655 (ContraLegRefID) = torquent, [57] 375 (ContraBroker) = conubia
[58] 337 (ContraTrader) = per, [59] 437 (ContraTradeQty) = 97017
[60] 438 (ContraTradeTime) = 20180528-16:38:03.972, [61] 655 (ContraLegRefID) = himenaeos.
 */



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


        /*
[82] 635 (ClearingFeeIndicator) = 5[5TH_YEAR_DELEGATE_TRADING_FOR_HIS_OWN_ACCOUNT], [83] 55 (Symbol) = ac,
[84] 65 (SymbolSfx) = non, [85] 48 (SecurityID) = Pellentesque
[86] 22 (SecurityIDSource) = B[WERTPAPIER], [87] 454 (NoSecurityAltID) = 2
[88] 455 (SecurityAltID) = lorem, [89] 456 (SecurityAltIDSource) = consequat
[90] 455 (SecurityAltID) = sapien, [91] 456 (SecurityAltIDSource) = tempor
[92] 460 (Product) = 2[COMMODITY], [93] 461 (CFICode) = a
[94] 167 (SecurityType) = SECLOAN[SECURITIES_LOAN], [95] 762 (SecuritySubType) = purus
[96] 200 (MaturityMonthYear) = ut, [97] 541 (MaturityDate) = 20180528-16:38:03.972
[98] 201 (PutOrCall) = 1[CALL], [99] 224 (CouponPaymentDate) = 20180528-16:38:03.972
[100] 225 (IssueDate) = 20180528-16:38:03.972, [101] 239 (RepoCollateralSecurityType) = Proin
[102] 226 (RepurchaseTerm) = 62025, [103] 227 (RepurchaseRate) = 27005
[104] 228 (Factor) = 68810, [105] 255 (CreditRating) = justo
[106] 543 (InstrRegistry) = ut, [107] 470 (CountryOfIssue) = nibh
[108] 471 (StateOrProvinceOfIssue) = at., [109] 472 (LocaleOfIssue) = fermentum
[110] 240 (RedemptionDate) = 20180528-16:38:03.972, [111] 202 (StrikePrice) = 52639
[112] 947 (StrikeCurrency) = 50824, [113] 206 (OptAttribute) = risus,
[114] 231 (ContractMultiplier) = 10378, [115] 223 (CouponRate) = 25946
[116] 207 (SecurityExchange) = placerat, [117] 106 (Issuer) = luctus
[118] 348 (EncodedIssuerLen) = 20, [119] 349 (EncodedIssuer) = zqJsegy0CQ8EyKQ1bmLw
[120] 107 (SecurityDesc) = Vivamus, [121] 350 (EncodedSecurityDescLen) = 20
[122] 351 (EncodedSecurityDesc) = A1xB4jDS31E4zM1xAbk5, [123] 691 (Pool) = mi
[124] 667 (ContractSettlMonth) = arcu, [125] 875 (CPProgram) = 2[4]
[126] 876 (CPRegType) = rhoncus, [127] 864 (NoEvents) = 1
[128] 865 (EventType) = 1[PUT], [129] 866 (EventDate) = 20180528-16:38:03.973
[130] 867 (EventPx) = 16817, [131] 868 (EventText) = amet
[132] 873 (DatedDate) = 20180528-16:38:03.973, [133] 874 (InterestAccrualDate) = 20180528-16:38:03.973
 */

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

        /*
[134] 913 (AgreementDesc) = sit, [135] 914 (AgreementID) = eleifend
[136] 915 (AgreementDate) = 20180528-16:38:03.973, [137] 918 (AgreementCurrency) = 78552
[138] 788 (TerminationType) = 2[TERM], [139] 916 (StartDate) = 20180528-16:38:03.973
[140] 917 (EndDate) = 20180528-16:38:03.973, [141] 919 (DeliveryType) = 3[HOLD_IN_CUSTODY]
[142] 898 (MarginRatio) = 13625, [143] 711 (NoUnderlyings) = 2
 */

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

        /*
[272] 211 (PegOffsetValue) = 38459, [273] 835 (PegMoveType) = 1[FIXED]
[274] 836 (PegOffsetType) = [undefined], [275] 837 (PegLimitType) = 2
[276] 838 (PegRoundDirection) = 2, [277] 840 (PegScope) = 4[NATIONAL_EXCLUDING_LOCAL]
 */

        [Test]
        public void PegInstructions_Structure_Test()
        {
            var structure = _views[0].Structure;
            var msg = structure?.Msg();
            Assert.That(msg, Is.Not.Null);
            var pegInstructions = structure?.GetInstance("PegInstructions");
            Assert.Multiple(() =>
            {
                Assert.That(pegInstructions, Is.Not.Null);
                Assert.That(pegInstructions.StartPosition, Is.EqualTo(272));
                Assert.That(pegInstructions.StartTag, Is.EqualTo(211));
                Assert.That(pegInstructions.EndPosition, Is.EqualTo(277));
                Assert.That(pegInstructions.Depth, Is.EqualTo(1));
                Assert.That(pegInstructions.EndTag, Is.EqualTo(840));
                Assert.That(pegInstructions.Type, Is.EqualTo(SegmentType.Component));
            });
        }

        /*
[278] 388 (DiscretionInst) = 2[RELATED_TO_PRIMARY_PRICE], [279] 389 (DiscretionOffsetValue) = 48430
[280] 841 (DiscretionMoveType) = 1[FIXED], [281] 842 (DiscretionOffsetType) = 3[PRICE_TIER]
[282] 843 (DiscretionLimitType) = 2, [283] 844 (DiscretionRoundDirection) = 2
[284] 846 (DiscretionScope) = 4[NATIONAL_EXCLUDING_LOCAL], [285] 839 (PeggedPrice) = 18644
 */


        [Test]
        public void DiscretionInstructions_Structure_Test()
        {
            var structure = _views[0].Structure;
            var msg = structure?.Msg();
            Assert.That(msg, Is.Not.Null);
            var discretionInstructions = structure?.GetInstance("DiscretionInstructions");
            Assert.Multiple(() =>
            {
                Assert.That(discretionInstructions, Is.Not.Null);
                Assert.That(discretionInstructions.StartPosition, Is.EqualTo(278));
                Assert.That(discretionInstructions.StartTag, Is.EqualTo(388));
                Assert.That(discretionInstructions.EndPosition, Is.EqualTo(284));
                Assert.That(discretionInstructions.Depth, Is.EqualTo(1));
                Assert.That(discretionInstructions.EndTag, Is.EqualTo(846));
                Assert.That(discretionInstructions.Type, Is.EqualTo(SegmentType.Component));
            });
        }

        /*
[324] 12 (Commission) = 3339, [325] 13 (CommType) = 1[PER_UNIT]
[326] 479 (CommCurrency) = 25841, [327] 497 (FundRenewWaiv) = N[NO]
 */

        [Test]
        public void CommissionData_Structure_Test()
        {
            var structure = _views[0].Structure;
            var msg = structure?.Msg();
            Assert.That(msg, Is.Not.Null);
            var commisionData = structure?.GetInstance("CommissionData");
            Assert.Multiple(() =>
            {
                Assert.That(commisionData, Is.Not.Null);
                Assert.That(commisionData.StartPosition, Is.EqualTo(324));
                Assert.That(commisionData.StartTag, Is.EqualTo(12));
                Assert.That(commisionData.EndPosition, Is.EqualTo(327));
                Assert.That(commisionData.Depth, Is.EqualTo(1));
                Assert.That(commisionData.EndTag, Is.EqualTo(497));
                Assert.That(commisionData.Type, Is.EqualTo(SegmentType.Component));
            });
        }

        /*
        [328] 218 (Spread) = 72687, [329] 220 (BenchmarkCurveCurrency) = 84249
        [330] 221 (BenchmarkCurveName) = Pellentesque, [331] 222 (BenchmarkCurvePoint) = luctus
        [332] 662 (BenchmarkPrice) = 90721, [333] 663 (BenchmarkPriceType) = 66615
        [334] 699 (BenchmarkSecurityID) = et, [335] 761 (BenchmarkSecurityIDSource) = nunc.
         */

        [Test]
        public void SpreadOrBenchmarkCurveData_Structure_Test()
        {
            var structure = _views[0].Structure;
            var msg = structure?.Msg();
            Assert.That(msg, Is.Not.Null);
            var commisionData = structure?.GetInstance("SpreadOrBenchmarkCurveData");
            Assert.Multiple(() =>
            {
                Assert.That(commisionData, Is.Not.Null);
                Assert.That(commisionData.StartPosition, Is.EqualTo(328));
                Assert.That(commisionData.StartTag, Is.EqualTo(218));
                Assert.That(commisionData.EndPosition, Is.EqualTo(335));
                Assert.That(commisionData.Depth, Is.EqualTo(1));
                Assert.That(commisionData.EndTag, Is.EqualTo(761));
                Assert.That(commisionData.Type, Is.EqualTo(SegmentType.Component));
            });
        }

        /*
[336] 235 (YieldType) = INVERSEFLOATER[INVERSE_FLOATER_BOND_YIELD], [337] 236 (Yield) = 34183
[338] 701 (YieldCalcDate) = 20180528-16:38:03.973, [339] 696 (YieldRedemptionDate) = 20180528-16:38:03.973
[340] 697 (YieldRedemptionPrice) = 3652, [341] 698 (YieldRedemptionPriceType) = 10535
 */


        [Test]
        public void YieldData_Structure_Test()
        {
            var structure = _views[0].Structure;
            var msg = structure?.Msg();
            Assert.That(msg, Is.Not.Null);
            var commisionData = structure?.GetInstance("YieldData");
            Assert.Multiple(() =>
            {
                Assert.That(commisionData, Is.Not.Null);
                Assert.That(commisionData.StartPosition, Is.EqualTo(336));
                Assert.That(commisionData.StartTag, Is.EqualTo(235));
                Assert.That(commisionData.EndPosition, Is.EqualTo(341));
                Assert.That(commisionData.Depth, Is.EqualTo(1));
                Assert.That(commisionData.EndTag, Is.EqualTo(698));
                Assert.That(commisionData.Type, Is.EqualTo(SegmentType.Component));
            });
        }

        /*
        [384] 851 (LastLiquidityInd) = 3[LIQUIDITY_ROUTED_OUT], [385] 518 (NoContAmts) = 3
        [386] 519 (ContAmtType) = 5[DISCOUNT_AMOUNT], [387] 520 (ContAmtValue) = 95673
        [388] 521 (ContAmtCurr) = 87528, [389] 519 (ContAmtType) = 14[FUND_BASED_RENEWAL_COMMISSION_AMOUNT_14]
        [390] 520 (ContAmtValue) = 56344, [391] 521 (ContAmtCurr) = 46066
        [392] 519 (ContAmtType) = 11[FUND_BASED_RENEWAL_COMMISSION], [393] 520 (ContAmtValue) = 94762
        [394] 521 (ContAmtCurr) = 6779, [395] 555 (NoLegs) = 3
         */

        [Test]
        public void ContAmtGrp_Structure_Test()
        {
            var structure = _views[0].Structure;
            var msg = structure?.Msg();
            Assert.That(msg, Is.Not.Null);
            var contAmtGrp = structure?.GetInstance("ContAmtGrp");
            Assert.Multiple(() =>
            {
                Assert.That(contAmtGrp, Is.Not.Null);
                Assert.That(contAmtGrp.StartPosition, Is.EqualTo(385));
                Assert.That(contAmtGrp.StartTag, Is.EqualTo(518));
                Assert.That(contAmtGrp.EndPosition, Is.EqualTo(394));
                Assert.That(contAmtGrp.Depth, Is.EqualTo(1));
                Assert.That(contAmtGrp.EndTag, Is.EqualTo(521));
                Assert.That(contAmtGrp.Type, Is.EqualTo(SegmentType.Component));
            });

            var noContAmts = structure?.GetInstance("NoContAmts");
            Assert.Multiple(() =>
            {
                Assert.That(noContAmts, Is.Not.Null);
                Assert.That(noContAmts.StartPosition, Is.EqualTo(385));
                Assert.That(noContAmts.StartTag, Is.EqualTo(518));
                Assert.That(noContAmts.EndPosition, Is.EqualTo(394));
                Assert.That(noContAmts.Depth, Is.EqualTo(2));
                Assert.That(noContAmts.EndTag, Is.EqualTo(521));
                Assert.That(noContAmts.DelimiterTag, Is.EqualTo(519));
                Assert.That(noContAmts.DelimiterPositions, Is.EqualTo(new List<int>{ 386, 389, 392 }));
                Assert.That(noContAmts.Type, Is.EqualTo(SegmentType.Group));
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
