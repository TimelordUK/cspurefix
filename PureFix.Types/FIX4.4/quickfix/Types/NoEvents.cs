using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public class NoEvents
	{
		public int? EventType { get; set; } // 865 INT
		public DateTime? EventDate { get; set; } // 866 LOCALMKTDATE
		public double? EventPx { get; set; } // 867 PRICE
		public string? EventText { get; set; } // 868 STRING
	}
}
