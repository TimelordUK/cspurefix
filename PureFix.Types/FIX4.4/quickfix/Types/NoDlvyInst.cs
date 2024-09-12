using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoDlvyInst
	{
		[TagDetails(165)]
		public string? SettlInstSource { get; set; } // CHAR
		
		[TagDetails(787)]
		public string? DlvyInstType { get; set; } // CHAR
		
		public SettlParties? SettlParties { get; set; }
	}
}
