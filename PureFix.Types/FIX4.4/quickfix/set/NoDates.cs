using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix.set
{
	public class NoDates
	{
		public DateTime? TradeDate { get; set; } // 75 LOCALMKTDATE
		public DateTime? TransactTime { get; set; } // 60 UTCTIMESTAMP
	}
}
