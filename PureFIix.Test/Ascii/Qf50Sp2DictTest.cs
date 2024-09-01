using PureFIix.Test.Env;
using PureFix.Dictionary.Definition;
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
            qf.Parse(Path.Join(rootFolder, "..", "..", "..", "..", "Data", "FIX50SP2.xml"));
        }

        [Test]
        public void Check_Field_By_Name_AdvSide_Test()
        {
            Assert.That(_definitions.Simple.TryGetValue("AdvSide", out var def), Is.True);
            Assert.That(def, Is.Not.Null);
            Assert.That(def.Tag, Is.EqualTo(4));
        }
    }
}
