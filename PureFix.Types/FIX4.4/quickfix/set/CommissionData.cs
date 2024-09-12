using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix.set
{
	public class CommissionData
	{
		public double? Commission { get; set; } // 12 AMT
		public string? CommType { get; set; } // 13 CHAR
		public string? CommCurrency { get; set; } // 479 CURRENCY
		public string? FundRenewWaiv { get; set; } // 497 CHAR
	}
}
