using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.Validation;

namespace PureFix.Types.Config
{
    public class SessionDescription : ISessionDescription
    {
        public MsgApplication? Application { get; set; }
        public string? Name { get; set; }
        public string? SenderCompID { get; set; }
        public string? TargetCompID { get; set; }
        public bool? ResetSeqNumFlag { get; set; }
        public string? SenderSubID { get; set; }
        public string? TargetSubID { get; set; }
        public string? BeginString { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public int? LastSentSeqNum { get; set; }
        public int? LastReceivedSeqNum { get; set; }
        public int? BodyLengthChars { get; set; }
        public int? HeartBtInt { get; set; }
        public int? MsgSeqNum { get; set; }
        public int? PeerSeqNum { get; set; }

        /// <summary>
        /// Logging configuration. If null, defaults are used.
        /// </summary>
        public LoggingConfig? Logging { get; set; }

        /// <summary>
        /// Message store configuration. If null, in-memory store is used.
        /// </summary>
        public StoreConfig? Store { get; set; }

        /// <summary>
        /// If true, always respond to ResendRequest with GapFill instead of replaying stored messages.
        /// This is a critical safety feature for clients/initiators to prevent accidentally resending
        /// old orders which could cause duplicate executions. Default is false for backwards compatibility.
        /// Recommended: Set to true for all client/initiator applications.
        /// </summary>
        public bool? ResendGapFillOnly { get; set; }

        /// <summary>
        /// Validation configuration for message parsing and processing.
        /// If null, uses legacy validation (checksum only for acceptors, none for initiators).
        /// </summary>
        public ValidationConfig? Validation { get; set; }

        /// <summary>
        /// Creates a shallow clone of this SessionDescription.
        /// Used for wildcard TargetCompID mode where each acceptor session needs its own
        /// description to track its specific counterparty CompID.
        /// </summary>
        public SessionDescription Clone()
        {
            return new SessionDescription
            {
                Application = Application, // Shared - TCP settings don't change per session
                Name = Name,
                SenderCompID = SenderCompID,
                TargetCompID = TargetCompID,
                ResetSeqNumFlag = ResetSeqNumFlag,
                SenderSubID = SenderSubID,
                TargetSubID = TargetSubID,
                BeginString = BeginString,
                Username = Username,
                Password = Password,
                LastSentSeqNum = LastSentSeqNum,
                LastReceivedSeqNum = LastReceivedSeqNum,
                BodyLengthChars = BodyLengthChars,
                HeartBtInt = HeartBtInt,
                MsgSeqNum = MsgSeqNum,
                PeerSeqNum = PeerSeqNum,
                Logging = Logging,
                Store = Store,
                ResendGapFillOnly = ResendGapFillOnly,
                Validation = Validation
            };
        }
    }
}
