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
            _setHelper.IsComponent(securityList, index++, "StandardTrailer", true);
        }
    }
}