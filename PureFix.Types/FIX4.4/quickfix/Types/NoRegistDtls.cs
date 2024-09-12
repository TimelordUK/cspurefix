using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoRegistDtls
	{
		[TagDetails(509)]
		public string? RegistDtls { get; set; } // STRING
		
		[TagDetails(511)]
		public string? RegistEmail { get; set; } // STRING
		
		[TagDetails(474)]
		public string? MailingDtls { get; set; } // STRING
		
		[TagDetails(482)]
		public string? MailingInst { get; set; } // STRING
		
		public NestedParties? NestedParties { get; set; }
		[TagDetails(522)]
		public int? OwnerType { get; set; } // INT
		
		[TagDetails(486)]
		public DateTime? DateOfBirth { get; set; } // LOCALMKTDATE
		
		[TagDetails(475)]
		public string? InvestorCountryOfResidence { get; set; } // COUNTRY
		
	}
}
