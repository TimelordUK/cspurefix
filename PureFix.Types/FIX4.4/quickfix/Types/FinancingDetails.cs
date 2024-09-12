using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class FinancingDetails
	{
		[TagDetails(913)]
		public string? AgreementDesc { get; set; } // STRING
		
		[TagDetails(914)]
		public string? AgreementID { get; set; } // STRING
		
		[TagDetails(915)]
		public DateTime? AgreementDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(918)]
		public string? AgreementCurrency { get; set; } // CURRENCY
		
		[TagDetails(788)]
		public int? TerminationType { get; set; } // INT
		
		[TagDetails(916)]
		public DateTime? StartDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(917)]
		public DateTime? EndDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(919)]
		public int? DeliveryType { get; set; } // INT
		
		[TagDetails(898)]
		public double? MarginRatio { get; set; } // PERCENTAGE
		
	}
}
