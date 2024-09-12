using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix.set
{
	public class FinancingDetails
	{
		public string? AgreementDesc { get; set; } // 913 STRING
		public string? AgreementID { get; set; } // 914 STRING
		public DateTime? AgreementDate { get; set; } // 915 LOCALMKTDATE
		public string? AgreementCurrency { get; set; } // 918 CURRENCY
		public int? TerminationType { get; set; } // 788 INT
		public DateTime? StartDate { get; set; } // 916 LOCALMKTDATE
		public DateTime? EndDate { get; set; } // 917 LOCALMKTDATE
		public int? DeliveryType { get; set; } // 919 INT
		public double? MarginRatio { get; set; } // 898 PERCENTAGE
	}
}
