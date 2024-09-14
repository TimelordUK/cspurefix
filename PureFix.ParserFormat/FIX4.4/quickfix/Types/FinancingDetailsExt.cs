using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class FinancingDetailsExt
	{
		public static void Parse(this FinancingDetails instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.AgreementDesc = view.GetString(913);
			instance.AgreementID = view.GetString(914);
			instance.AgreementDate = view.GetDateOnly(915);
			instance.AgreementCurrency = view.GetString(918);
			instance.TerminationType = view.GetInt32(788);
			instance.StartDate = view.GetDateOnly(916);
			instance.EndDate = view.GetDateOnly(917);
			instance.DeliveryType = view.GetInt32(919);
			instance.MarginRatio = view.GetDouble(898);
		}
	}
}
