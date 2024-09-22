using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Dictionary.Contained;
using PureFix.Dictionary.Definition;
using PureFix.Types;
using PureFix.Types.Config;


namespace PureFix.Buffer.Ascii
{
    public class AsciiEncoder : IMessageEncoder
    {
        public IFixDefinitions Definitions { get; set; }
        private StoragePool Pool { get; set; }
        public byte LogDelimiter { get; set; } = AsciiChars.Pipe;
        public byte Delimiter { get; set; } = AsciiChars.Soh;
        public ISessionDescription SessionDescription { get; }
        public int MsgSeqNum { get; set; }
        public ISessionMessageFactory SessionMessageFactory { get; }
        public IFixClock Clock {get;}

        public AsciiEncoder(IFixDefinitions definitions, ISessionDescription sessionDescription, ISessionMessageFactory messageFactory, IFixClock clock)
        {
            Definitions = definitions;
            Pool = new StoragePool();
            SessionDescription = sessionDescription;
            SessionMessageFactory = messageFactory;
            Clock = clock;
        }

        public void Return(StoragePool.Storage storage)
        {
            Pool.Deliver(storage);
            Pool.Reclaim();
        }

        // take an application created object e.g. Logon, and encode to fix wire format such that it
        // can be transmitted to the socket. We expect the storage to be returned to the pool once
        // message has been sent.

        public StoragePool.Storage? Encode(string msgType, IFixMessage message)
        {
            if (msgType == null) return null;
            if (!Definitions.Message.TryGetValue(msgType, out _))
            {
                return null; 
            }

            MsgSeqNum = Math.Max(1, MsgSeqNum);

            var hdr = SessionMessageFactory.Header(msgType, MsgSeqNum, Clock.Current);
            if (hdr == null)
            {
                return null;
            }

            // may have to fold permitted user specified header override fields onto the destination message.
            // if this is a replay, then ensure the header is set appropriately. The typescript version supports
            // any other field than BeginString, BodyLength, MsgType, SenderCompID, SendingTime, TargetCompID, TargetSubID
            // to be overriden in header based on supplied fields, this is not supported here.

            if (message.StandardHeader != null)
            {
                hdr.OrigSendingTime = message.StandardHeader.SendingTime;
                hdr.MsgSeqNum = message.StandardHeader.MsgSeqNum;
                hdr.PossDupFlag = message.StandardHeader.PossDupFlag;
            } 

            var storage = Pool.Rent();
            
            var writer = new DefaultFixWriter(storage.Buffer, storage.Locations);
            hdr.Encode(writer);
            message.Encode(writer);

            // checksum can only be caluculated after the body length is correctly set which we now will know
            // having serialised the header and message contents.
            // "8=FIX.4.4|9=100001|35=D"

            var width = Math.Max(4, SessionDescription.BodyLengthChars ?? 7);
            storage.PatchBodyLength(width);

            var checksum = storage.Buffer.Checksum();
            var trailer = SessionMessageFactory.Trailer(checksum);
            if (trailer == null)
            {
                return null;
            }
            trailer.Encode(writer);
            return storage;
        }
    }
}
