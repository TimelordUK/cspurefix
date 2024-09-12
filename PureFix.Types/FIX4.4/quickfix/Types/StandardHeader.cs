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
		[TagDetails(Tag = 8, Type = TagType.String, Offset = 0)]
		public string? BeginString { get; set; }
		
		[TagDetails(Tag = 9, Type = TagType.Length, Offset = 1)]
		public int? BodyLength { get; set; }
		
		[TagDetails(Tag = 35, Type = TagType.String, Offset = 2)]
		public string? MsgType { get; set; }
		
		[TagDetails(Tag = 49, Type = TagType.String, Offset = 3)]
		public string? SenderCompID { get; set; }
		
		[TagDetails(Tag = 56, Type = TagType.String, Offset = 4)]
		public string? TargetCompID { get; set; }
		
		[TagDetails(Tag = 115, Type = TagType.String, Offset = 5)]
		public string? OnBehalfOfCompID { get; set; }
		
		[TagDetails(Tag = 128, Type = TagType.String, Offset = 6)]
		public string? DeliverToCompID { get; set; }
		
		[TagDetails(Tag = 90, Type = TagType.Length, Offset = 7)]
		public int? SecureDataLen { get; set; }
		
		[TagDetails(Tag = 91, Type = TagType.RawData, Offset = 8)]
		public byte[]? SecureData { get; set; }
		
		[TagDetails(Tag = 34, Type = TagType.Int, Offset = 9)]
		public int? MsgSeqNum { get; set; }
		
		[TagDetails(Tag = 50, Type = TagType.String, Offset = 10)]
		public string? SenderSubID { get; set; }
		
		[TagDetails(Tag = 142, Type = TagType.String, Offset = 11)]
		public string? SenderLocationID { get; set; }
		
		[TagDetails(Tag = 57, Type = TagType.String, Offset = 12)]
		public string? TargetSubID { get; set; }
		
		[TagDetails(Tag = 143, Type = TagType.String, Offset = 13)]
		public string? TargetLocationID { get; set; }
		
		[TagDetails(Tag = 116, Type = TagType.String, Offset = 14)]
		public string? OnBehalfOfSubID { get; set; }
		
		[TagDetails(Tag = 144, Type = TagType.String, Offset = 15)]
		public string? OnBehalfOfLocationID { get; set; }
		
		[TagDetails(Tag = 129, Type = TagType.String, Offset = 16)]
		public string? DeliverToSubID { get; set; }
		
		[TagDetails(Tag = 145, Type = TagType.String, Offset = 17)]
		public string? DeliverToLocationID { get; set; }
		
		[TagDetails(Tag = 43, Type = TagType.Boolean, Offset = 18)]
		public bool? PossDupFlag { get; set; }
		
		[TagDetails(Tag = 97, Type = TagType.Boolean, Offset = 19)]
		public bool? PossResend { get; set; }
		
		[TagDetails(Tag = 52, Type = TagType.UtcTimestamp, Offset = 20)]
		public DateTime? SendingTime { get; set; }
		
		[TagDetails(Tag = 122, Type = TagType.UtcTimestamp, Offset = 21)]
		public DateTime? OrigSendingTime { get; set; }
		
		[TagDetails(Tag = 212, Type = TagType.Length, Offset = 22)]
		public int? XmlDataLen { get; set; }
		
		[TagDetails(Tag = 213, Type = TagType.RawData, Offset = 23)]
		public byte[]? XmlData { get; set; }
		
		[TagDetails(Tag = 347, Type = TagType.String, Offset = 24)]
		public string? MessageEncoding { get; set; }
		
		[TagDetails(Tag = 369, Type = TagType.Int, Offset = 25)]
		public int? LastMsgSeqNumProcessed { get; set; }
		
		[Component(Offset = 26)]
		public Hop? Hop { get; set; }
		
	}
}
