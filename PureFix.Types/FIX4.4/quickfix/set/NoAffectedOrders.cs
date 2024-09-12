using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix.set
{
	public class NoAffectedOrders
	{
		public string? OrigClOrdID { get; set; } // 41 STRING
		public string? AffectedOrderID { get; set; } // 535 STRING
		public string? AffectedSecondaryOrderID { get; set; } // 536 STRING
	}
}
