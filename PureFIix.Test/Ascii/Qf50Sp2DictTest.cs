using PureFIix.Test.Env;
using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser;
using PureFix.Dictionary.Parser.QuickFix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFIix.Test.Ascii
{
    public class Qf50Sp2DictTest
    {
        private FixDefinitions _definitions;
        private SetConstraintHelper _setHelper;

        [OneTimeSetUp]
        public void OnceSetup()
        {
            var rootFolder = Directory.GetCurrentDirectory();
            _definitions = new FixDefinitions();
            _setHelper = new SetConstraintHelper(_definitions);
            var qf = new QuickFixXmlFileParser(_definitions);
            qf.Parse(Path.Join(rootFolder, "..", "..", "..", "..", "Data", "FIX50SP2.xml"));
        }

        private void CheckTradeCapture(MessageDefinition tc)
        {
            var index = 0;
            _setHelper.IsComponent(tc, index++, "StandardHeader", true);
            _setHelper.IsComponent(tc, index++, "ApplicationSequenceControl", false);
            _setHelper.IsSimple(tc, index++, "TradeReportID", false);
            _setHelper.IsSimple(tc, index++, "TradeID", false);
            _setHelper.IsSimple(tc, index++, "SecondaryTradeID", false);
            _setHelper.IsSimple(tc, index++, "FirmTradeID", false);
            _setHelper.IsSimple(tc, index++, "SecondaryFirmTradeID", false);
            _setHelper.IsSimple(tc, index++, "PackageID", false);
            _setHelper.IsSimple(tc, index++, "TradeNumber", false);
            _setHelper.IsSimple(tc, index++, "TradeReportTransType", false);
            _setHelper.IsSimple(tc, index++, "TradeReportType", false);
            _setHelper.IsSimple(tc, index++, "TrdRptStatus", false);
            _setHelper.IsSimple(tc, index++, "TradeRequestID", false);
            _setHelper.IsSimple(tc, index++, "TrdType", false);
            _setHelper.IsSimple(tc, index++, "TrdSubType", false);
            _setHelper.IsSimple(tc, index++, "SecondaryTrdType", false);
            _setHelper.IsSimple(tc, index++, "TertiaryTrdType", false);
            _setHelper.IsSimple(tc, index++, "AnonymousTradeIndicator", false);
            _setHelper.IsSimple(tc, index++, "AlgorithmicTradeIndicator", false);
            _setHelper.IsSimple(tc, index++, "OffsetInstruction", false);
            _setHelper.IsComponent(tc, index++, "TradePriceConditionGrp", false);
            _setHelper.IsSimple(tc, index++, "TradeHandlingInstr", false);
            _setHelper.IsSimple(tc, index++, "OrigTradeHandlingInstr", false);
            _setHelper.IsSimple(tc, index++, "OrigTradeDate", false);
            _setHelper.IsSimple(tc, index++, "OrigTradeID", false);
            _setHelper.IsSimple(tc, index++, "OrigSecondaryTradeID", false);
            _setHelper.IsSimple(tc, index++, "TransferReason", false);
            _setHelper.IsSimple(tc, index++, "ExecType", false);
            _setHelper.IsSimple(tc, index++, "TotNumTradeReports", false);
            _setHelper.IsSimple(tc, index++, "LastRptRequested", false);
            _setHelper.IsSimple(tc, index++, "ManualOrderIndicator", false);
            _setHelper.IsSimple(tc, index++, "UnsolicitedIndicator", false);
            _setHelper.IsSimple(tc, index++, "SubscriptionRequestType", false);
            _setHelper.IsSimple(tc, index++, "TradeReportRefID", false);
            _setHelper.IsSimple(tc, index++, "SecondaryTradeReportRefID", false);
            _setHelper.IsSimple(tc, index++, "SecondaryTradeReportID", false);
            _setHelper.IsSimple(tc, index++, "TradeLinkID", false);
            _setHelper.IsSimple(tc, index++, "TrdMatchID", false);
            _setHelper.IsSimple(tc, index++, "ExecID", false);
            _setHelper.IsSimple(tc, index++, "ExecRefID", false);
            _setHelper.IsSimple(tc, index++, "SecondaryExecID", false);
            _setHelper.IsSimple(tc, index++, "ExecRestatementReason", false);
            _setHelper.IsSimple(tc, index++, "RegulatoryTransactionType", false);
            _setHelper.IsComponent(tc, index++, "RegulatoryTradeIDGrp", false);
            _setHelper.IsSimple(tc, index++, "PreviouslyReported", false);
            _setHelper.IsSimple(tc, index++, "PriceType", false);
            _setHelper.IsComponent(tc, index++, "PriceQualifierGrp", false);
            _setHelper.IsSimple(tc, index++, "CrossType", false);
            _setHelper.IsComponent(tc, index++, "RootParties", false);
            _setHelper.IsSimple(tc, index++, "AsOfIndicator", false);
            _setHelper.IsSimple(tc, index++, "SettlSessID", false);
            _setHelper.IsSimple(tc, index++, "SettlSessSubID", false);
            _setHelper.IsSimple(tc, index++, "VenueType", false);
            _setHelper.IsSimple(tc, index++, "MarketSegmentID", false);
            _setHelper.IsSimple(tc, index++, "MarketID", false);
            _setHelper.IsSimple(tc, index++, "TaxonomyType", false);
            _setHelper.IsComponent(tc, index++, "Instrument", true);
            _setHelper.IsComponent(tc, index++, "InstrumentExtension", false);
            _setHelper.IsComponent(tc, index++, "FinancingDetails", false);
            _setHelper.IsComponent(tc, index++, "PaymentGrp", false);
            _setHelper.IsSimple(tc, index++, "QtyType", false);
            _setHelper.IsComponent(tc, index++, "YieldData", false);
            _setHelper.IsComponent(tc, index++, "UndInstrmtGrp", false);
            _setHelper.IsComponent(tc, index++, "RelatedInstrumentGrp", false);
            _setHelper.IsComponent(tc, index++, "CollateralAmountGrp", false);
            _setHelper.IsSimple(tc, index++, "CollateralizationValueDate", false);
            _setHelper.IsComponent(tc, index++, "RateSource", false);
            _setHelper.IsComponent(tc, index++, "TransactionAttributeGrp", false);
            _setHelper.IsSimple(tc, index++, "UnderlyingTradingSessionID", false);
            _setHelper.IsSimple(tc, index++, "UnderlyingTradingSessionSubID", false);
            _setHelper.IsSimple(tc, index++, "LastQty", false);
            _setHelper.IsSimple(tc, index++, "LastQtyVariance", false);
            _setHelper.IsSimple(tc, index++, "LastQtyChanged", false);
            _setHelper.IsSimple(tc, index++, "LastMultipliedQty", false);
            _setHelper.IsSimple(tc, index++, "TotalTradeQty", false);
            _setHelper.IsSimple(tc, index++, "TotalTradeMultipliedQty", false);
            _setHelper.IsSimple(tc, index++, "LastPx", false);
            _setHelper.IsSimple(tc, index++, "MidPx", false);
            _setHelper.IsSimple(tc, index++, "DifferentialPrice", false);
            _setHelper.IsSimple(tc, index++, "CalculatedCcyLastQty", false);
            _setHelper.IsSimple(tc, index++, "PriceMarkup", false);
            _setHelper.IsComponent(tc, index++, "AveragePriceDetail", false);
            _setHelper.IsSimple(tc, index++, "Currency", false);
            _setHelper.IsSimple(tc, index++, "CurrencyCodeSource", false);
            _setHelper.IsSimple(tc, index++, "SettlCurrency", false);
            _setHelper.IsSimple(tc, index++, "SettlCurrencyCodeSource", false);
            _setHelper.IsSimple(tc, index++, "SettlPriceFxRateCalc", false);
            _setHelper.IsSimple(tc, index++, "LastParPx", false);
            _setHelper.IsSimple(tc, index++, "LastSpotRate", false);
            _setHelper.IsSimple(tc, index++, "LastForwardPoints", false);
            _setHelper.IsSimple(tc, index++, "LastSwapPoints", false);
            _setHelper.IsSimple(tc, index++, "PricePrecision", false);
            _setHelper.IsSimple(tc, index++, "LastMkt", false);
            _setHelper.IsSimple(tc, index++, "ClearingTradePrice", false);
            _setHelper.IsSimple(tc, index++, "TradePriceNegotiationMethod", false);
            _setHelper.IsSimple(tc, index++, "LastUpfrontPrice", false);
            _setHelper.IsSimple(tc, index++, "UpfrontPriceType", false);
            _setHelper.IsSimple(tc, index++, "TradeDate", false);
            _setHelper.IsSimple(tc, index++, "ClearingBusinessDate", false);
            _setHelper.IsSimple(tc, index++, "ClearingPortfolioID", false);
            _setHelper.IsSimple(tc, index++, "AvgPx", false);
            _setHelper.IsComponent(tc, index++, "SpreadOrBenchmarkCurveData", false);
            _setHelper.IsSimple(tc, index++, "AvgPxGroupID", false);
            _setHelper.IsSimple(tc, index++, "AvgPxIndicator", false);
            _setHelper.IsSimple(tc, index++, "ValuationDate", false);
            _setHelper.IsSimple(tc, index++, "ValuationTime", false);
            _setHelper.IsSimple(tc, index++, "ValuationBusinessCenter", false);
            _setHelper.IsComponent(tc, index++, "PositionAmountData", false);
            _setHelper.IsSimple(tc, index++, "MultiLegReportingType", false);
            _setHelper.IsSimple(tc, index++, "TradeLegRefID", false);
            _setHelper.IsComponent(tc, index++, "TrdInstrmtLegGrp", false);
            _setHelper.IsSimple(tc, index++, "TransactTime", false);
            _setHelper.IsComponent(tc, index++, "TrdRegTimestamps", false);
            _setHelper.IsSimple(tc, index++, "SettlType", false);
            _setHelper.IsSimple(tc, index++, "SettlDate", false);
            _setHelper.IsSimple(tc, index++, "TerminationDate", false);
            _setHelper.IsSimple(tc, index++, "UnderlyingSettlementDate", false);
            _setHelper.IsSimple(tc, index++, "MatchStatus", false);
            _setHelper.IsSimple(tc, index++, "ExecMethod", false);
            _setHelper.IsSimple(tc, index++, "MatchType", false);
            _setHelper.IsComponent(tc, index++, "TradeQtyGrp", false);
            _setHelper.IsComponent(tc, index++, "TrdCapRptSideGrp", true);
            _setHelper.IsSimple(tc, index++, "Volatility", false);
            _setHelper.IsSimple(tc, index++, "TimeToExpiration", false);
            _setHelper.IsSimple(tc, index++, "DividendYield", false);
            _setHelper.IsSimple(tc, index++, "RiskFreeRate", false);
            _setHelper.IsSimple(tc, index++, "PriceDelta", false);
            _setHelper.IsSimple(tc, index++, "CurrencyRatio", false);
            _setHelper.IsSimple(tc, index++, "CopyMsgIndicator", false);
            _setHelper.IsComponent(tc, index++, "TrdRepIndicatorsGrp", false);
            _setHelper.IsSimple(tc, index++, "TradeReportingIndicator", false);
            _setHelper.IsSimple(tc, index++, "PublishTrdIndicator", false);
            _setHelper.IsSimple(tc, index++, "TradePublishIndicator", false);
            _setHelper.IsComponent(tc, index++, "TrdRegPublicationGrp", false);
            _setHelper.IsSimple(tc, index++, "ShortSaleReason", false);
            _setHelper.IsSimple(tc, index++, "TierCode", false);
            _setHelper.IsSimple(tc, index++, "MessageEventSource", false);
            _setHelper.IsSimple(tc, index++, "LastUpdateTime", false);
            _setHelper.IsSimple(tc, index++, "RndPx", false);
            _setHelper.IsSimple(tc, index++, "TZTransactTime", false);
            _setHelper.IsSimple(tc, index++, "ReportedPxDiff", false);
            _setHelper.IsSimple(tc, index++, "GrossTradeAmt", false);
            _setHelper.IsSimple(tc, index++, "TotalGrossTradeAmt", false);
            _setHelper.IsSimple(tc, index++, "TradeReportRejectReason", false);
            _setHelper.IsSimple(tc, index++, "RejectText", false);
            _setHelper.IsSimple(tc, index++, "EncodedRejectTextLen", false);
            _setHelper.IsSimple(tc, index++, "EncodedRejectText", false);
            _setHelper.IsSimple(tc, index++, "FeeMultiplier", false);
            _setHelper.IsSimple(tc, index++, "ClearedIndicator", false);
            _setHelper.IsSimple(tc, index++, "ClearingIntention", false);
            _setHelper.IsSimple(tc, index++, "TradeClearingInstruction", false);
            _setHelper.IsSimple(tc, index++, "BackloadedTradeIndicator", false);
            _setHelper.IsSimple(tc, index++, "ConfirmationMethod", false);
            _setHelper.IsSimple(tc, index++, "MandatoryClearingIndicator", false);
            _setHelper.IsComponent(tc, index++, "MandatoryClearingJurisdictionGrp", false);
            _setHelper.IsSimple(tc, index++, "MixedSwapIndicator", false);
            _setHelper.IsSimple(tc, index++, "MultiAssetSwapIndicator", false);
            _setHelper.IsSimple(tc, index++, "InternationalSwapIndicator", false);
            _setHelper.IsSimple(tc, index++, "OffMarketPriceIndicator", false);
            _setHelper.IsSimple(tc, index++, "VerificationMethod", false);
            _setHelper.IsSimple(tc, index++, "ClearingRequirementException", false);
            _setHelper.IsSimple(tc, index++, "IRSDirection", false);
            _setHelper.IsSimple(tc, index++, "RegulatoryReportType", false);
            _setHelper.IsSimple(tc, index++, "RegulatoryReportTypeBusinessDate", false);
            _setHelper.IsSimple(tc, index++, "VoluntaryRegulatoryReport", false);
            _setHelper.IsSimple(tc, index++, "MultiJurisdictionReportingIndicator", false);
            _setHelper.IsSimple(tc, index++, "TradeCollateralization", false);
            _setHelper.IsSimple(tc, index++, "TradeContinuation", false);
            _setHelper.IsSimple(tc, index++, "TradeContingency", false);
            _setHelper.IsSimple(tc, index++, "TradeVersion", false);
            _setHelper.IsSimple(tc, index++, "HistoricalReportIndicator", false);
            _setHelper.IsSimple(tc, index++, "DeltaCrossed", false);
            _setHelper.IsSimple(tc, index++, "TradeContinuationText", false);
            _setHelper.IsSimple(tc, index++, "EncodedTradeContinuationTextLen", false);
            _setHelper.IsSimple(tc, index++, "EncodedTradeContinuationText", false);
            _setHelper.IsSimple(tc, index++, "IntraFirmTradeIndicator", false);
            _setHelper.IsSimple(tc, index++, "AffiliatedFirmsTradeIndicator", false);
            _setHelper.IsComponent(tc, index++, "AttachmentGrp", false);
            _setHelper.IsSimple(tc, index++, "RiskLimitCheckStatus", false);
            _setHelper.IsComponent(tc, index++, "StandardTrailer", true);
            Assert.That(tc.Fields.Count, Is.EqualTo(index));
        }

        [Test]
        public void Check_Trade_Capture_Message_Def_Test()
        {
            CheckTradeCapture(_definitions.Message["AE"]);
        }

        [Test]
        public void Check_Field_By_Name_AdvSide_Test()
        {
            Assert.That(_definitions.Simple.TryGetValue("AdvSide", out var def), Is.True);
            Assert.That(def, Is.Not.Null);
            Assert.That(def.Tag, Is.EqualTo(4));
        }

        [Test]
        public void Check_Definitions_Version_Test()
        {
            Assert.That(_definitions.GetMajor(), Is.EqualTo(5));
            Assert.That(_definitions.GetMinor(), Is.EqualTo(0));
            Assert.That(_definitions.GetServicePack(), Is.EqualTo(2));
        }

        [Test]
        public void Check_Version_Test()
        {
            Assert.That(_definitions.Version, Is.EqualTo(FixVersion.FIX50SP2));
        }

        private void CheckEnum(IReadOnlyDictionary<string, FieldEnum>  enums, string key, string expectedVal, string expectedDescription)
        {
            Assert.That(enums, Is.Not.Null);
            Assert.That(enums.TryGetValue(key, out var en), Is.True);
            Assert.That(en?.Val, Is.EqualTo(expectedVal));
            Assert.That(en?.Description, Is.EqualTo(expectedDescription));
            Assert.That(en?.Key, Is.EqualTo(key));
        }

    [Test]
        public void Check_Field_AdvSide_Test()
        {
            Assert.That(_definitions.Simple.TryGetValue("AdvSide", out var def), Is.True);
            Assert.That(def, Is.Not.Null);
            Assert.That(def.Tag, Is.EqualTo(4));
            Assert.That(def.IsEnum, Is.True);
            CheckEnum(def?.Enums, "B", "Buy", "BUY");
            CheckEnum(def?.Enums, "S", "Sell", "SELL");
            CheckEnum(def?.Enums, "X", "Cross", "CROSS");
            CheckEnum(def?.Enums, "T", "Trade", "TRADE");
        }
    }
}
