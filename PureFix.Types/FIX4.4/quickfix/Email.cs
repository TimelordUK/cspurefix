using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix
{
	public class Email : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? EmailThreadID { get; set; } // 164 STRING
		public string? EmailType { get; set; } // 94 CHAR
		public DateTime? OrigTime { get; set; } // 42 UTCTIMESTAMP
		public string? Subject { get; set; } // 147 STRING
		public int? EncodedSubjectLen { get; set; } // 356 LENGTH
		public byte[]? EncodedSubject { get; set; } // 357 DATA
		public RoutingGrp? RoutingGrp { get; set; }
		public InstrmtGrp? InstrmtGrp { get; set; }
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		public string? OrderID { get; set; } // 37 STRING
		public string? ClOrdID { get; set; } // 11 STRING
		public LinesOfTextGrp? LinesOfTextGrp { get; set; }
		public int? RawDataLength { get; set; } // 95 LENGTH
		public byte[]? RawData { get; set; } // 96 DATA
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
