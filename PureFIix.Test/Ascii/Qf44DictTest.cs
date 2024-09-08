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
    public class Qf44DictTest
    {
        private FixDefinitions _definitions;
        private SetConstraintHelper _setHelper;
        public static string RootPath { get; } = Path.Join(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "Data");

        [OneTimeSetUp]
        public void OnceSetup()
        {
            _definitions = new FixDefinitions();
            _setHelper = new SetConstraintHelper(_definitions);
            var qf = new QuickFixXmlFileParser(_definitions);
            qf.Parse(Path.Join(RootPath, "FIX44.xml"));
        }

        [Test]
        public void Check_Definitions_Version_Test()
        {
            Assert.That(_definitions.GetMajor(), Is.EqualTo(4));
            Assert.That(_definitions.GetMinor(), Is.EqualTo(4));
            Assert.That(_definitions.GetServicePack(), Is.EqualTo(0));
        }

        [Test]
        public void Check_Version_Test()
        {
            Assert.That(_definitions.Version, Is.EqualTo(FixVersion.FIX44));
        }

        /*
         *   <field number='54' name='Side' type='CHAR'>
             <value enum='1' description='BUY' />
             <value enum='2' description='SELL' />
             <value enum='3' description='BUY_MINUS' />
             <value enum='4' description='SELL_PLUS' />
             <value enum='5' description='SELL_SHORT' />
             <value enum='6' description='SELL_SHORT_EXEMPT' />
             <value enum='7' description='UNDISCLOSED' />
             <value enum='8' description='CROSS' />
             <value enum='9' description='CROSS_SHORT' />
             <value enum='A' description='CROSS_SHORT_EXEMPT' />
             <value enum='B' description='AS_DEFINED' />
             <value enum='C' description='OPPOSITE' />
             <value enum='D' description='SUBSCRIBE' />
             <value enum='E' description='REDEEM' />
             <value enum='F' description='LEND' />
             <value enum='G' description='BORROW' />
           </field>
         */
        [Test]
        public void Check_Field_AdvSide_Test()
        {
            Assert.That(_definitions.Simple.TryGetValue("Side", out var def), Is.True);
            Assert.That(def, Is.Not.Null);
            Assert.That(def.Tag, Is.EqualTo(54));
            Assert.That(def.IsEnum, Is.True);
            _setHelper.IsEnum(def.Enums, "1", "Buy", "BUY");
            _setHelper.IsEnum(def.Enums, "2", "Sell", "SELL");
            _setHelper.IsEnum(def.Enums, "3", "BuyMinus", "BUY_MINUS");
            _setHelper.IsEnum(def.Enums, "4", "SellPlus", "SELL_PLUS");
            _setHelper.IsEnum(def.Enums, "5", "SellShort", "SELL_SHORT");
            _setHelper.IsEnum(def.Enums, "6", "SellShortExempt", "SELL_SHORT_EXEMPT");
            _setHelper.IsEnum(def.Enums, "7", "Undisclosed", "UNDISCLOSED");
            _setHelper.IsEnum(def.Enums, "8", "Cross", "CROSS");
            _setHelper.IsEnum(def.Enums, "9", "CrossShort", "CROSS_SHORT");
            _setHelper.IsEnum(def.Enums, "A", "CrossShortExempt", "CROSS_SHORT_EXEMPT");
            _setHelper.IsEnum(def.Enums, "B", "AsDefined", "AS_DEFINED");
            _setHelper.IsEnum(def.Enums, "C", "Opposite", "OPPOSITE");
            _setHelper.IsEnum(def.Enums, "D", "Subscribe", "SUBSCRIBE");
            _setHelper.IsEnum(def.Enums, "E", "Redeem", "REDEEM");
            _setHelper.IsEnum(def.Enums, "F", "Lend", "LEND");
            _setHelper.IsEnum(def.Enums, "G", "Borrow", "BORROW");
        }

        /*
         *  <message name='Heartbeat' msgcat='admin' msgtype='0'>
             <field name='TestReqID' required='N' />
           </message>
         */
        [Test]
        public void Check_Heartbeat_Fields()
        {
            var hb = _definitions.Message["Heartbeat"];
            Assert.That(hb, Is.Not.Null);
            var index = 0;
            _setHelper.IsComponent(hb, index++, "StandardHeader", true);
            _setHelper.IsSimple(hb, index++, "TestReqID", false);
            _setHelper.IsComponent(hb, index++, "StandardTrailer", true);
            Assert.That(hb.Fields.Count, Is.EqualTo(index));
        }

        /*
         *  <message name='ResendRequest' msgcat='admin' msgtype='2'>
             <field name='BeginSeqNo' required='Y' />
             <field name='EndSeqNo' required='Y' />
           </message>
         */

        [Test]
        public void Check_ResendRequest_Fields()
        {
            var hb = _definitions.Message["ResendRequest"];
            Assert.That(hb, Is.Not.Null);
            var index = 0;
            _setHelper.IsComponent(hb, index++, "StandardHeader", true);
            _setHelper.IsSimple(hb, index++, "BeginSeqNo", true);
            _setHelper.IsSimple(hb, index++, "EndSeqNo", true);
            _setHelper.IsComponent(hb, index++, "StandardTrailer", true);
            Assert.That(hb.Fields.Count, Is.EqualTo(index));
        }
    }
}
