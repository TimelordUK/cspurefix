using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class SecurityList : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? SecurityReqID { get; set; } // 320 STRING
		public string? SecurityResponseID { get; set; } // 322 STRING
		public int? SecurityRequestResult { get; set; } // 560 INT
		public int? TotNoRelatedSym { get; set; } // 393 INT
		public bool? LastFragment { get; set; } // 893 BOOLEAN
		public SecListGrp? SecListGrp { get; set; }
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
