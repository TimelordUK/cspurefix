using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoContraBrokers
	{
		public string? ContraBroker { get; set; } // 375 STRING
		public string? ContraTrader { get; set; } // 337 STRING
		public double? ContraTradeQty { get; set; } // 437 QTY
		public DateTime? ContraTradeTime { get; set; } // 438 UTCTIMESTAMP
		public string? ContraLegRefID { get; set; } // 655 STRING
	}
}
