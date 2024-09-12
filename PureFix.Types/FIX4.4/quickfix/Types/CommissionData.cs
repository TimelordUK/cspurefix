using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class CommissionData
	{
		[TagDetails(12)]
		public double? Commission { get; set; } // AMT
		
		[TagDetails(13)]
		public string? CommType { get; set; } // CHAR
		
		[TagDetails(479)]
		public string? CommCurrency { get; set; } // CURRENCY
		
		[TagDetails(497)]
		public string? FundRenewWaiv { get; set; } // CHAR
		
	}
}
