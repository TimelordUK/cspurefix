using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class QuoteRequest : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(131)]
		public string? QuoteReqID { get; set; } // STRING
		
		[TagDetails(644)]
		public string? RFQReqID { get; set; } // STRING
		
		[TagDetails(11)]
		public string? ClOrdID { get; set; } // STRING
		
		[TagDetails(528)]
		public string? OrderCapacity { get; set; } // CHAR
		
		public QuotReqGrp? QuotReqGrp { get; set; }
		[TagDetails(58)]
		public string? Text { get; set; } // STRING
		
		[TagDetails(354)]
		public int? EncodedTextLen { get; set; } // LENGTH
		
		[TagDetails(355)]
		public byte[]? EncodedText { get; set; } // DATA
		
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
