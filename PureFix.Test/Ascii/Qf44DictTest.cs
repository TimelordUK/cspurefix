using PureFIix.Test.Env;
using PureFix.Dictionary.Contained;
using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser.QuickFix;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Test.Ascii
{
    public class Qf44DictTest
    {
        private IFixDefinitions _definitions;
        private SetConstraintHelper _setHelper;

        [OneTimeSetUp]
        public void OnceSetup()
        {
            _definitions = new FixDefinitions();
            _setHelper = new SetConstraintHelper(_definitions);
            var qf = new QuickFixXmlFileParser(_definitions);
            qf.Parse(Fix44PathHelper.DataDictPath);
        }

        [Test]
        public void Check_Definitions_Version_Test()
        {
            Assert.Multiple(() =>
            {
                Assert.That(_definitions.GetMajor(), Is.EqualTo(4));
                Assert.That(_definitions.GetMinor(), Is.EqualTo(4));
                Assert.That(_definitions.GetServicePack(), Is.EqualTo(0));
            });
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
            Assert.Multiple(() =>
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
            });
        }

        /*
         *  <message name='Heartbeat' msgcat='admin' msgtype='0'>
             <field name='TestReqID' required='N' />
           </message>
         */
        [Test]
        public void Check_Heartbeat_Fields_Test()
        {
            var hb = _definitions.Message["Heartbeat"];
            Assert.That(hb, Is.Not.Null);
            var index = 0;
            _setHelper.IsComponent(hb, index++, "StandardHeader", true);
            _setHelper.IsSimple(hb, index++, "TestReqID", false);
            _setHelper.IsComponent(hb, index++, "StandardTrailer", true);
            Assert.That(hb.Fields, Has.Count.EqualTo(index));
        }

        /*
         *  <message name='ResendRequest' msgcat='admin' msgtype='2'>
             <field name='BeginSeqNo' required='Y' />
             <field name='EndSeqNo' required='Y' />
           </message>
         */

        [Test]
        public void Check_ResendRequest_Fields_Test()
        {
            var hb = _definitions.Message["ResendRequest"];
            Assert.That(hb, Is.Not.Null);
            var index = 0;
            _setHelper.IsComponent(hb, index++, "StandardHeader", true);
            _setHelper.IsSimple(hb, index++, "BeginSeqNo", true);
            _setHelper.IsSimple(hb, index++, "EndSeqNo", true);
            _setHelper.IsComponent(hb, index++, "StandardTrailer", true);
            Assert.That(hb.Fields, Has.Count.EqualTo(index));
        }

        [Test]
        public void Check_Instrument_Contained_Tags_Test()
        {
            var instrumnet = _definitions.Component["Instrument"];
            Assert.That(instrumnet, Is.Not.Null);
            // var tags = string.Join(",", instrumnet.ContainedTag.Keys.ToList());
            // make sure all tags are held from all contained fields.
            int[] t = [
                 55,65,48,22,460,461,167,762,200,541,201,224,225,239,226,227,228,255,543,470,471,472,240,202,947,206,231,223,207,106,348,349,107,350,351,691,667,875,876,873,874,454,455,456,864,865,866,867,868
                ];
            // Console.WriteLine($"{tags}");
            foreach (var tag in t)
            {
                Assert.That(instrumnet.ContainedTag.ContainsKey(tag), Is.True); 
            }
            Assert.That(instrumnet.ContainedTag, Has.Count.EqualTo(49));
        }

        [Test]
        public void Check_Instrument_IndexTest()
        {
            var instrumnet = _definitions.Component["Instrument"];
            Assert.That(instrumnet, Is.Not.Null);
            var collector = new ContainedFieldCollector();
            var res = collector.Compute(instrumnet);
            var tags = res.Select(kv=>kv.child).OfType<ContainedSimpleField>().Select(sf=>sf.Definition.Tag).Distinct().ToList();
            // sim[le field collection will be missing the 2 NoOfGroup fields.
            Assert.That(tags, Has.Count.EqualTo(47));
            var tags2 = res.Select(kv => kv.child).OfType<ContainedGroupField>().Select(sf => sf.Definition?.NoOfField?.Tag).Distinct().ToList();
            Assert.That(tags2, Is.EqualTo(new List<int>() { 454, 864 }));
        }

        [Test]
        [TestCase("Instrument")]
        [TestCase("Logon")]
        [TestCase("TradeCaptureReport")]
        [TestCase("ExecutionReport")]
        public void Check_Get_Msg_Component_Test(string name)
        {
            var instrument = _definitions.GetMsgOrComponent(name);
            Assert.That(instrument, Is.Not.Null);
            Assert.That(instrument.Name, Is.EqualTo(name));
        }

        [Test]
        [TestCase(8)]
        [TestCase(9)]
        [TestCase(35)]
        public void Check_Index_Tag_Fetch_Test(int tag)
        {
            var simple = _definitions[tag];
            Assert.That(simple, Is.Not.Null);
            Assert.That(simple.Tag, Is.EqualTo(tag));
        }
    }
}
