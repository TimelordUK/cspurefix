using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class News : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(42)]
		public DateTime? OrigTime { get; set; } // UTCTIMESTAMP
		
		[TagDetails(61)]
		public string? Urgency { get; set; } // CHAR
		
		[TagDetails(148)]
		public string? Headline { get; set; } // STRING
		
		[TagDetails(358)]
		public int? EncodedHeadlineLen { get; set; } // LENGTH
		
		[TagDetails(359)]
		public byte[]? EncodedHeadline { get; set; } // DATA
		
		public RoutingGrp? RoutingGrp { get; set; }
		public InstrmtGrp? InstrmtGrp { get; set; }
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		public LinesOfTextGrp? LinesOfTextGrp { get; set; }
		[TagDetails(149)]
		public string? URLLink { get; set; } // STRING
		
		[TagDetails(95)]
		public int? RawDataLength { get; set; } // LENGTH
		
		[TagDetails(96)]
		public byte[]? RawData { get; set; } // DATA
		
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
