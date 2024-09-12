using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class Email : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(164)]
		public string? EmailThreadID { get; set; } // STRING
		
		[TagDetails(94)]
		public string? EmailType { get; set; } // CHAR
		
		[TagDetails(42)]
		public DateTime? OrigTime { get; set; } // UTCTIMESTAMP
		
		[TagDetails(147)]
		public string? Subject { get; set; } // STRING
		
		[TagDetails(356)]
		public int? EncodedSubjectLen { get; set; } // LENGTH
		
		[TagDetails(357)]
		public byte[]? EncodedSubject { get; set; } // DATA
		
		public RoutingGrp? RoutingGrp { get; set; }
		public InstrmtGrp? InstrmtGrp { get; set; }
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		[TagDetails(37)]
		public string? OrderID { get; set; } // STRING
		
		[TagDetails(11)]
		public string? ClOrdID { get; set; } // STRING
		
		public LinesOfTextGrp? LinesOfTextGrp { get; set; }
		[TagDetails(95)]
		public int? RawDataLength { get; set; } // LENGTH
		
		[TagDetails(96)]
		public byte[]? RawData { get; set; } // DATA
		
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
