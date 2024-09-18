using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Constraints;
using PureFIix.Test.Env;
using PureFix.Buffer.Ascii;
using PureFix.Types;
using PureFix.Types.FIX44.QuickFix;
using PureFix.Types.FIX44.QuickFix.Types;
using static PureFix.Buffer.Ascii.AsciiParser;

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

        private static NewOrderSingle MakeOrder()
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

        (IFixWriter writer, Pool.Storage storage) GetWriter()
        {
            var pool = new Pool();
            var storage = pool.Rent();
            var writer = new DefaultFixWriter(storage.Buffer, storage.Locations);
            return (writer, storage);
        }

        [Test]
        public void Create_Session_Header_Test()
        {
            var session = GetDescription();
            var factory = new Fix44SessionMessageFactory(session);
            var header = factory.Header(MsgTypeValues.OrderSingle,1, new DateTime(2024, 1, 1));
            Assert.That(header, Is.Not.Null);
            var (writer, storage) = GetWriter();
            header.Encode(writer);
            var s = storage.AsString(AsciiChars.Pipe);
            Assert.That(s, Is.EqualTo("8=FIX.4.4|9=100001|35=D|49=init-tls-comp|56=accept-tls-comp|34=1|57=fix|52=20240101-00:00:00.000|"));
        }

        [Test]
        public void Patch_BodyLen_Header_Test()
        {
            var session = GetDescription();
            var factory = new Fix44SessionMessageFactory(session);
            var header = factory.Header(MsgTypeValues.OrderSingle, 1, new DateTime(2024, 1, 1));
            Assert.That(header, Is.Not.Null);
            var (writer, storage) = GetWriter();
            header.Encode(writer);
            storage.PatchBodyLength(session.BodyLengthChars ?? 7);
            var s = storage.AsString(AsciiChars.Pipe);
            var expected = storage.Buffer.Pos - "8=FIX.4.4|9=100001|".Length;
            var begin = "8=FIX.4.4|9=000078|";
            Assert.That(s, Does.StartWith(begin));
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
