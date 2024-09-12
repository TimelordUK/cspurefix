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
		[TagDetails(375)]
		public string? ContraBroker { get; set; } // STRING
		
		[TagDetails(337)]
		public string? ContraTrader { get; set; } // STRING
		
		[TagDetails(437)]
		public double? ContraTradeQty { get; set; } // QTY
		
		[TagDetails(438)]
		public DateTime? ContraTradeTime { get; set; } // UTCTIMESTAMP
		
		[TagDetails(655)]
		public string? ContraLegRefID { get; set; } // STRING
		
	}
}
