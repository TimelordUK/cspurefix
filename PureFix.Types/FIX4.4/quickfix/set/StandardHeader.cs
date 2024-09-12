using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix.set
{
	public class StandardHeader
	{
		public string? BeginString { get; set; }
		public int? BodyLength { get; set; }
		public string? MsgType { get; set; }
		public string? SenderCompID { get; set; }
		public string? TargetCompID { get; set; }
		public string? OnBehalfOfCompID { get; set; }
		public string? DeliverToCompID { get; set; }
		public int? SecureDataLen { get; set; }
		public byte[]? SecureData { get; set; }
		public int? MsgSeqNum { get; set; }
		public string? SenderSubID { get; set; }
		public string? SenderLocationID { get; set; }
		public string? TargetSubID { get; set; }
		public string? TargetLocationID { get; set; }
		public string? OnBehalfOfSubID { get; set; }
		public string? OnBehalfOfLocationID { get; set; }
		public string? DeliverToSubID { get; set; }
		public string? DeliverToLocationID { get; set; }
		public bool? PossDupFlag { get; set; }
		public bool? PossResend { get; set; }
		public DateTime? SendingTime { get; set; }
		public DateTime? OrigSendingTime { get; set; }
		public int? XmlDataLen { get; set; }
		public byte[]? XmlData { get; set; }
		public string? MessageEncoding { get; set; }
		public int? LastMsgSeqNumProcessed { get; set; }
		public Hop? Hop { get; set; }
	}
}
