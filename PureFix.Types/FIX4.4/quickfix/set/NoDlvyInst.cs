using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix.set
{
	public class NoDlvyInst
	{
		public string? SettlInstSource { get; set; } // 165 CHAR
		public string? DlvyInstType { get; set; } // 787 CHAR
		public SettlParties? SettlParties { get; set; }
	}
}
