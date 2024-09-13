using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class AllocGrpNoAllocsExt
	{
		public static void Parse(this AllocGrpNoAllocs instance, MsgView? view)
		{
			instance.AllocAccount = view?.GetString(79);
			instance.AllocAcctIDSource = view?.GetInt32(661);
			instance.MatchStatus = view?.GetString(573);
			instance.AllocPrice = view?.GetDouble(366);
			instance.AllocQty = view?.GetDouble(80);
			instance.IndividualAllocID = view?.GetString(467);
			instance.ProcessCode = view?.GetString(81);
			instance.NestedParties = new NestedParties();
			instance.NestedParties?.Parse(view?.GetView("NestedParties"));
			instance.NotifyBrokerOfCredit = view?.GetBool(208);
			instance.AllocHandlInst = view?.GetInt32(209);
			instance.AllocText = view?.GetString(161);
			instance.EncodedAllocTextLen = view?.GetInt32(360);
			instance.EncodedAllocText = view?.GetByteArray(361);
			instance.CommissionData = new CommissionData();
			instance.CommissionData?.Parse(view?.GetView("CommissionData"));
			instance.AllocAvgPx = view?.GetDouble(153);
			instance.AllocNetMoney = view?.GetDouble(154);
			instance.SettlCurrAmt = view?.GetDouble(119);
			instance.AllocSettlCurrAmt = view?.GetDouble(737);
			instance.SettlCurrency = view?.GetString(120);
			instance.AllocSettlCurrency = view?.GetString(736);
			instance.SettlCurrFxRate = view?.GetDouble(155);
			instance.SettlCurrFxRateCalc = view?.GetString(156);
			instance.AllocAccruedInterestAmt = view?.GetDouble(742);
			instance.AllocInterestAtMaturity = view?.GetDouble(741);
			instance.MiscFeesGrp = new MiscFeesGrp();
			instance.MiscFeesGrp?.Parse(view?.GetView("MiscFeesGrp"));
			instance.ClrInstGrp = new ClrInstGrp();
			instance.ClrInstGrp?.Parse(view?.GetView("ClrInstGrp"));
			instance.AllocSettlInstType = view?.GetInt32(780);
			instance.SettlInstructionsData = new SettlInstructionsData();
			instance.SettlInstructionsData?.Parse(view?.GetView("SettlInstructionsData"));
		}
	}
}
