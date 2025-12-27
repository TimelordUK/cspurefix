using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Dictionary.Contained;
using PureFix.Dictionary.Definition;
using PureFix.Types;
using PureFix.Types.Config;


namespace PureFix.Buffer.Ascii
{
    public class AsciiEncoder(
        IFixDefinitions definitions,
        ISessionDescription sessionDescription,
        ISessionMessageFactory messageFactory,
        IFixClock clock)
        : IMessageEncoder
    {
        public IFixDefinitions Definitions { get; set; } = definitions;
        private StoragePool Pool { get; } = new();

        public ISessionDescription SessionDescription { get; } = sessionDescription;

        // application if not reset seq num will need to set correctly
        public int MsgSeqNum { get; set; } = 1;
        public ISessionMessageFactory SessionMessageFactory { get; } = messageFactory;
        public IFixClock Clock {get;} = clock;

        public void Return(StoragePool.Storage storage)
        {
            Pool.Return(storage);
        }

        // take an application created object e.g. Logon, and encode to fix wire format such that it
        // can be transmitted to the socket. We expect the storage to be returned to the pool once
        // message has been sent.

        public StoragePool.Storage? Encode(string msgType, IFixMessage message)
        {
            var startTicks = Stopwatch.GetTimestamp();
            try
            {
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
                    hdr.MergeFrom(message.StandardHeader);
                    // having folded fields from the provided header to the message header, do not wish to serialise the
                    // old header so reset all fields. Also reset trailer it will be re-computed.
                    message.StandardHeader.Reset();
                    message.StandardTrailer?.Reset();
                }

                var storage = Pool.Rent();

                var delimiter = SessionDescription.Application?.Delimiter ?? AsciiChars.Soh;
                var writer = new DefaultFixWriter(storage.Buffer, storage.Locations, delimiter);
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
                    Return(storage);
                    return null;
                }

                trailer.Encode(writer);
                MsgSeqNum = MsgSeqNum + 1;
                return storage;
            }
            finally
            {
                var elapsed = Stopwatch.GetElapsedTime(startTicks);
                FixMetrics.EncodeLatency.Record(elapsed.TotalMicroseconds,
                    new KeyValuePair<string, object?>("msgType", msgType));
            }
        }
    }
}
