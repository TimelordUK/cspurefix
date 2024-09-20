using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Constraints;
using PureFIix.Test.Env;
using PureFix.Buffer;
using PureFix.Buffer.Ascii;
using PureFix.Types;
using PureFix.Types.Config;
using PureFix.Types.FIX44.QuickFix;
using PureFix.Types.FIX44.QuickFix.Types;
using static PureFix.Buffer.Ascii.AsciiParser;

namespace PureFIix.Test.Ascii
{
    public class AsciiEncoderTest
    {
        private TestEntity _testEntity;
        private IFixClock _clock = new TestClock() { Current = new DateTime(2024, 1, 1) };

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

        (IFixWriter writer, StoragePool.Storage storage) GetWriter()
        {
            var pool = new StoragePool();
            var storage = pool.Rent();
            var writer = new DefaultFixWriter(storage.Buffer, storage.Locations);
            return (writer, storage);
        }

        [Test]
        public void Create_Session_Header_Test()
        {
            var session = GetDescription();
            var factory = new Fix44SessionMessageFactory(session);
            var header = factory.Header(MsgTypeValues.NewOrderSingle,1, new DateTime(2024, 1, 1));
            Assert.That(header, Is.Not.Null);
            var (writer, storage) = GetWriter();
            header.Encode(writer);
            var s = storage.AsString(AsciiChars.Pipe);
            Assert.That(s, Is.EqualTo("8=FIX.4.4|9=100001|35=D|49=init-tls-comp|56=accept-tls-comp|34=1|57=fix|52=20240101-00:00:00.000|"));
        }

        private StoragePool.Storage Make_Encode_Header()
        {
            var session = GetDescription();
            var factory = new Fix44SessionMessageFactory(session);
            var header = factory.Header(MsgTypeValues.NewOrderSingle, 1, new DateTime(2024, 1, 1));
            Assert.That(header, Is.Not.Null);
            var (writer, storage) = GetWriter();
            header.Encode(writer);
            storage.PatchBodyLength(session.BodyLengthChars ?? 7);
            return storage;
        }

        [Test]
        public void Patch_BodyLen_Header_Test()
        {
            var storage = Make_Encode_Header();
            var s = storage.AsString(AsciiChars.Pipe);
            var expected = storage.Buffer.Pos - "8=FIX.4.4|9=100001|".Length;
            const string begin = "8=FIX.4.4|9=000078|";
            Assert.That(s, Does.StartWith(begin));
        }

        [Test]
        public void Encode_NewOrderSingle_Test()
        {
            var session = GetDescription();
            Assert.That(session, Is.Not.Null);
            var def = _testEntity.Definitions.Message.GetValueOrDefault("NewOrderSingle");
            Assert.That(def, Is.Not.Null);
            var msg = MakeOrder();
            var formatter = new AsciiEncoder(_testEntity.Definitions, session, new Fix44SessionMessageFactory(session), _clock);
            var res = formatter.Encode(MsgTypeValues.NewOrderSingle,  msg);
            var s = res.AsString(AsciiChars.Pipe);
            Assert.That(s, Is.EqualTo("8=FIX.4.4|9=000160|35=D|49=init-tls-comp|56=accept-tls-comp|57=fix|52=20240101-00:00:00.000|35=D|11=NF 0040/03022010|1=ABC123ZYX|21=1|111=0|55=IOC|54=1|38=1000|40=2|44=49.38|59=0|10=251|"));
        }

        [Test]
        public void Encode_Header_With_BodyLen_Test()
        {
            var storage = Make_Encode_Header();
            var s = storage.AsString(AsciiChars.Pipe);
            Assert.That(s, Is.EqualTo("8=FIX.4.4|9=000078|35=D|49=init-tls-comp|56=accept-tls-comp|34=1|57=fix|52=20240101-00:00:00.000|"));
        }

        private static readonly TagPos BeginString = new() { Position = 0, Tag = 8, Start = 2, Len = 7 };
        private static readonly TagPos BodyLength = new() { Position = 1, Tag = 9, Start = 12, Len = 6 };
        private static readonly TagPos MsgType = new() { Position = 2, Tag = 35, Start = 22, Len = 1 };
        private static readonly TagPos SenderCompID = new() { Position = 3, Tag = 49, Start = 27, Len = 13 };
        private static readonly TagPos TargetCompID = new() { Position = 4, Tag = 56, Start = 44, Len = 15 };
        private static readonly TagPos MsgSeqNum = new() { Position = 5, Tag = 34, Start = 63, Len = 1 };
        
        // 012345678901234567890123
        // 8=FIX.4.4|9=000078|35=D|

        [Test]
        public void Encode_Header_BeginString_Test()
        {
            var storage = Make_Encode_Header();
            var beginString = storage.BeginStringLoc;
            Assert.That(beginString, Is.Not.Null);
            Assert.That(beginString, Is.EqualTo(BeginString));
            var v = storage.GetStringAt(0);
            Assert.That(v, Is.EqualTo("FIX.4.4"));
        }

        [Test]
        public void Encode_Header_BodyLength_Test()
        {
            var storage = Make_Encode_Header();
            var bodyLength = storage.BodyLengthLoc;
            Assert.That(bodyLength, Is.Not.Null);
            Assert.That(bodyLength, Is.EqualTo(BodyLength));
            var v = storage.GetStringAt(1);
            Assert.That(v, Is.EqualTo("000078"));
        }

        [Test]
        public void Encode_Header_MsgType_Test()
        {
            var storage = Make_Encode_Header();
            var msgType = storage.MsgTypeLoc;
            Assert.That(msgType, Is.Not.Null);
            Assert.That(msgType, Is.EqualTo(MsgType));
            var v = storage.GetStringAt(2);
            Assert.That(v, Is.EqualTo("D"));
        }

        [Test]
        public void Encode_Header_SenderCompID_Test()
        {
            var storage = Make_Encode_Header();
            var senderCompID = storage.Locations[3];
            Assert.That(senderCompID, Is.EqualTo(SenderCompID));
            var v = storage.GetStringAt(3);
            Assert.That(v, Is.EqualTo("init-tls-comp"));
        }

        [Test]
        public void Encode_Header_TargetCompID_Test()
        {
            var storage = Make_Encode_Header();
            var senderCompID = storage.Locations[4];
            Assert.That(senderCompID, Is.EqualTo(TargetCompID));
            var v = storage.GetStringAt(4);
            Assert.That(v, Is.EqualTo("accept-tls-comp"));
        }

        [Test]
        public void Encode_Header_MsgSeqNum_Test()
        {
            var storage = Make_Encode_Header();
            var msgSeqNum = storage.Locations[5];
            Assert.That(msgSeqNum, Is.EqualTo(MsgSeqNum));
            var v = storage.GetStringAt(5);
            Assert.That(v, Is.EqualTo("1"));
        }
    }
}