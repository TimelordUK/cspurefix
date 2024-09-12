using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public class NoRegistDtls
	{
		public string? RegistDtls { get; set; } // 509 STRING
		public string? RegistEmail { get; set; } // 511 STRING
		public string? MailingDtls { get; set; } // 474 STRING
		public string? MailingInst { get; set; } // 482 STRING
		public NestedParties? NestedParties { get; set; }
		public int? OwnerType { get; set; } // 522 INT
		public DateTime? DateOfBirth { get; set; } // 486 LOCALMKTDATE
		public string? InvestorCountryOfResidence { get; set; } // 475 COUNTRY
	}
}
