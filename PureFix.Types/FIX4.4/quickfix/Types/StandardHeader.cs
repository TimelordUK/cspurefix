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
		[TagDetails(8)]
		public string? BeginString { get; set; } // STRING
		
		[TagDetails(9)]
		public int? BodyLength { get; set; } // LENGTH
		
		[TagDetails(35)]
		public string? MsgType { get; set; } // STRING
		
		[TagDetails(49)]
		public string? SenderCompID { get; set; } // STRING
		
		[TagDetails(56)]
		public string? TargetCompID { get; set; } // STRING
		
		[TagDetails(115)]
		public string? OnBehalfOfCompID { get; set; } // STRING
		
		[TagDetails(128)]
		public string? DeliverToCompID { get; set; } // STRING
		
		[TagDetails(90)]
		public int? SecureDataLen { get; set; } // LENGTH
		
		[TagDetails(91)]
		public byte[]? SecureData { get; set; } // DATA
		
		[TagDetails(34)]
		public int? MsgSeqNum { get; set; } // SEQNUM
		
		[TagDetails(50)]
		public string? SenderSubID { get; set; } // STRING
		
		[TagDetails(142)]
		public string? SenderLocationID { get; set; } // STRING
		
		[TagDetails(57)]
		public string? TargetSubID { get; set; } // STRING
		
		[TagDetails(143)]
		public string? TargetLocationID { get; set; } // STRING
		
		[TagDetails(116)]
		public string? OnBehalfOfSubID { get; set; } // STRING
		
		[TagDetails(144)]
		public string? OnBehalfOfLocationID { get; set; } // STRING
		
		[TagDetails(129)]
		public string? DeliverToSubID { get; set; } // STRING
		
		[TagDetails(145)]
		public string? DeliverToLocationID { get; set; } // STRING
		
		[TagDetails(43)]
		public bool? PossDupFlag { get; set; } // BOOLEAN
		
		[TagDetails(97)]
		public bool? PossResend { get; set; } // BOOLEAN
		
		[TagDetails(52)]
		public DateTime? SendingTime { get; set; } // UTCTIMESTAMP
		
		[TagDetails(122)]
		public DateTime? OrigSendingTime { get; set; } // UTCTIMESTAMP
		
		[TagDetails(212)]
		public int? XmlDataLen { get; set; } // LENGTH
		
		[TagDetails(213)]
		public byte[]? XmlData { get; set; } // DATA
		
		[TagDetails(347)]
		public string? MessageEncoding { get; set; } // STRING
		
		[TagDetails(369)]
		public int? LastMsgSeqNumProcessed { get; set; } // SEQNUM
		
		public Hop? Hop { get; set; }
	}
}
