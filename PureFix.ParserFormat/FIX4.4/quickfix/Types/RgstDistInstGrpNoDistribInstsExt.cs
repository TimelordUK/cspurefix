using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class RgstDistInstGrpNoDistribInstsExt
	{
		public static void Parse(this RgstDistInstGrpNoDistribInsts instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.DistribPaymentMethod = view.GetInt32(477);
			instance.DistribPercentage = view.GetDouble(512);
			instance.CashDistribCurr = view.GetString(478);
			instance.CashDistribAgentName = view.GetString(498);
			instance.CashDistribAgentCode = view.GetString(499);
			instance.CashDistribAgentAcctNumber = view.GetString(500);
			instance.CashDistribPayRef = view.GetString(501);
			instance.CashDistribAgentAcctName = view.GetString(502);
		}
	}
}
