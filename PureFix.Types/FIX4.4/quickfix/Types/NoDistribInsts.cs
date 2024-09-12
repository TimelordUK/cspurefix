using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoDistribInsts
	{
		[TagDetails(477)]
		public int? DistribPaymentMethod { get; set; } // INT
		
		[TagDetails(512)]
		public double? DistribPercentage { get; set; } // PERCENTAGE
		
		[TagDetails(478)]
		public string? CashDistribCurr { get; set; } // CURRENCY
		
		[TagDetails(498)]
		public string? CashDistribAgentName { get; set; } // STRING
		
		[TagDetails(499)]
		public string? CashDistribAgentCode { get; set; } // STRING
		
		[TagDetails(500)]
		public string? CashDistribAgentAcctNumber { get; set; } // STRING
		
		[TagDetails(501)]
		public string? CashDistribPayRef { get; set; } // STRING
		
		[TagDetails(502)]
		public string? CashDistribAgentAcctName { get; set; } // STRING
		
	}
}
