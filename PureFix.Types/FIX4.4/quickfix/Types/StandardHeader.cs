using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class StandardHeader
	{
		[TagDetails(8, TagType.String)]
		public string? BeginString { get; set; }
		
		[TagDetails(9, TagType.Length)]
		public int? BodyLength { get; set; }
		
		[TagDetails(35, TagType.String)]
		public string? MsgType { get; set; }
		
		[TagDetails(49, TagType.String)]
		public string? SenderCompID { get; set; }
		
		[TagDetails(56, TagType.String)]
		public string? TargetCompID { get; set; }
		
		[TagDetails(115, TagType.String)]
		public string? OnBehalfOfCompID { get; set; }
		
		[TagDetails(128, TagType.String)]
		public string? DeliverToCompID { get; set; }
		
		[TagDetails(90, TagType.Length)]
		public int? SecureDataLen { get; set; }
		
		[TagDetails(91, TagType.RawData)]
		public byte[]? SecureData { get; set; }
		
		[TagDetails(34, TagType.Int)]
		public int? MsgSeqNum { get; set; }
		
		[TagDetails(50, TagType.String)]
		public string? SenderSubID { get; set; }
		
		[TagDetails(142, TagType.String)]
		public string? SenderLocationID { get; set; }
		
		[TagDetails(57, TagType.String)]
		public string? TargetSubID { get; set; }
		
		[TagDetails(143, TagType.String)]
		public string? TargetLocationID { get; set; }
		
		[TagDetails(116, TagType.String)]
		public string? OnBehalfOfSubID { get; set; }
		
		[TagDetails(144, TagType.String)]
		public string? OnBehalfOfLocationID { get; set; }
		
		[TagDetails(129, TagType.String)]
		public string? DeliverToSubID { get; set; }
		
		[TagDetails(145, TagType.String)]
		public string? DeliverToLocationID { get; set; }
		
		[TagDetails(43, TagType.Boolean)]
		public bool? PossDupFlag { get; set; }
		
		[TagDetails(97, TagType.Boolean)]
		public bool? PossResend { get; set; }
		
		[TagDetails(52, TagType.UtcTimestamp)]
		public DateTime? SendingTime { get; set; }
		
		[TagDetails(122, TagType.UtcTimestamp)]
		public DateTime? OrigSendingTime { get; set; }
		
		[TagDetails(212, TagType.Length)]
		public int? XmlDataLen { get; set; }
		
		[TagDetails(213, TagType.RawData)]
		public byte[]? XmlData { get; set; }
		
		[TagDetails(347, TagType.String)]
		public string? MessageEncoding { get; set; }
		
		[TagDetails(369, TagType.Int)]
		public int? LastMsgSeqNumProcessed { get; set; }
		
		[Component]
		public Hop? Hop { get; set; }
		
	}
}
