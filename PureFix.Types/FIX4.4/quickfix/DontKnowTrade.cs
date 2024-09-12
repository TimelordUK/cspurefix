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
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(37, TagType.String)]
		public string? OrderID { get; set; }
		
		[TagDetails(198, TagType.String)]
		public string? SecondaryOrderID { get; set; }
		
		[TagDetails(17, TagType.String)]
		public string? ExecID { get; set; }
		
		[TagDetails(127, TagType.String)]
		public string? DKReason { get; set; }
		
		[Component]
		public Instrument? Instrument { get; set; }
		
		[Component]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[Component]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[TagDetails(54, TagType.String)]
		public string? Side { get; set; }
		
		[Component]
		public OrderQtyData? OrderQtyData { get; set; }
		
		[TagDetails(32, TagType.Float)]
		public double? LastQty { get; set; }
		
		[TagDetails(31, TagType.Float)]
		public double? LastPx { get; set; }
		
		[TagDetails(58, TagType.String)]
		public string? Text { get; set; }
		
		[TagDetails(354, TagType.Length)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(355, TagType.RawData)]
		public byte[]? EncodedText { get; set; }
		
		[Component]
		public override StandardTrailer? StandardTrailer { get; set; }
		
	}
}
