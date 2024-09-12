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
		public int? DistribPaymentMethod { get; set; } // 477 INT
		public double? DistribPercentage { get; set; } // 512 PERCENTAGE
		public string? CashDistribCurr { get; set; } // 478 CURRENCY
		public string? CashDistribAgentName { get; set; } // 498 STRING
		public string? CashDistribAgentCode { get; set; } // 499 STRING
		public string? CashDistribAgentAcctNumber { get; set; } // 500 STRING
		public string? CashDistribPayRef { get; set; } // 501 STRING
		public string? CashDistribAgentAcctName { get; set; } // 502 STRING
	}
}
