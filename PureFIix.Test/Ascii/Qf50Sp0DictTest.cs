using PureFIix.Test.Env;
using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser.QuickFix;

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
            _setHelper.IsComponent(securityList, index, "StandardTrailer", true);
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
        public void Check_Sec_List_Grp()
        {
            var secListGrp = _secHelper.GetSecListGrp();
            Assert.That(secListGrp, Is.Not.Null);
            Assert.That(secListGrp.Fields.Count, Is.EqualTo(1));
            _setHelper.IsGroup(secListGrp, 0, "NoRelatedSym", false);
        }


        [Test]
        public void Check_No_Related_Sym()
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
            _setHelper.IsSimple(noRelatedSym, index, "RelSymTransactTime", false);
        }
    }
}