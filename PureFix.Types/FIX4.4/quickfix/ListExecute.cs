using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class ListExecute : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(66)]
		public string? ListID { get; set; } // STRING
		
		[TagDetails(391)]
		public string? ClientBidID { get; set; } // STRING
		
		[TagDetails(390)]
		public string? BidID { get; set; } // STRING
		
		[TagDetails(60)]
		public DateTime? TransactTime { get; set; } // UTCTIMESTAMP
		
		[TagDetails(58)]
		public string? Text { get; set; } // STRING
		
		[TagDetails(354)]
		public int? EncodedTextLen { get; set; } // LENGTH
		
		[TagDetails(355)]
		public byte[]? EncodedText { get; set; } // DATA
		
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
