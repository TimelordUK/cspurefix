using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class RegistrationInstructionsResponse : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? RegistID { get; set; } // 513 STRING
		public string? RegistTransType { get; set; } // 514 CHAR
		public string? RegistRefID { get; set; } // 508 STRING
		public string? ClOrdID { get; set; } // 11 STRING
		public Parties? Parties { get; set; }
		public string? Account { get; set; } // 1 STRING
		public int? AcctIDSource { get; set; } // 660 INT
		public string? RegistStatus { get; set; } // 506 CHAR
		public int? RegistRejReasonCode { get; set; } // 507 INT
		public string? RegistRejReasonText { get; set; } // 496 STRING
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
