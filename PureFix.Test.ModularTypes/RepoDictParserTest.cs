using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser.QuickFix;
using PureFix.Dictionary.Parser.Repo;

namespace PureFix.Test.ModularTypes
{
    internal class RepoDictParserTest
    {
        FixDefinitions _definitions = new FixDefinitions();
        public static readonly string DataDictRootPath = Path.Join(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "Data", "FIX.5.0SP2","Base");
        public static readonly string FieldDictPath = Path.Join(DataDictRootPath, "Fields.xml");

        [OneTimeSetUp]
        public void OnceSetup()
        {
            _definitions = new FixDefinitions();
            var qf = new RepoFixXmlFileParser(PureFix.Types.FixVersion.FIX50SP2, _definitions);
            qf.Parse(DataDictRootPath);
        }

        [Test]
        public void ParseCheck()
        {
        }
        
    }
}
