using PureFIix.Test.Env;
using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser.QuickFix;
using System;
using System.Reflection;

namespace PureFIix.Test.Ascii
{
    public class Qf50Sp0DictTest
    {
        private FixDefinitions _definitions;
        private SecDefHelper _secHelper;
        private SetConstraintHelper _setHelper;

        [OneTimeSetUp]
        public void OnceSetup()
        {
            var rootFolder = Directory.GetCurrentDirectory();
            _definitions = new FixDefinitions();
            _secHelper = new SecDefHelper(_definitions);
            _setHelper = new SetConstraintHelper(_definitions);
            var qf = new QuickFixXmlFileParser(_definitions);
            qf.Parse(Path.Join(rootFolder, "Data", "fix5-mod.xml"));
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Check_Security_List_Exists_Test()
        {
            var securityList = _definitions.GetSet("SecurityList");
            Assert.That(securityList, Is.Not.Null);
        }

        [Test]
        public void Check_Security_List_Test()
        {
            var securityList = _definitions.GetSet("SecurityList");
            Assert.That(securityList, Is.Not.Null);
            var index = 0;
            // the header and trailer is not yet being added to the message definitions.
            _setHelper.IsComponent(securityList, index++, "StandardHeader", true);
            _setHelper.IsSimple(securityList, index++, "SecurityReqID", false);
            _setHelper.IsSimple(securityList, index++, "SecurityResponseID", false);
            _setHelper.IsSimple(securityList, index++, "SecurityRequestResult", false);
            _setHelper.IsSimple(securityList, index++, "SecurityListRequestType", false);
            _setHelper.IsSimple(securityList, index++, "TotNoRelatedSym", false);
            _setHelper.IsSimple(securityList, index++, "LastFragment", false);
            _setHelper.IsComponent(securityList, index++, "SecListGrp", false);
            _setHelper.IsSimple(securityList, index++, "SecurityReportID", false);
            _setHelper.IsSimple(securityList, index++, "ClearingBusinessDate", false);
            _setHelper.IsSimple(securityList, index++, "MarketID", false);
            _setHelper.IsSimple(securityList, index++, "MarketSegmentID", false);
            _setHelper.IsSimple(securityList, index++, "SecurityListID", false);
            _setHelper.IsSimple(securityList, index++, "SecurityListRefID", false);
            _setHelper.IsSimple(securityList, index++, "SecurityListDesc", false);
            _setHelper.IsSimple(securityList, index++, "EncodedSecurityListDescLen", false);
            _setHelper.IsSimple(securityList, index++, "EncodedSecurityListDesc", false);
            _setHelper.IsSimple(securityList, index++, "SecurityListType", false);
            _setHelper.IsSimple(securityList, index++, "SecurityListTypeSource", false);
            _setHelper.IsSimple(securityList, index++, "TransactTime", false);
            _setHelper.IsComponent(securityList, index++, "StandardTrailer", true);
            Assert.That(securityList.Fields.Count, Is.EqualTo(index));
        }

        /*
        <component name="SecListGrp">
            <group name="NoRelatedSym" required="N">
                <component name="Instrument"/>
                <component name="InstrumentExtension"/>
                <component name="FinancingDetails"/>
                <component name="UndInstrmtGrp"/>
                <field name="Currency" required="N"/>
                <field name="ContractPositionNumber" required="N"/>
                <component name="Stipulations"/>
                <component name="InstrmtLegSecListGrp"/>
                <component name="SpreadOrBenchmarkCurveData"/>
                <component name="YieldData"/>
                <field name="Text" required="N"/>
                <field name="EncodedTextLen" required="N"/>
                <field name="EncodedText" required="N"/>
                <component name="SecurityTradingRules" required="N" />
                <component name="StrikeRules" required="N" />
                <field name="RelSymTransactTime" required="N" />
            </group>
        </component>
 */

        [Test]
        public void Check_Sec_List_Grp_Test()
        {
            var secListGrp = _secHelper.GetSecListGrp();
            Assert.That(secListGrp, Is.Not.Null);
            Assert.That(secListGrp.Fields, Has.Count.EqualTo(1));
            _setHelper.IsGroup(secListGrp, 0, "NoRelatedSym", false);
        }

        [Test]
        public void Check_No_Related_Sym_Test()
        {
            var index = 0;
            var noRelatedSym = _secHelper.GetNumRelatedSym();
            _setHelper.IsComponent(noRelatedSym, index++, "Instrument", false);
            _setHelper.IsComponent(noRelatedSym, index++, "InstrumentExtension", false);
            _setHelper.IsComponent(noRelatedSym, index++, "FinancingDetails", false);
            _setHelper.IsComponent(noRelatedSym, index++, "UndInstrmtGrp", false);
            _setHelper.IsSimple(noRelatedSym, index++, "Currency", false);
            _setHelper.IsSimple(noRelatedSym, index++, "ContractPositionNumber", false);
            _setHelper.IsComponent(noRelatedSym, index++, "Stipulations", false);
            _setHelper.IsComponent(noRelatedSym, index++, "InstrmtLegSecListGrp", false);
            _setHelper.IsComponent(noRelatedSym, index++, "SpreadOrBenchmarkCurveData", false);
            _setHelper.IsComponent(noRelatedSym, index++, "YieldData", false);
            _setHelper.IsSimple(noRelatedSym, index++, "Text", false);
            _setHelper.IsSimple(noRelatedSym, index++, "EncodedTextLen", false);
            _setHelper.IsSimple(noRelatedSym, index++, "EncodedText", false);
            _setHelper.IsComponent(noRelatedSym, index++, "SecurityTradingRules", false);
            _setHelper.IsComponent(noRelatedSym, index++, "StrikeRules", false);
            _setHelper.IsSimple(noRelatedSym, index++, "RelSymTransactTime", false);
            Assert.That(noRelatedSym.Fields, Has.Count.EqualTo(index));
        }

        /*
        <component name="SecurityTradingRules">
            <component name="BaseTradingRules" required="N" />
            <component name="TradingSessionRulesGrp" required="N" />
            <component name="NestedInstrumentAttribute" required="N" />
        </component>
        */

        [Test]
        public void Check_Security_Trading_Rules_Test()
        {
            var index = 0;
            var securityTradingRules = _secHelper.GetSecurityTradingRules();
            _setHelper.IsComponent(securityTradingRules, index++, "BaseTradingRules", false);
            _setHelper.IsComponent(securityTradingRules, index++, "TradingSessionRulesGrp", false);
            _setHelper.IsComponent(securityTradingRules, index++, "NestedInstrumentAttribute", false);
            Assert.That(securityTradingRules.Fields, Has.Count.EqualTo(index));
        }

        /*
        <component name="BaseTradingRules">
            <component name="TickRules" required="N" />
            <component name="LotTypeRules" required="N" />
            <component name="PriceLimits" required="N" />
            <field name="ExpirationCycle" required="N" />
            <field name="MinTradeVol" required="N" />
            <field name="MaxTradeVol" required="N" />
            <field name="MaxPriceVariation" required="N" />
            <field name="ImpliedMarketIndicator" required="N" />
            <field name="TradingCurrency" required="N" />
            <field name="RoundLot" required="N" />
            <field name="MultilegModel" required="N" />
            <field name="MultilegPriceMethod" required="N" />
            <field name="PriceType" required="N" />
        </component>
 */

        [Test]
        public void Check_Base_Trading_Rules_Test()
        {
            var index = 0;
            var securityTradingRules = _secHelper.GetBaseTradingRules();
            _setHelper.IsComponent(securityTradingRules, index++, "TickRules", false);
            _setHelper.IsComponent(securityTradingRules, index++, "LotTypeRules", false);
            _setHelper.IsComponent(securityTradingRules, index++, "PriceLimits", false);
            _setHelper.IsSimple(securityTradingRules, index++, "ExpirationCycle", false);
            _setHelper.IsSimple(securityTradingRules, index++, "MinTradeVol", false);
            _setHelper.IsSimple(securityTradingRules, index++, "MaxTradeVol", false);
            _setHelper.IsSimple(securityTradingRules, index++, "MaxPriceVariation", false);
            _setHelper.IsSimple(securityTradingRules, index++, "ImpliedMarketIndicator", false);
            _setHelper.IsSimple(securityTradingRules, index++, "TradingCurrency", false);
            _setHelper.IsSimple(securityTradingRules, index++, "RoundLot", false);
            _setHelper.IsSimple(securityTradingRules, index++, "MultilegModel", false);
            _setHelper.IsSimple(securityTradingRules, index++, "MultilegPriceMethod", false);
            _setHelper.IsSimple(securityTradingRules, index++, "PriceType", false);
            Assert.That(securityTradingRules.Fields, Has.Count.EqualTo(index));
        }


        /*
        <component name="TickRules">
            <group name="NoTickRules" required="N">
                <field name="StartTickPriceRange" required="N" />
                <field name="EndTickPriceRange" required="N" />
                <field name="TickIncrement" required="N" />
                <field name="TickRuleType" required="N" />
            </group>
        </component>
 */
        [Test]
        public void Check_Tick_Rules_Test()
        {
            var index = 0;
            var tickRules = _secHelper.GetTickRules();
            _setHelper.IsGroup(tickRules, index++, "NoTickRules", false);
            Assert.That(tickRules.Fields, Has.Count.EqualTo(index));
        }

        [Test]
        public void Check_No_Tick_Rules_Test()
        {
            var index = 0;
            var noTickRules = _secHelper.GetNoTickRules();
            _setHelper.IsSimple(noTickRules, index++, "StartTickPriceRange", false);
            _setHelper.IsSimple(noTickRules, index++, "EndTickPriceRange", false);
            _setHelper.IsSimple(noTickRules, index++, "TickIncrement", false);
            _setHelper.IsSimple(noTickRules, index++, "TickRuleType", false);
            Assert.That(noTickRules.Fields, Has.Count.EqualTo(index));
        }
    }
}