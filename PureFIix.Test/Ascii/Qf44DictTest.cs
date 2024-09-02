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
    public class Qf44DictTest
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
            qf.Parse(Path.Join(rootFolder, "..", "..", "..", "..", "Data", "FIX44.xml"));
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
            _setHelper.IsEnum(def?.Enums, "1", "Buy", "BUY");
            _setHelper.IsEnum(def?.Enums, "2", "Sell", "SELL");
            _setHelper.IsEnum(def?.Enums, "3", "BuyMinus", "BUY_MINUS");
            _setHelper.IsEnum(def?.Enums, "4", "SellPlus", "SELL_PLUS");
            _setHelper.IsEnum(def?.Enums, "5", "SellShort", "SELL_SHORT");
            _setHelper.IsEnum(def?.Enums, "6", "SellShortExempt", "SELL_SHORT_EXEMPT");
            _setHelper.IsEnum(def?.Enums, "7", "Undisclosed", "UNDISCLOSED");
            _setHelper.IsEnum(def?.Enums, "8", "Cross", "CROSS");
            _setHelper.IsEnum(def?.Enums, "9", "CrossShort", "CROSS_SHORT");
            _setHelper.IsEnum(def?.Enums, "A", "CrossShortExempt", "CROSS_SHORT_EXEMPT");
            _setHelper.IsEnum(def?.Enums, "B", "AsDefined", "AS_DEFINED");
            _setHelper.IsEnum(def?.Enums, "C", "Opposite", "OPPOSITE");
            _setHelper.IsEnum(def?.Enums, "D", "Subscribe", "SUBSCRIBE");
            _setHelper.IsEnum(def?.Enums, "E", "Redeem", "REDEEM");
            _setHelper.IsEnum(def?.Enums, "F", "Lend", "LEND");
            _setHelper.IsEnum(def?.Enums, "G", "Borrow", "BORROW");
        }
    }
}
