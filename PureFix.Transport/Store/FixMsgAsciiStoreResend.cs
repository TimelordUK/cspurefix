using PureFix.Buffer;
using PureFix.Buffer.Ascii;
using PureFix.Transport.Session;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace PureFix.Transport.Store
{
    public class FixMsgAsciiStoreResend : IFixMsgResender
    {
        private readonly IMessageParser m_Parser;
        private readonly IFixMsgStore m_store;
        private readonly IFixConfig m_config;
        private readonly IFixClock m_clock;
        private readonly IFixMessageFactory m_factory;
        private readonly ISessionMessageFactory m_sessionFactory;

        public FixMsgAsciiStoreResend(IFixMsgStore store, IFixMessageFactory factory, IFixConfig config, IFixClock clock)
        {
            ArgumentNullException.ThrowIfNull(config.Definitions);
            ArgumentNullException.ThrowIfNull(config.MessageFactory);

            m_Parser = new AsciiParser(config.Definitions) { 
                Delimiter = config.Delimiter ?? AsciiChars.Soh, 
                WriteDelimiter = config.LogDelimiter ?? AsciiChars.Pipe
            };
            m_store = store;
            m_config = config;
            m_clock = clock;
            m_factory = factory;
            m_sessionFactory = config.MessageFactory;
        }

        public async Task<IReadOnlyList<IFixMsgStoreRecord>> GetResendRequest(int startReq, int endSeq)
        {
            // need to cover request from start to end where any missing numbers are
            // included as gaps to allow vector of messages to be sent by the session
            // on a request
            var records = await m_store.GetSeqNumRange(startReq, endSeq);
            if (records == null)
            {
                List<IFixMsgStoreRecord> toResend = [];
                return toResend;
            }
            var inflated = InflateRange(startReq, endSeq, records);
            return inflated;
        }

        private IReadOnlyList<IFixMsgStoreRecord> InflateRange(int startSeq, int endSeq, IFixMsgStoreRecord?[] input)
        {
            List<IFixMsgStoreRecord> toResend = [];
            if (input.Length == 0)
            {
                return toResend;
            }

            var expected = startSeq;
            for (int i = 0; i < input.Length; i++)
            {
                var v = input[i];
                if (v == null) continue;
                // inflate the record back from encoded form to an object and set standard header params.
                var record = PrepareRecordForRetransmission(v);
                if (record == null) continue;
                var seqNum = record.SeqNum;
                var toGap = seqNum - expected;
                if (toGap > 0)
                {
                    Gap(expected, seqNum, toResend);
                }
                expected = seqNum + 1;
                toResend.Add(record); 
            }
            if (endSeq - expected > 0)
            {
                Gap(expected, endSeq + 1, toResend);
            }
            return toResend;
        }

        private void Gap(int beginGap, int newSeq, List<IFixMsgStoreRecord> arr) {
            if (beginGap > 0) {
                var reset = SequenceResetGap(beginGap, newSeq);
                if (reset != null)
                {
                    arr.Add(reset);
                }
            }
        }

        /**
           * A continuous sequence of messages not being retransmitted should be skipped over using a
           * single SequenceReset(35=4) message with GapFillFlag(123) set to “Y” and MsgSeqNum(34) set
           * to the sequence number of the first skipped message and NewSeqNo(36) must always be set
           * to the value of the next sequence number to be expected by the peer immediately following
           * the messages being skipped.
           */

        private IFixMsgStoreRecord? SequenceResetGap(int startGap, int newSeq)
        {
            var gapFill = m_sessionFactory.SequenceReset(newSeq, true);
            var hdr = m_sessionFactory.Header(MsgType.SequenceReset, startGap, m_clock.Current);
            if (gapFill?.StandardHeader == null) return null;
            if (hdr == null) return null;
            var header = gapFill.StandardHeader;
            header.CopyFrom(hdr);

            header.OrigSendingTime = hdr.SendingTime;
            header.MsgSeqNum = startGap;
            header.PossDupFlag = true;
            header.SendingTime = m_clock.Current;
            
            var record = new FixMsgStoreRecord(MsgType.SequenceReset, m_clock.Current, startGap, null)
            { InflatedMessage = gapFill };
            return record;
        }

        private void Inflate(IFixMsgStoreRecord originalRecord)
        {
            if (originalRecord.Encoded == null) return;
            var bytes = Encoding.UTF8.GetBytes(originalRecord.Encoded);
            MsgView? msgView = null;
            m_Parser.ParseFrom(bytes, bytes.Length, (ptr, v) => msgView = v);
            if (msgView == null) return;
            var o = m_factory.ToFixMessage(msgView);
            if (o?.StandardHeader == null) return;
            originalRecord.InflatedMessage = o;
        }

    /**
      * Prepares the FIX message as response to ResendRequest (2).
      *
      * The FIX session processor retransmitting a message with the PossDupFlag(43) set to "Y" must modify the following fields:
      *
      * SendingTime(52) set to the current sending time
      * OrigSendingTime(122) set to the SendingTime(52) from the original message
      * Recalculate the BodyLength(9)
      * Recalculate the CheckSum(10)
      *
      * If the message is encrypted, SecureDataLen(90) and SecureData(91) may also require re-encryption and re-encoding
      *
      * @see https://www.fixtrading.org/standards/fix-session-layer-online/#message-recovery
      *
      * @param originalRecord the FIX message to be retransmitted as possible duplicate
      * @returns the FIX message ready to be retransmitted
      */
        private IFixMsgStoreRecord? PrepareRecordForRetransmission(IFixMsgStoreRecord originalRecord)
        {
            var factory = m_config.MessageFactory;
            if (factory == null) { return null; }
            if (originalRecord.Encoded == null) return null;
            var retransmitted = originalRecord.Clone(); // We don't want to accidently change any fields of the original record
            // Rebuilds header with the updated fields
            Inflate(retransmitted);
            var o = retransmitted.InflatedMessage;
            if (o?.StandardHeader == null) return null;
            o.StandardHeader.PossDupFlag = true;
            o.StandardHeader.OrigSendingTime = o.StandardHeader.SendingTime;
            o.StandardHeader.SendingTime = m_clock.Current;
            
            return retransmitted;
        }
    }
}
