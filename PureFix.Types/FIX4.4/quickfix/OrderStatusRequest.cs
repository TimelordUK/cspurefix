using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class OrderStatusRequest : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(37)]
		public string? OrderID { get; set; } // STRING
		
		[TagDetails(11)]
		public string? ClOrdID { get; set; } // STRING
		
		[TagDetails(526)]
		public string? SecondaryClOrdID { get; set; } // STRING
		
		[TagDetails(583)]
		public string? ClOrdLinkID { get; set; } // STRING
		
		public Parties? Parties { get; set; }
		[TagDetails(790)]
		public string? OrdStatusReqID { get; set; } // STRING
		
		[TagDetails(1)]
		public string? Account { get; set; } // STRING
		
		[TagDetails(660)]
		public int? AcctIDSource { get; set; } // INT
		
		public Instrument? Instrument { get; set; }
		public FinancingDetails? FinancingDetails { get; set; }
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		[TagDetails(54)]
		public string? Side { get; set; } // CHAR
		
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
