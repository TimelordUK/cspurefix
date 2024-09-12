using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class OrderCancelRequest : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(41)]
		public string? OrigClOrdID { get; set; } // STRING
		
		[TagDetails(37)]
		public string? OrderID { get; set; } // STRING
		
		[TagDetails(11)]
		public string? ClOrdID { get; set; } // STRING
		
		[TagDetails(526)]
		public string? SecondaryClOrdID { get; set; } // STRING
		
		[TagDetails(583)]
		public string? ClOrdLinkID { get; set; } // STRING
		
		[TagDetails(66)]
		public string? ListID { get; set; } // STRING
		
		[TagDetails(586)]
		public DateTime? OrigOrdModTime { get; set; } // UTCTIMESTAMP
		
		[TagDetails(1)]
		public string? Account { get; set; } // STRING
		
		[TagDetails(660)]
		public int? AcctIDSource { get; set; } // INT
		
		[TagDetails(581)]
		public int? AccountType { get; set; } // INT
		
		public Parties? Parties { get; set; }
		public Instrument? Instrument { get; set; }
		public FinancingDetails? FinancingDetails { get; set; }
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		[TagDetails(54)]
		public string? Side { get; set; } // CHAR
		
		[TagDetails(60)]
		public DateTime? TransactTime { get; set; } // UTCTIMESTAMP
		
		public OrderQtyData? OrderQtyData { get; set; }
		[TagDetails(376)]
		public string? ComplianceID { get; set; } // STRING
		
		[TagDetails(58)]
		public string? Text { get; set; } // STRING
		
		[TagDetails(354)]
		public int? EncodedTextLen { get; set; } // LENGTH
		
		[TagDetails(355)]
		public byte[]? EncodedText { get; set; } // DATA
		
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
