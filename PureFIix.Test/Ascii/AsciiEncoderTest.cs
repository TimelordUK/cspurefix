using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFIix.Test.Env;
using PureFix.Buffer.Ascii;
using PureFix.Types;
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
                    MsgType = MsgTypeValues.NewOrderSingle
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

        private static SessionDescription GetDescription()
        {
            using var streamReader = File.OpenText(Path.Join(Fix44PathHelper.SessionRootPath, "test-qf44-initiator-tls.json"));
            var all = streamReader.ReadToEnd();
            var session = JsonHelper.FromJson<SessionDescription>(all);
            return session;
        }

        [Test]
        public void Create_Session_Trailer_Test()
        {
            var session = GetDescription();
            var factory = new Fix44SessionMessageFactory(session);
            var trailer = factory.Trailer(1);
            Assert.That(trailer, Is.Not.Null);
            Assert.That(trailer.CheckSum, Is.EqualTo("001"));

            var trailer2 = factory.Trailer(12);
            Assert.That(trailer2, Is.Not.Null);
            Assert.That(trailer2.CheckSum, Is.EqualTo("012"));
        }

        [Test]
        public void Create_Session_Header_Test()
        {
            var session = GetDescription();
            var factory = new Fix44SessionMessageFactory(session);
            var header = factory.Header(MsgTypeValues.OrderSingle,1, DateTime.Now);
            Assert.That(header, Is.Not.Null);
        }

        [Test]
        public void Encode_Instument_Test()
        {
            var session = GetDescription();
            Assert.That(session, Is.Not.Null);
            var def = _testEntity.Definitions.Message.GetValueOrDefault("NewOrderSingle");
            Assert.That(def, Is.Not.Null);
            var msg = MakeOrder();
            var formatter = new AsciiEncoder(_testEntity.Definitions, session, new Fix44SessionMessageFactory(session), new RealtimeClock());
            var res = formatter.Encode(MsgTypeValues.OrderSingle,  msg);
        }
    }
}
