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
		[TagDetails(320)]
		public string? SecurityReqID { get; set; } // STRING
		
		[TagDetails(322)]
		public string? SecurityResponseID { get; set; } // STRING
		
		[TagDetails(560)]
		public int? SecurityRequestResult { get; set; } // INT
		
		[TagDetails(393)]
		public int? TotNoRelatedSym { get; set; } // INT
		
		[TagDetails(893)]
		public bool? LastFragment { get; set; } // BOOLEAN
		
		public SecListGrp? SecListGrp { get; set; }
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
