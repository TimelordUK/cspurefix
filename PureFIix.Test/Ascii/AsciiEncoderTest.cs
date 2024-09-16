using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFIix.Test.Env;
using PureFix.Buffer.Ascii;
using PureFix.Types.FIX44.QuickFix;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFIix.Test.Ascii
{
    public class AsciiEncoderTest
    {
        private TestEntity _testEntity;

        [OneTimeSetUp]
        public void OnceSetup()
        {
            _testEntity = new TestEntity();
        }

        [SetUp]
        public void Setup()
        {
            _testEntity.Prepare();
        }

        private NewOrderSingle MakeOrder()
        {
            var msg = new NewOrderSingle
            {
                StandardHeader = new()
                {
                    MsgType = MsgTypeValues.OrderSingle
                },
                ClOrdID = "NF 0040/03022010",
                Account = "ABC123ZYX",
                HandlInst = "1",
                MaxFloor = 0,
                Instrument = new()
                {
                    Symbol = "IOC"
                },
                Side = SideValues.Buy,
                OrderQtyData = new()
                {
                    OrderQty = 1000
                },
                OrdType = OrdTypeValues.Limit,
                Price = 49.38,
                TimeInForce = TimeInForceValues.Day
            };
            return msg;
        }

        [Test]
        public void Encode_Instument_Test()
        {
            var def = _testEntity.Definitions.Message.GetValueOrDefault("NewOrderSingle");
            Assert.That(def, Is.Not.Null);
            var msg = MakeOrder();
            var formatter = new AsciiEncoder(_testEntity.Definitions);
            var res = formatter.Encode(msg);
        }
    }
}
