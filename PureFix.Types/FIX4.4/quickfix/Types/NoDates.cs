using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoDates
	{
		[TagDetails(Tag = 75, Type = TagType.LocalDate, Offset = 0, Required = false)]
		public DateTime? TradeDate { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 1, Required = false)]
		public DateTime? TransactTime { get; set; }
		
	}
}
