using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class DontKnowTrade : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(37)]
		public string? OrderID { get; set; } // STRING
		
		[TagDetails(198)]
		public string? SecondaryOrderID { get; set; } // STRING
		
		[TagDetails(17)]
		public string? ExecID { get; set; } // STRING
		
		[TagDetails(127)]
		public string? DKReason { get; set; } // CHAR
		
		public Instrument? Instrument { get; set; }
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		[TagDetails(54)]
		public string? Side { get; set; } // CHAR
		
		public OrderQtyData? OrderQtyData { get; set; }
		[TagDetails(32)]
		public double? LastQty { get; set; } // QTY
		
		[TagDetails(31)]
		public double? LastPx { get; set; } // PRICE
		
		[TagDetails(58)]
		public string? Text { get; set; } // STRING
		
		[TagDetails(354)]
		public int? EncodedTextLen { get; set; } // LENGTH
		
		[TagDetails(355)]
		public byte[]? EncodedText { get; set; } // DATA
		
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
