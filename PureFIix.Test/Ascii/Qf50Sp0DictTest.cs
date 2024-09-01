using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser.QuickFix;

namespace PureFIix.Test.Ascii
{
    public class Qf50Sp0DictTest
    {
        private FixDefinitions _definitions;
        [OneTimeSetUp]
        public void OnceSetup()
        {
            var rootFolder = Directory.GetCurrentDirectory();
            _definitions = new FixDefinitions();
            var qf = new QuickFixXmlFileParser(_definitions);
            qf.Parse(Path.Join(rootFolder, "Data", "fix5-mod.xml"));
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Check_Security_List_Test()
        {
            Assert.Pass();
        }
    }
}