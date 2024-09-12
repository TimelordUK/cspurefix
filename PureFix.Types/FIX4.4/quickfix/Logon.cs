using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class Logon : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(98)]
		public int? EncryptMethod { get; set; } // INT
		
		[TagDetails(108)]
		public int? HeartBtInt { get; set; } // INT
		
		[TagDetails(95)]
		public int? RawDataLength { get; set; } // LENGTH
		
		[TagDetails(96)]
		public byte[]? RawData { get; set; } // DATA
		
		[TagDetails(141)]
		public bool? ResetSeqNumFlag { get; set; } // BOOLEAN
		
		[TagDetails(789)]
		public int? NextExpectedMsgSeqNum { get; set; } // SEQNUM
		
		[TagDetails(383)]
		public int? MaxMessageSize { get; set; } // LENGTH
		
		public NoMsgTypes? NoMsgTypes { get; set; }
		[TagDetails(464)]
		public bool? TestMessageIndicator { get; set; } // BOOLEAN
		
		[TagDetails(553)]
		public string? Username { get; set; } // STRING
		
		[TagDetails(554)]
		public string? Password { get; set; } // STRING
		
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
