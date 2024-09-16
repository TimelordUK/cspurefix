using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public static class CommissionDataExt
	{
		public static void Parse(this CommissionData instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.Commission = view.GetDouble(12);
			instance.CommType = view.GetString(13);
			instance.CommCurrency = view.GetString(479);
			instance.FundRenewWaiv = view.GetString(497);
		}
	}
}
