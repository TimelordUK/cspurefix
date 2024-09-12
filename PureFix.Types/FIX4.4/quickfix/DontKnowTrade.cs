using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix
{
	public class DontKnowTrade : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? OrderID { get; set; } // 37 STRING
		public string? SecondaryOrderID { get; set; } // 198 STRING
		public string? ExecID { get; set; } // 17 STRING
		public string? DKReason { get; set; } // 127 CHAR
		public Instrument? Instrument { get; set; }
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		public string? Side { get; set; } // 54 CHAR
		public OrderQtyData? OrderQtyData { get; set; }
		public double? LastQty { get; set; } // 32 QTY
		public double? LastPx { get; set; } // 31 PRICE
		public string? Text { get; set; } // 58 STRING
		public int? EncodedTextLen { get; set; } // 354 LENGTH
		public byte[]? EncodedText { get; set; } // 355 DATA
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
