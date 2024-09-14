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
using System.Text.Json;
using DIs = NUnit.DeepObjectCompare.Is;
using static PureFix.Dictionary.Compiler.MsgCompiler;
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
            var ptysSubGrp = structure.Value.GetInstances("PtysSubGrp");
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

                var noPartySubIDs = structure.Value.GetInstances("NoPartySubIDs");
                Assert.That(noPartySubIDs, Is.Not.Null);
                Assert.That(noPartySubIDs, Has.Count.EqualTo(3));

                Assert.That(noPartySubIDs[0].DelimiterTag, Is.EqualTo(523));
                Assert.That(noPartySubIDs[0].Type, Is.EqualTo(SegmentType.Group));
                Assert.That(noPartySubIDs[0].DelimiterPositions, Is.EqualTo(new List<int>([25, 27])));
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
            var contraGrp = structure.Value.GetInstance("ContraGrp");
            Assert.Multiple(() =>
            {
                Assert.That(contraGrp, Is.Not.Null);
                Assert.That(contraGrp.StartPosition, Is.EqualTo(46));
                Assert.That(contraGrp.EndPosition, Is.EqualTo(61));
                Assert.That(contraGrp.Depth, Is.EqualTo(1));
                Assert.That(contraGrp.Type, Is.EqualTo(SegmentType.Component));

                var noContraBrokers = structure.Value.GetInstance("NoContraBrokers");
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
            var instrument = structure.Value.GetInstance("Instrument");
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
            var financingDetails = structure.Value.GetInstance("FinancingDetails");
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
            var pegInstructions = structure.Value.GetInstance("PegInstructions");
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
            var discretionInstructions = structure.Value.GetInstance("DiscretionInstructions");
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
            var commisionData = structure.Value.GetInstance("CommissionData");
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
                Assert.That(noContAmts.DelimiterPositions, Is.EqualTo(new List<int> { 386, 389, 392 }));
                Assert.That(noContAmts.Type, Is.EqualTo(SegmentType.Group));
            });
        }

        /*
[636] 136 (NoMiscFees) = 2, [637] 137 (MiscFeeAmt) = 56059
[638] 138 (MiscFeeCurr) = 92115, [639] 139 (MiscFeeType) = 7[OTHER]
[640] 891 (MiscFeeBasis) = [undefined], [641] 137 (MiscFeeAmt) = 93185
[642] 138 (MiscFeeCurr) = 72195, [643] 139 (MiscFeeType) = 12[AGENT]
[644] 891 (MiscFeeBasis) = [undefined], [645] 10 (CheckSum) = 59
 */

        [Test]
        public void MiscFeesGrp_Structure_Test()
        {
            var structure = _views[0].Structure;
            var msg = structure?.Msg();
            Assert.That(msg, Is.Not.Null);
            var contAmtGrp = structure.Value.GetInstance("MiscFeesGrp");
            Assert.Multiple(() =>
            {
                Assert.That(contAmtGrp, Is.Not.Null);
                Assert.That(contAmtGrp.StartPosition, Is.EqualTo(636));
                Assert.That(contAmtGrp.StartTag, Is.EqualTo(136));
                Assert.That(contAmtGrp.EndPosition, Is.EqualTo(644));
                Assert.That(contAmtGrp.Depth, Is.EqualTo(1));
                Assert.That(contAmtGrp.EndTag, Is.EqualTo(891));
                Assert.That(contAmtGrp.Type, Is.EqualTo(SegmentType.Component));
            });

            var noContAmts = structure?.GetInstance("NoMiscFees");
            Assert.Multiple(() =>
            {
                Assert.That(noContAmts, Is.Not.Null);
                Assert.That(noContAmts.StartPosition, Is.EqualTo(636));
                Assert.That(noContAmts.StartTag, Is.EqualTo(136));
                Assert.That(noContAmts.EndPosition, Is.EqualTo(644));
                Assert.That(noContAmts.Depth, Is.EqualTo(2));
                Assert.That(noContAmts.EndTag, Is.EqualTo(891));
                Assert.That(noContAmts.DelimiterTag, Is.EqualTo(137));
                Assert.That(noContAmts.DelimiterPositions, Is.EqualTo(new List<int> { 637, 641 }));
                Assert.That(noContAmts.Type, Is.EqualTo(SegmentType.Group));
            });
        }

        /*
[142] 898 (MarginRatio) = 13625, [143] 711 (NoUnderlyings) = 2
[144] 311 (UnderlyingSymbol) = massa., [145] 312 (UnderlyingSymbolSfx) = metus
[146] 309 (UnderlyingSecurityID) = maximus, [147] 305 (UnderlyingSecurityIDSource) = facilisis
[148] 457 (NoUnderlyingSecurityAltID) = 3, [149] 458 (UnderlyingSecurityAltID) = ornare
[150] 459 (UnderlyingSecurityAltIDSource) = magna., [151] 458 (UnderlyingSecurityAltID) = non
[152] 459 (UnderlyingSecurityAltIDSource) = at, [153] 458 (UnderlyingSecurityAltID) = hendrerit
[154] 459 (UnderlyingSecurityAltIDSource) = Pellentesque, [155] 462 (UnderlyingProduct) = 89682
[156] 463 (UnderlyingCFICode) = arcu,, [157] 310 (UnderlyingSecurityType) = eu
[158] 763 (UnderlyingSecuritySubType) = vitae,, [159] 313 (UnderlyingMaturityMonthYear) = ut
[160] 542 (UnderlyingMaturityDate) = 20180528-16:38:03.973, [161] 315 (UnderlyingPutOrCall) = 81619
[162] 241 (UnderlyingCouponPaymentDate) = 20180528-16:38:03.973, [163] 242 (UnderlyingIssueDate) = 20180528-16:38:03.973
[164] 243 (UnderlyingRepoCollateralSecurityType) = Aliquam, [165] 244 (UnderlyingRepurchaseTerm) = 1819
[166] 245 (UnderlyingRepurchaseRate) = 12004, [167] 246 (UnderlyingFactor) = 81916
[168] 256 (UnderlyingCreditRating) = erat, [169] 595 (UnderlyingInstrRegistry) = tellus
[170] 592 (UnderlyingCountryOfIssue) = in, [171] 593 (UnderlyingStateOrProvinceOfIssue) = nisi
[172] 594 (UnderlyingLocaleOfIssue) = Interdum, [173] 247 (UnderlyingRedemptionDate) = 20180528-16:38:03.973
[174] 316 (UnderlyingStrikePrice) = 46328, [175] 941 (UnderlyingStrikeCurrency) = 21536
[176] 317 (UnderlyingOptAttribute) = malesuada, [177] 436 (UnderlyingContractMultiplier) = 83404
[178] 435 (UnderlyingCouponRate) = 1709, [179] 308 (UnderlyingSecurityExchange) = ac
[180] 306 (UnderlyingIssuer) = ipsum, [181] 362 (EncodedUnderlyingIssuerLen) = 20
[182] 363 (EncodedUnderlyingIssuer) = gJ40LPqdkNUGQxMisPLk, [183] 307 (UnderlyingSecurityDesc) = in
[184] 364 (EncodedUnderlyingSecurityDescLen) = 20, [185] 365 (EncodedUnderlyingSecurityDesc) = RUQjbmg6gsPoWBsuwDCh
[186] 877 (UnderlyingCPProgram) = Ut, [187] 878 (UnderlyingCPRegType) = massa
[188] 318 (UnderlyingCurrency) = 47704, [189] 879 (UnderlyingQty) = 31703
[190] 810 (UnderlyingPx) = 60977, [191] 882 (UnderlyingDirtyPrice) = 67361
[192] 883 (UnderlyingEndPrice) = 51785, [193] 884 (UnderlyingStartValue) = 80625
[194] 885 (UnderlyingCurrentValue) = 29888, [195] 886 (UnderlyingEndValue) = 49266
[196] 887 (NoUnderlyingStips) = 3, [197] 888 (UnderlyingStipType) = cursus
[198] 889 (UnderlyingStipValue) = Vivamus, [199] 888 (UnderlyingStipType) = convallis
[200] 889 (UnderlyingStipValue) = nec, [201] 888 (UnderlyingStipType) = urna
[202] 889 (UnderlyingStipValue) = vitae., [203] 311 (UnderlyingSymbol) = erat
[204] 312 (UnderlyingSymbolSfx) = In, [205] 309 (UnderlyingSecurityID) = feugiat
[206] 305 (UnderlyingSecurityIDSource) = ut, [207] 457 (NoUnderlyingSecurityAltID) = 1
[208] 458 (UnderlyingSecurityAltID) = Quisque, [209] 459 (UnderlyingSecurityAltIDSource) = tortor
[210] 462 (UnderlyingProduct) = 36068, [211] 463 (UnderlyingCFICode) = est
[212] 310 (UnderlyingSecurityType) = Lorem, [213] 763 (UnderlyingSecuritySubType) = dolor
[214] 313 (UnderlyingMaturityMonthYear) = amet,, [215] 542 (UnderlyingMaturityDate) = 20180528-16:38:03.973
[216] 315 (UnderlyingPutOrCall) = 94032, [217] 241 (UnderlyingCouponPaymentDate) = 20180528-16:38:03.973
[218] 242 (UnderlyingIssueDate) = 20180528-16:38:03.973, [219] 243 (UnderlyingRepoCollateralSecurityType) = adipiscing
[220] 244 (UnderlyingRepurchaseTerm) = 17712, [221] 245 (UnderlyingRepurchaseRate) = 95092
[222] 246 (UnderlyingFactor) = 82914, [223] 256 (UnderlyingCreditRating) = Nunc
[224] 595 (UnderlyingInstrRegistry) = orci,, [225] 592 (UnderlyingCountryOfIssue) = vel
[226] 593 (UnderlyingStateOrProvinceOfIssue) = sed,, [227] 594 (UnderlyingLocaleOfIssue) = cursus
[228] 247 (UnderlyingRedemptionDate) = 20180528-16:38:03.973, [229] 316 (UnderlyingStrikePrice) = 84513
[230] 941 (UnderlyingStrikeCurrency) = 31556, [231] 317 (UnderlyingOptAttribute) = Aenean
[232] 436 (UnderlyingContractMultiplier) = 8879, [233] 435 (UnderlyingCouponRate) = 68005
[234] 308 (UnderlyingSecurityExchange) = diam, [235] 306 (UnderlyingIssuer) = Aenean
[236] 362 (EncodedUnderlyingIssuerLen) = 20, [237] 363 (EncodedUnderlyingIssuer) = VxRjId4eWuuNiBYgjNpp
[238] 307 (UnderlyingSecurityDesc) = viverra, [239] 364 (EncodedUnderlyingSecurityDescLen) = 20
[240] 365 (EncodedUnderlyingSecurityDesc) = fwlY5CmVswvapjFalVLb, [241] 877 (UnderlyingCPProgram) = non
[242] 878 (UnderlyingCPRegType) = neque., [243] 318 (UnderlyingCurrency) = 53806
[244] 879 (UnderlyingQty) = 26390, [245] 810 (UnderlyingPx) = 32442
[246] 882 (UnderlyingDirtyPrice) = 82617, [247] 883 (UnderlyingEndPrice) = 86326
[248] 884 (UnderlyingStartValue) = 34911, [249] 885 (UnderlyingCurrentValue) = 46867
[250] 886 (UnderlyingEndValue) = 94014, [251] 887 (NoUnderlyingStips) = 3
[252] 888 (UnderlyingStipType) = arcu, [253] 889 (UnderlyingStipValue) = dignissim
[254] 888 (UnderlyingStipType) = auctor, [255] 889 (UnderlyingStipValue) = maximus
[256] 888 (UnderlyingStipType) = quam., [257] 889 (UnderlyingStipValue) = varius
 */

        [Test]
        public void UndInstrmtGrp_Structure_Test()
        {
            var structure = _views[0].Structure;
            var msg = structure?.Msg();
            Assert.That(msg, Is.Not.Null);
            var undInstrmtGrp = structure?.GetInstance("UndInstrmtGrp");
            Assert.Multiple(() =>
            {
                Assert.That(undInstrmtGrp, Is.Not.Null);
                Assert.That(undInstrmtGrp.StartPosition, Is.EqualTo(143));
                Assert.That(undInstrmtGrp.StartTag, Is.EqualTo(711));
                Assert.That(undInstrmtGrp.EndPosition, Is.EqualTo(257));
                Assert.That(undInstrmtGrp.Depth, Is.EqualTo(1));
                Assert.That(undInstrmtGrp.EndTag, Is.EqualTo(889));
                Assert.That(undInstrmtGrp.Type, Is.EqualTo(SegmentType.Component));
            });

            var noUnderlyings = structure?.GetInstance("NoUnderlyings");
            Assert.Multiple(() =>
            {
                Assert.That(noUnderlyings, Is.Not.Null);
                Assert.That(noUnderlyings.Depth, Is.EqualTo(2));
                Assert.That(noUnderlyings.DelimiterTag, Is.EqualTo(311));
                Assert.That(noUnderlyings.DelimiterPositions, Is.EqualTo(new List<int> { 144, 203 }));
                Assert.That(noUnderlyings.Type, Is.EqualTo(SegmentType.Group));
            });

            var underlyingInstrument = structure?.GetInstances("UnderlyingInstrument");
            Assert.That(underlyingInstrument, Is.Not.Null);
            Assert.That(underlyingInstrument, Has.Count.EqualTo(2));

            Assert.Multiple(() =>
            {
                Assert.That(underlyingInstrument, Is.Not.Null);
                Assert.That(underlyingInstrument[0].StartPosition, Is.EqualTo(144));
                Assert.That(underlyingInstrument[0].StartTag, Is.EqualTo(311));
                Assert.That(underlyingInstrument[0].EndPosition, Is.EqualTo(202));
                Assert.That(underlyingInstrument[0].Depth, Is.EqualTo(3));
                Assert.That(underlyingInstrument[0].EndTag, Is.EqualTo(889));
                Assert.That(underlyingInstrument[0].Type, Is.EqualTo(SegmentType.Component));
            });

            Assert.Multiple(() =>
            {
                Assert.That(underlyingInstrument, Is.Not.Null);
                Assert.That(underlyingInstrument[1].StartPosition, Is.EqualTo(203));
                Assert.That(underlyingInstrument[1].StartTag, Is.EqualTo(311));
                Assert.That(underlyingInstrument[1].EndPosition, Is.EqualTo(257));
                Assert.That(underlyingInstrument[1].Depth, Is.EqualTo(3));
                Assert.That(underlyingInstrument[1].EndTag, Is.EqualTo(889));
                Assert.That(underlyingInstrument[1].Type, Is.EqualTo(SegmentType.Component));
            });

            var noUnderlyingSecurityAltID = structure?.GetInstances("NoUnderlyingSecurityAltID");
            Assert.That(noUnderlyingSecurityAltID, Is.Not.Null);
            Assert.That(noUnderlyingSecurityAltID, Has.Count.EqualTo(2));

            Assert.Multiple(() =>
            {
                Assert.That(noUnderlyingSecurityAltID[0], Is.Not.Null);
                Assert.That(noUnderlyingSecurityAltID[0].Depth, Is.EqualTo(5));
                Assert.That(noUnderlyingSecurityAltID[0].DelimiterTag, Is.EqualTo(458));
                Assert.That(noUnderlyingSecurityAltID[0].Type, Is.EqualTo(SegmentType.Group));
                Assert.That(noUnderlyingSecurityAltID[0].StartPosition, Is.EqualTo(148));
                Assert.That(noUnderlyingSecurityAltID[0].EndPosition, Is.EqualTo(154));
                Assert.That(noUnderlyingSecurityAltID[0].DelimiterPositions,
                    Is.EqualTo(new List<int> { 149, 151, 153 }));
            });

            Assert.Multiple(() =>
            {
                Assert.That(noUnderlyingSecurityAltID[1], Is.Not.Null);
                Assert.That(noUnderlyingSecurityAltID[1].Depth, Is.EqualTo(5));
                Assert.That(noUnderlyingSecurityAltID[1].DelimiterTag, Is.EqualTo(458));
                Assert.That(noUnderlyingSecurityAltID[1].Type, Is.EqualTo(SegmentType.Group));
                Assert.That(noUnderlyingSecurityAltID[1].StartPosition, Is.EqualTo(207));
                Assert.That(noUnderlyingSecurityAltID[1].EndPosition, Is.EqualTo(209));
                Assert.That(noUnderlyingSecurityAltID[1].DelimiterPositions, Is.EqualTo(new List<int> { 208 }));
            });

            var boundNoUnderlyingSecurityAltID = structure?.FirstContainedWithin(
                "NoUnderlyingSecurityAltID",
                underlyingInstrument[1]);
            Assert.That(boundNoUnderlyingSecurityAltID, Is.Not.Null);
        }

        /*
[394] 521 (ContAmtCurr) = 6779, [395] 555 (NoLegs) = 3
[396] 600 (LegSymbol) = posuere, [397] 601 (LegSymbolSfx) = nibh.
[398] 602 (LegSecurityID) = ornare,, [399] 603 (LegSecurityIDSource) = semper
[400] 604 (NoLegSecurityAltID) = 3, [401] 605 (LegSecurityAltID) = laoreet,
[402] 606 (LegSecurityAltIDSource) = odio, [403] 605 (LegSecurityAltID) = velit,
[404] 606 (LegSecurityAltIDSource) = sollicitudin, [405] 605 (LegSecurityAltID) = augue
[406] 606 (LegSecurityAltIDSource) = odio., [407] 607 (LegProduct) = 12438
[408] 608 (LegCFICode) = tempor, [409] 609 (LegSecurityType) = nisi,
[410] 764 (LegSecuritySubType) = euismod, [411] 610 (LegMaturityMonthYear) = convallis
[412] 611 (LegMaturityDate) = 20180528-16:38:03.973, [413] 248 (LegCouponPaymentDate) = 20180528-16:38:03.973
[414] 249 (LegIssueDate) = 20180528-16:38:03.973, [415] 250 (LegRepoCollateralSecurityType) = Donec
[416] 251 (LegRepurchaseTerm) = 50636, [417] 252 (LegRepurchaseRate) = 29567
[418] 253 (LegFactor) = 12603, [419] 257 (LegCreditRating) = erat
[420] 599 (LegInstrRegistry) = vel, [421] 596 (LegCountryOfIssue) = nulla
[422] 597 (LegStateOrProvinceOfIssue) = ac., [423] 598 (LegLocaleOfIssue) = vel
[424] 254 (LegRedemptionDate) = 20180528-16:38:03.973, [425] 612 (LegStrikePrice) = 45964
[426] 942 (LegStrikeCurrency) = 52294, [427] 613 (LegOptAttribute) = eros.
[428] 614 (LegContractMultiplier) = 15566, [429] 615 (LegCouponRate) = 29897
[430] 616 (LegSecurityExchange) = urna,, [431] 617 (LegIssuer) = sit
[432] 618 (EncodedLegIssuerLen) = 20, [433] 619 (EncodedLegIssuer) = bsCdR5NlOBXt99NrTSbA
[434] 620 (LegSecurityDesc) = bibendum, [435] 621 (EncodedLegSecurityDescLen) = 20
[436] 622 (EncodedLegSecurityDesc) = 3Mzh9ClDlcxCLLtgAZ0x, [437] 623 (LegRatioQty) = 47570
[438] 624 (LegSide) = amet,, [439] 556 (LegCurrency) = 18817
[440] 740 (LegPool) = non, [441] 739 (LegDatedDate) = 20180528-16:38:03.973
[442] 955 (LegContractSettlMonth) = Nam, [443] 956 (LegInterestAccrualDate) = 20180528-16:38:03.973
[444] 687 (LegQty) = 38413, [445] 690 (LegSwapType) = 4[RISK]
[446] 683 (NoLegStipulations) = 2, [447] 688 (LegStipulationType) = mi,
[448] 689 (LegStipulationValue) = et, [449] 688 (LegStipulationType) = in,
[450] 689 (LegStipulationValue) = sed, [451] 564 (LegPositionEffect) = Nulla
[452] 565 (LegCoveredOrUncovered) = 45098, [453] 539 (NoNestedPartyIDs) = 1
[454] 524 (NestedPartyID) = leo, [455] 525 (NestedPartyIDSource) = odio
[456] 538 (NestedPartyRole) = 59224, [457] 804 (NoNestedPartySubIDs) = 1
[458] 545 (NestedPartySubID) = placerat., [459] 805 (NestedPartySubIDType) = 83297
[460] 654 (LegRefID) = felis, [461] 566 (LegPrice) = 39429
[462] 587 (LegSettlType) = elementum, [463] 588 (LegSettlDate) = 20180528-16:38:03.973
[464] 637 (LegLastPx) = 75387, [465] 600 (LegSymbol) = mollis
[466] 601 (LegSymbolSfx) = euismod, [467] 602 (LegSecurityID) = diam.
[468] 603 (LegSecurityIDSource) = porttitor, [469] 604 (NoLegSecurityAltID) = 3
[470] 605 (LegSecurityAltID) = eget, [471] 606 (LegSecurityAltIDSource) = cursus,
[472] 605 (LegSecurityAltID) = hendrerit, [473] 606 (LegSecurityAltIDSource) = tempus.
[474] 605 (LegSecurityAltID) = et, [475] 606 (LegSecurityAltIDSource) = fames
[476] 607 (LegProduct) = 6990, [477] 608 (LegCFICode) = ante
[478] 609 (LegSecurityType) = primis, [479] 764 (LegSecuritySubType) = faucibus.
[480] 610 (LegMaturityMonthYear) = egestas, [481] 611 (LegMaturityDate) = 20180528-16:38:03.973
[482] 248 (LegCouponPaymentDate) = 20180528-16:38:03.973, [483] 249 (LegIssueDate) = 20180528-16:38:03.973
[484] 250 (LegRepoCollateralSecurityType) = ac, [485] 251 (LegRepurchaseTerm) = 58397
[486] 252 (LegRepurchaseRate) = 22917, [487] 253 (LegFactor) = 96132
[488] 257 (LegCreditRating) = semper., [489] 599 (LegInstrRegistry) = rhoncus
[490] 596 (LegCountryOfIssue) = nulla,, [491] 597 (LegStateOrProvinceOfIssue) = ornare
[492] 598 (LegLocaleOfIssue) = iaculis, [493] 254 (LegRedemptionDate) = 20180528-16:38:03.973
[494] 612 (LegStrikePrice) = 86522, [495] 942 (LegStrikeCurrency) = 3727
[496] 613 (LegOptAttribute) = Aliquam, [497] 614 (LegContractMultiplier) = 18485
[498] 615 (LegCouponRate) = 36169, [499] 616 (LegSecurityExchange) = volutpat.
[500] 617 (LegIssuer) = condimentum, [501] 618 (EncodedLegIssuerLen) = 20
[502] 619 (EncodedLegIssuer) = P813lip75NKBgG1C8re8, [503] 620 (LegSecurityDesc) = sem
[504] 621 (EncodedLegSecurityDescLen) = 20, [505] 622 (EncodedLegSecurityDesc) = 8qbetIHVFcKFS7EY3XOK
[506] 623 (LegRatioQty) = 42919, [507] 624 (LegSide) = porttitor.
[508] 556 (LegCurrency) = 34832, [509] 740 (LegPool) = pellentesque
[510] 739 (LegDatedDate) = 20180528-16:38:03.973, [511] 955 (LegContractSettlMonth) = eget
[512] 956 (LegInterestAccrualDate) = 20180528-16:38:03.973, [513] 687 (LegQty) = 16560
[514] 690 (LegSwapType) = 1[PAR_FOR_PAR], [515] 683 (NoLegStipulations) = 3
[516] 688 (LegStipulationType) = tincidunt, [517] 689 (LegStipulationValue) = ipsum
[518] 688 (LegStipulationType) = sit, [519] 689 (LegStipulationValue) = consectetur
[520] 688 (LegStipulationType) = elit., [521] 689 (LegStipulationValue) = odio
[522] 564 (LegPositionEffect) = blandit, [523] 565 (LegCoveredOrUncovered) = 61021
[524] 539 (NoNestedPartyIDs) = 3, [525] 524 (NestedPartyID) = semper
[526] 525 (NestedPartyIDSource) = bibendum, [527] 538 (NestedPartyRole) = 33332
[528] 804 (NoNestedPartySubIDs) = 1, [529] 545 (NestedPartySubID) = lectus.
[530] 805 (NestedPartySubIDType) = 78292, [531] 524 (NestedPartyID) = vel
[532] 525 (NestedPartyIDSource) = magna., [533] 538 (NestedPartyRole) = 8563
[534] 804 (NoNestedPartySubIDs) = 3, [535] 545 (NestedPartySubID) = et
[536] 805 (NestedPartySubIDType) = 91784, [537] 545 (NestedPartySubID) = leo,
[538] 805 (NestedPartySubIDType) = 69741, [539] 545 (NestedPartySubID) = iaculis
[540] 805 (NestedPartySubIDType) = 88660, [541] 524 (NestedPartyID) = Nullam
[542] 525 (NestedPartyIDSource) = lectus,, [543] 538 (NestedPartyRole) = 92219
[544] 804 (NoNestedPartySubIDs) = 2, [545] 545 (NestedPartySubID) = eget
[546] 805 (NestedPartySubIDType) = 61048, [547] 545 (NestedPartySubID) = vitae,
[548] 805 (NestedPartySubIDType) = 82229, [549] 654 (LegRefID) = ac
[550] 566 (LegPrice) = 87104, [551] 587 (LegSettlType) = Quisque
[552] 588 (LegSettlDate) = 20180528-16:38:03.974, [553] 637 (LegLastPx) = 56182
[554] 600 (LegSymbol) = tincidunt, [555] 601 (LegSymbolSfx) = Class
[556] 602 (LegSecurityID) = taciti, [557] 603 (LegSecurityIDSource) = ad
[558] 604 (NoLegSecurityAltID) = 3, [559] 605 (LegSecurityAltID) = torquent
[560] 606 (LegSecurityAltIDSource) = conubia, [561] 605 (LegSecurityAltID) = per
[562] 606 (LegSecurityAltIDSource) = himenaeos., [563] 605 (LegSecurityAltID) = eu
[564] 606 (LegSecurityAltIDSource) = risus,, [565] 607 (LegProduct) = 36203
[566] 608 (LegCFICode) = tincidunt, [567] 609 (LegSecurityType) = Morbi
[568] 764 (LegSecuritySubType) = mi,, [569] 610 (LegMaturityMonthYear) = vel
[570] 611 (LegMaturityDate) = 20180528-16:38:03.974, [571] 248 (LegCouponPaymentDate) = 20180528-16:38:03.974
[572] 249 (LegIssueDate) = 20180528-16:38:03.974, [573] 250 (LegRepoCollateralSecurityType) = ac,
[574] 251 (LegRepurchaseTerm) = 89976, [575] 252 (LegRepurchaseRate) = 16951
[576] 253 (LegFactor) = 28912, [577] 257 (LegCreditRating) = non
[578] 599 (LegInstrRegistry) = Pellentesque, [579] 596 (LegCountryOfIssue) = lorem
[580] 597 (LegStateOrProvinceOfIssue) = consequat, [581] 598 (LegLocaleOfIssue) = sapien
[582] 254 (LegRedemptionDate) = 20180528-16:38:03.974, [583] 612 (LegStrikePrice) = 97965
[584] 942 (LegStrikeCurrency) = 62977, [585] 613 (LegOptAttribute) = tempor
[586] 614 (LegContractMultiplier) = 4694, [587] 615 (LegCouponRate) = 20263
[588] 616 (LegSecurityExchange) = a, [589] 617 (LegIssuer) = purus
[590] 618 (EncodedLegIssuerLen) = 20, [591] 619 (EncodedLegIssuer) = Z2GKqFkA713xSVG2nqOx
[592] 620 (LegSecurityDesc) = ut, [593] 621 (EncodedLegSecurityDescLen) = 20
[594] 622 (EncodedLegSecurityDesc) = lyQLPxezPpjDNJnvSnCo, [595] 623 (LegRatioQty) = 24444
[596] 624 (LegSide) = Proin, [597] 556 (LegCurrency) = 86893
[598] 740 (LegPool) = justo, [599] 739 (LegDatedDate) = 20180528-16:38:03.974
[600] 955 (LegContractSettlMonth) = ut, [601] 956 (LegInterestAccrualDate) = 20180528-16:38:03.974
[602] 687 (LegQty) = 33589, [603] 690 (LegSwapType) = 4[RISK]
[604] 683 (NoLegStipulations) = 2, [605] 688 (LegStipulationType) = nibh
[606] 689 (LegStipulationValue) = at., [607] 688 (LegStipulationType) = fermentum
[608] 689 (LegStipulationValue) = risus,, [609] 564 (LegPositionEffect) = placerat
[610] 565 (LegCoveredOrUncovered) = 74238, [611] 539 (NoNestedPartyIDs) = 2
[612] 524 (NestedPartyID) = luctus, [613] 525 (NestedPartyIDSource) = Vivamus
[614] 538 (NestedPartyRole) = 41104, [615] 804 (NoNestedPartySubIDs) = 3
[616] 545 (NestedPartySubID) = mi, [617] 805 (NestedPartySubIDType) = 26305
[618] 545 (NestedPartySubID) = arcu, [619] 805 (NestedPartySubIDType) = 97537
[620] 545 (NestedPartySubID) = rhoncus, [621] 805 (NestedPartySubIDType) = 71231
[622] 524 (NestedPartyID) = amet, [623] 525 (NestedPartyIDSource) = sit
[624] 538 (NestedPartyRole) = 66844, [625] 804 (NoNestedPartySubIDs) = 2
[626] 545 (NestedPartySubID) = eleifend, [627] 805 (NestedPartySubIDType) = 8186
[628] 545 (NestedPartySubID) = massa., [629] 805 (NestedPartySubIDType) = 82689
[630] 654 (LegRefID) = metus, [631] 566 (LegPrice) = 6725
[632] 587 (LegSettlType) = maximus, [633] 588 (LegSettlDate) = 20180528-16:38:03.974
[634] 637 (LegLastPx) = 88665, [635] 797 (CopyMsgIndicator) = Y
 */

        [Test]
        public void InstrmtLegExecGrp_Structure_Test()
        {
            var structure = _views[0].Structure;
            var msg = structure?.Msg();
            Assert.That(msg, Is.Not.Null);
            var instrmtLegExecGrp = structure?.GetInstance("InstrmtLegExecGrp");
            Assert.That(instrmtLegExecGrp, Is.Not.Null);

            Assert.Multiple(() =>
            {
                Assert.That(instrmtLegExecGrp, Is.Not.Null);
                Assert.That(instrmtLegExecGrp.Type, Is.EqualTo(SegmentType.Component));
                Assert.That(instrmtLegExecGrp.StartPosition, Is.EqualTo(395));
                Assert.That(instrmtLegExecGrp.StartTag, Is.EqualTo(555));
                Assert.That(instrmtLegExecGrp.EndTag, Is.EqualTo(637));
                Assert.That(instrmtLegExecGrp.EndPosition, Is.EqualTo(634));
                Assert.That(instrmtLegExecGrp.Depth, Is.EqualTo(1));
            });

            var noLegs = structure?.GetInstance("NoLegs");
            Assert.Multiple(() =>
            {
                Assert.That(noLegs, Is.Not.Null);
                Assert.That(noLegs.Type, Is.EqualTo(SegmentType.Group));
                Assert.That(noLegs.StartPosition, Is.EqualTo(395));
                Assert.That(noLegs.StartTag, Is.EqualTo(555));
                Assert.That(noLegs.EndTag, Is.EqualTo(637));
                Assert.That(noLegs.EndPosition, Is.EqualTo(634));
                Assert.That(noLegs.Depth, Is.EqualTo(2));
                Assert.That(noLegs.DelimiterTag, Is.EqualTo(600));
                Assert.That(noLegs.DelimiterPositions, Is.EqualTo(new List<int> { 396, 465, 554 }));
            });
        }


        [Test]
        public void InstrmtLeg_Structure_Test()
        {
            var structure = _views[0].Structure;
            var msg = structure?.Msg();
            Assert.That(msg, Is.Not.Null);
            var instrumentLeg = structure?.GetInstances("InstrumentLeg");
            Assert.That(instrumentLeg, Is.Not.Null);
            Assert.That(instrumentLeg, Has.Count.EqualTo(3));

            Assert.Multiple(() =>
            {
                Assert.That(instrumentLeg[0], Is.Not.Null);
                Assert.That(instrumentLeg[0].Type, Is.EqualTo(SegmentType.Component));
                Assert.That(instrumentLeg[0].Depth, Is.EqualTo(3));
                Assert.That(instrumentLeg[0].StartTag, Is.EqualTo(600));
                Assert.That(instrumentLeg[0].StartPosition, Is.EqualTo(396));
                Assert.That(instrumentLeg[0].EndPosition, Is.EqualTo(443));
                Assert.That(instrumentLeg[0].EndTag, Is.EqualTo(956));
            });


            Assert.Multiple(() =>
            {
                Assert.That(instrumentLeg[1], Is.Not.Null);
                Assert.That(instrumentLeg[1].Type, Is.EqualTo(SegmentType.Component));
                Assert.That(instrumentLeg[1].Depth, Is.EqualTo(3));
                Assert.That(instrumentLeg[1].StartTag, Is.EqualTo(600));
                Assert.That(instrumentLeg[1].StartPosition, Is.EqualTo(465));
                Assert.That(instrumentLeg[1].EndPosition, Is.EqualTo(512));
                Assert.That(instrumentLeg[1].EndTag, Is.EqualTo(956));
            });

            Assert.Multiple(() =>
            {
                Assert.That(instrumentLeg[2], Is.Not.Null);
                Assert.That(instrumentLeg[2].Type, Is.EqualTo(SegmentType.Component));
                Assert.That(instrumentLeg[2].Depth, Is.EqualTo(3));
                Assert.That(instrumentLeg[2].StartTag, Is.EqualTo(600));
                Assert.That(instrumentLeg[2].StartPosition, Is.EqualTo(554));
                Assert.That(instrumentLeg[2].EndPosition, Is.EqualTo(601));
                Assert.That(instrumentLeg[2].EndTag, Is.EqualTo(956));
            });
        }

        private SegmentDescription GetNoLegSecurityAtlID(int index)
        {
            var structure = _views[0].Structure;
            var msg = structure?.Msg();
            Assert.That(msg, Is.Not.Null);
            var noLegSecurityAltID = structure?.GetInstances("NoLegSecurityAltID");
            Assert.That(noLegSecurityAltID, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(noLegSecurityAltID, Is.Not.Null);
                Assert.That(noLegSecurityAltID, Has.Count.EqualTo(3));
                Assert.That(noLegSecurityAltID[index].Type, Is.EqualTo(SegmentType.Group));
                Assert.That(noLegSecurityAltID[index].Depth, Is.EqualTo(5));
                Assert.That(noLegSecurityAltID[index].StartTag, Is.EqualTo(604));
            });

            return noLegSecurityAltID[index];
        }

        [Test]
        public void LegSecAltIDGrp_0_Structure_Test()
        {
            var structure = _views[0].Structure;
            var msg = structure?.Msg();
            Assert.That(msg, Is.Not.Null);
            var legSecAltIDGrp = structure?.GetInstances("LegSecAltIDGrp");
            Assert.That(legSecAltIDGrp, Is.Not.Null);
            Assert.That(legSecAltIDGrp, Has.Count.EqualTo(3));

            Assert.Multiple(() =>
            {
                Assert.That(legSecAltIDGrp[0], Is.Not.Null);
                Assert.That(legSecAltIDGrp[0].Type, Is.EqualTo(SegmentType.Component));
                Assert.That(legSecAltIDGrp[0].Depth, Is.EqualTo(4));
                Assert.That(legSecAltIDGrp[0].StartTag, Is.EqualTo(604));
                Assert.That(legSecAltIDGrp[0].StartPosition, Is.EqualTo(400));
                Assert.That(legSecAltIDGrp[0].EndPosition, Is.EqualTo(406));
                Assert.That(legSecAltIDGrp[0].EndTag, Is.EqualTo(606));
            });

            var noLegSecurityAltID = GetNoLegSecurityAtlID(0);
            Assert.Multiple(() =>
            {
                Assert.That(noLegSecurityAltID, Is.Not.Null);
                Assert.That(noLegSecurityAltID.StartPosition, Is.EqualTo(400));
                Assert.That(noLegSecurityAltID.EndPosition, Is.EqualTo(406));
                Assert.That(noLegSecurityAltID.EndTag, Is.EqualTo(606));
                Assert.That(noLegSecurityAltID.DelimiterTag, Is.EqualTo(605));
                Assert.That(noLegSecurityAltID.DelimiterPositions, Is.EqualTo(new List<int> { 401, 403, 405 }));
            });
        }

        [Test]
        public void LegSecAltIDGrp_1_Structure_Test()
        {
            var structure = _views[0].Structure;
            var msg = structure?.Msg();
            Assert.That(msg, Is.Not.Null);
            var legSecAltIDGrp = structure?.GetInstances("LegSecAltIDGrp");
            Assert.That(legSecAltIDGrp, Is.Not.Null);
            Assert.That(legSecAltIDGrp, Has.Count.EqualTo(3));

            Assert.Multiple(() =>
            {
                Assert.That(legSecAltIDGrp[1], Is.Not.Null);
                Assert.That(legSecAltIDGrp[1].Type, Is.EqualTo(SegmentType.Component));
                Assert.That(legSecAltIDGrp[1].Depth, Is.EqualTo(4));
                Assert.That(legSecAltIDGrp[1].StartTag, Is.EqualTo(604));
                Assert.That(legSecAltIDGrp[1].StartPosition, Is.EqualTo(469));
                Assert.That(legSecAltIDGrp[1].EndPosition, Is.EqualTo(475));
                Assert.That(legSecAltIDGrp[1].EndTag, Is.EqualTo(606));
            });

            var noLegSecurityAltID = GetNoLegSecurityAtlID(1);
            Assert.Multiple(() =>
            {
                Assert.That(noLegSecurityAltID, Is.Not.Null);
                Assert.That(noLegSecurityAltID.StartPosition, Is.EqualTo(469));
                Assert.That(noLegSecurityAltID.EndPosition, Is.EqualTo(475));
                Assert.That(noLegSecurityAltID.EndTag, Is.EqualTo(606));
                Assert.That(noLegSecurityAltID.DelimiterTag, Is.EqualTo(605));
                Assert.That(noLegSecurityAltID.DelimiterPositions, Is.EqualTo(new List<int> { 470, 472, 474 }));
            });
        }

        [Test]
        public void LegSecAltIDGrp_2_Structure_Test()
        {
            var structure = _views[0].Structure;
            var msg = structure?.Msg();
            Assert.That(msg, Is.Not.Null);
            var legSecAltIDGrp = structure?.GetInstances("LegSecAltIDGrp");
            Assert.That(legSecAltIDGrp, Is.Not.Null);
            Assert.That(legSecAltIDGrp, Has.Count.EqualTo(3));

            Assert.Multiple(() =>
            {
                Assert.That(legSecAltIDGrp[2], Is.Not.Null);
                Assert.That(legSecAltIDGrp[2].Type, Is.EqualTo(SegmentType.Component));
                Assert.That(legSecAltIDGrp[2].Depth, Is.EqualTo(4));
                Assert.That(legSecAltIDGrp[2].StartTag, Is.EqualTo(604));
                Assert.That(legSecAltIDGrp[2].StartPosition, Is.EqualTo(558));
                Assert.That(legSecAltIDGrp[2].EndPosition, Is.EqualTo(564));
                Assert.That(legSecAltIDGrp[2].EndTag, Is.EqualTo(606));
            });

            var noLegSecurityAltID = GetNoLegSecurityAtlID(2);
            Assert.Multiple(() =>
            {
                Assert.That(noLegSecurityAltID, Is.Not.Null);
                Assert.That(noLegSecurityAltID.StartPosition, Is.EqualTo(558));
                Assert.That(noLegSecurityAltID.EndPosition, Is.EqualTo(564));
                Assert.That(noLegSecurityAltID.EndTag, Is.EqualTo(606));
                Assert.That(noLegSecurityAltID.DelimiterTag, Is.EqualTo(605));
                Assert.That(noLegSecurityAltID.DelimiterPositions, Is.EqualTo(new List<int> { 559, 561, 563 }));
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
            Assert.That(er.StandardHeader, Is.Not.Null);
            Assert.That(er.StandardHeader.BeginString, Is.EqualTo("FIX4.4"));
            JsonSerializerOptions options = new(JsonSerializerDefaults.Web)
            {
                WriteIndented = true
            };
            string json = JsonSerializer.Serialize(er, options);
        }

        [Test]
        public void View_Execution_Report_Tag_Decode_Test()
        {
            Assert.That(_views, Is.Not.Null);
            Assert.That(_views, Has.Count.EqualTo(1));
            var erView = _views[0];
            Assert.That(erView, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(erView.GetString(35), Is.EqualTo("8"));
                Assert.That(erView.GetString("MsgType"), Is.EqualTo("8"));
                Assert.That(erView.GetString(8), Is.EqualTo("FIX4.4"));
                Assert.That(erView.GetInt32(9), Is.EqualTo(6545));
                Assert.That(erView.GetInt32("TotNumReports"), Is.EqualTo(19404));
                Assert.That(erView.GetInt32("StrikePrice"), Is.EqualTo(52639));
            });
        }

        [Test]
        public void View_Simple_Repeat_Tag_Decode_Test()
        {
            Assert.That(_views, Is.Not.Null);
            Assert.That(_views, Has.Count.EqualTo(1));
            var erView = _views[0];
            Assert.That(erView, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(erView.GetStrings("PartyID"), Is.EqualTo((string[])["magna.", "iaculis", "vitae,"]));
            });
        }

        T FromJson<T>(string s) {
            JsonSerializerOptions options2 = new()
            {
                PropertyNameCaseInsensitive = true
            };
            var instance = JsonSerializer.Deserialize<T>(s, options2);
            return instance;
        }

        string ToJson<T>(T instance)
        {
            JsonSerializerOptions options = new()
            {
                WriteIndented = true
            };
            var json = JsonSerializer.Serialize(instance, options);
            return json;
        }

        [Test]
        public void View_Parse_Parties_To_Type_Test()
        {
            Assert.That(_views, Is.Not.Null);
            Assert.That(_views, Has.Count.EqualTo(1));
            var erView = _views[0];
            Assert.That(erView, Is.Not.Null);
            var partyView = erView.GetView("Parties");
            Assert.That(partyView, Is.Not.Null);
            var parties = new Parties();
            parties.Parse(partyView);
            Assert.That(parties.NoPartyIDs, Is.Not.Null);
            var json = ToJson(parties.NoPartyIDs[0]);
            var expected = """
                           {
                             "partyID": "magna.",
                             "partyIDSource": "9",
                             "partyRole": 28,
                             "ptysSubGrp": {
                               "noPartySubIDs": [
                                 {
                                   "partySubID": "et",
                                   "partySubIDType": 22
                                 },
                                 {
                                   "partySubID": "leo,",
                                   "partySubIDType": 10
                                 }
                               ]
                             }
                           }
                           """;


            var instance = FromJson<PartiesNoPartyIDs>(expected);
            Assert.That(parties.NoPartyIDs[0], DIs.DeepEqualTo(instance));

            var noParties = partyView?.GetView("NoPartyIDs");
            var np0View = noParties?.GetGroupInstance(0);
            var np0ViewPtysSubGrp = np0View?.GetView("PtysSubGrp");
            var psg = new PtysSubGrp();
            psg.Parse(np0ViewPtysSubGrp);

            Assert.Multiple(() =>
            {
                Assert.That(noParties, Is.Not.Null);
                Assert.That(noParties.GroupCount(), Is.EqualTo(3));
                Assert.That(np0View, Is.Not.Null);
                Assert.That(np0View?.GetString("PartyID"), Is.EqualTo("magna."));
                Assert.That(np0View?.GetString("PartyIDSource"), Is.EqualTo("9"));
                Assert.That(np0ViewPtysSubGrp, Is.Not.Null);
                Assert.That(psg, DIs.DeepEqualTo(instance.PtysSubGrp));
            });
        }

        [Test]
        public void View_Instrument_Decode_Test()
        {
            Assert.That(_views, Is.Not.Null);
            Assert.That(_views, Has.Count.EqualTo(1));
            var erView = _views[0];
            Assert.That(erView, Is.Not.Null);
            // check the instrument component
            var instrumentView = erView.GetView("Instrument");
            Assert.That(instrumentView, Is.Not.Null);
            Assert.That(instrumentView.GetString("Symbol"), Is.EqualTo("ac,"));
            var secAltIDGrpAsObject = instrumentView?.GetView("SecAltIDGrp");
            var sag = new SecAltIDGrp();
            sag.Parse(secAltIDGrpAsObject);
            Assert.Multiple(() =>
            {
                Assert.That(sag.NoSecurityAltID, Is.Not.Null);
                Assert.That(sag.NoSecurityAltID.Length, Is.EqualTo(2));
            });
            var er = new ExecutionReport();
            er.Parse(erView);
            var json = ToJson(er);
            var expected = """
{
    "Symbol": "ac,",
    "SymbolSfx": "non",
    "SecurityID": "Pellentesque",
    "SecurityIDSource": "B",
    "SecAltIDGrp": {
      "NoSecurityAltID": [
        {
          "SecurityAltID": "lorem",
          "SecurityAltIDSource": "consequat"
        },
        {
          "SecurityAltID": "sapien",
          "SecurityAltIDSource": "tempor"
        }
      ]
    },
    "Product": 2,
    "CFICode": "a",
    "SecurityType": "SECLOAN",
    "SecuritySubType": "purus",
    "MaturityMonthYear": null,
    "MaturityDate": null,
    "PutOrCall": 1,
    "CouponPaymentDate": null,
    "IssueDate": null,
    "RepoCollateralSecurityType": "Proin",
    "RepurchaseTerm": 62025,
    "RepurchaseRate": 27005,
    "Factor": 68810,
    "CreditRating": "justo",
    "InstrRegistry": "ut",
    "CountryOfIssue": "nibh",
    "StateOrProvinceOfIssue": "at.",
    "LocaleOfIssue": "fermentum",
    "RedemptionDate": null,
    "StrikePrice": 52639,
    "StrikeCurrency": "magna.",
    "OptAttribute": "risus,",
    "ContractMultiplier": 10378,
    "CouponRate": 25946,
    "SecurityExchange": "placerat",
    "Issuer": "luctus",
    "EncodedIssuerLen": 20,
    "EncodedIssuer": "enFKc2VneTBDUThFeUtRMWJtTHc=",
    "SecurityDesc": "Vivamus",
    "EncodedSecurityDescLen": 20,
    "EncodedSecurityDesc": "QTF4QjRqRFMzMUU0ek0xeEFiazU=",
    "Pool": "mi",
    "ContractSettlMonth": null,
    "CPProgram": 2,
    "CPRegType": "rhoncus",
    "EvntGrp": {
      "NoEvents": [
        {
          "EventType": 1,
          "EventDate": null,
          "EventPx": 16817,
          "EventText": "amet"
        }
      ]
    },
    "DatedDate": null,
    "InterestAccrualDate": null
  }
""";

            var er2 = FromJson<Instrument>(expected);
            Assert.That(er2.SecurityID, Is.EqualTo(er.Instrument.SecurityID));


        }


        [Test]
        public void View_UndInstrmtGrp_Component_Decode_Test()
        {
            Assert.That(_views, Is.Not.Null);
            Assert.That(_views, Has.Count.EqualTo(1));
            var erView = _views[0];
            Assert.That(erView, Is.Not.Null);
            var undInstrmtGrpView = erView.GetView("UndInstrmtGrp");
            Assert.That(undInstrmtGrpView, Is.Not.Null);
            var uig = new UndInstrmtGrp();
            uig.Parse(undInstrmtGrpView);
            Assert.That(uig.NoUnderlyings, Is.Not.Null);
            Assert.That(uig.NoUnderlyings.Length, Is.EqualTo(2));
            var u0 = uig.NoUnderlyings[0];
            Assert.That(u0, Is.Not.Null);
            var underlying0 = u0.UnderlyingInstrument;
            Assert.That(underlying0, Is.Not.Null);
            Assert.That(underlying0.UnderlyingSymbol, Is.EqualTo("massa."));

            var expected0 = """
{
  "NoUnderlyingSecurityAltID": [
  {
     "UnderlyingSecurityAltID": "ornare",
     "UnderlyingSecurityAltIDSource": "magna."
   },
   {
     "UnderlyingSecurityAltID": "non",
     "UnderlyingSecurityAltIDSource": "at"
   },
   {
     "UnderlyingSecurityAltID": "hendrerit",
     "UnderlyingSecurityAltIDSource": "Pellentesque"
    }
  ]
}
""";
            /*
              "UnderlyingInstrument": {
          "UnderlyingSymbol": "erat",
          "UnderlyingSymbolSfx": "In",
          "UnderlyingSecurityID": "feugiat",
          "UnderlyingSecurityIDSource": "ut",
          "UndSecAltIDGrp": {
            "NoUnderlyingSecurityAltID": [
              {
                "UnderlyingSecurityAltID": "Quisque",
                "UnderlyingSecurityAltIDSource": "tortor"
              }
            ]
          },
             */
            var expectedInst = FromJson<UndSecAltIDGrp>(expected0);
            Assert.Multiple(() =>
            {
                Assert.That(expectedInst, Is.Not.Null);
                Assert.That(underlying0.UndSecAltIDGrp, DIs.DeepEqualTo(expectedInst));
                Assert.That(uig.NoUnderlyings[1].UnderlyingInstrument.UnderlyingSymbol, Is.EqualTo("erat"));
                Assert.That(uig.NoUnderlyings[1].UnderlyingInstrument.UndSecAltIDGrp.NoUnderlyingSecurityAltID[0].UnderlyingSecurityAltID, Is.EqualTo("Quisque"));
                Assert.That(uig.NoUnderlyings[1].UnderlyingInstrument.UndSecAltIDGrp.NoUnderlyingSecurityAltID[0].UnderlyingSecurityAltIDSource, Is.EqualTo("tortor"));
            });
        }
    }
}
