using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public class NoDlvyInst
	{
		public string? SettlInstSource { get; set; } // 165 CHAR
		public string? DlvyInstType { get; set; } // 787 CHAR
		public SettlParties? SettlParties { get; set; }
	}
}
