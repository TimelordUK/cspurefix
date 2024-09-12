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
		public DateTime? OrigTime { get; set; } // 42 UTCTIMESTAMP
		public string? Urgency { get; set; } // 61 CHAR
		public string? Headline { get; set; } // 148 STRING
		public int? EncodedHeadlineLen { get; set; } // 358 LENGTH
		public byte[]? EncodedHeadline { get; set; } // 359 DATA
		public RoutingGrp? RoutingGrp { get; set; }
		public InstrmtGrp? InstrmtGrp { get; set; }
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		public LinesOfTextGrp? LinesOfTextGrp { get; set; }
		public string? URLLink { get; set; } // 149 STRING
		public int? RawDataLength { get; set; } // 95 LENGTH
		public byte[]? RawData { get; set; } // 96 DATA
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
