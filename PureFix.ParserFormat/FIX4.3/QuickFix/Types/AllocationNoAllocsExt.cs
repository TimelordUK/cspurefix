using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public static class AllocationNoAllocsExt
	{
		public static void Parse(this AllocationNoAllocs instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.AllocAccount = view.GetString(79);
			instance.AllocPrice = view.GetDouble(366);
			instance.AllocQty = view.GetDouble(80);
			instance.IndividualAllocID = view.GetString(467);
			instance.ProcessCode = view.GetString(81);
			if (view.GetView("NestedParties") is MsgView groupViewNestedParties)
			{
				instance.NestedParties = new NestedParties();
				instance.NestedParties!.Parse(groupViewNestedParties);
			}
			instance.NotifyBrokerOfCredit = view.GetBool(208);
			instance.AllocHandlInst = view.GetInt32(209);
			instance.AllocText = view.GetString(161);
			instance.EncodedAllocTextLen = view.GetInt32(360);
			instance.EncodedAllocText = view.GetByteArray(361);
			if (view.GetView("CommissionData") is MsgView groupViewCommissionData)
			{
				instance.CommissionData = new CommissionData();
				instance.CommissionData!.Parse(groupViewCommissionData);
			}
			instance.AllocAvgPx = view.GetDouble(153);
			instance.AllocNetMoney = view.GetDouble(154);
			instance.SettlCurrAmt = view.GetDouble(119);
			instance.SettlCurrency = view.GetString(120);
			instance.SettlCurrFxRate = view.GetDouble(155);
			instance.SettlCurrFxRateCalc = view.GetString(156);
			instance.AccruedInterestAmt = view.GetDouble(159);
			instance.SettlInstMode = view.GetString(160);
			var groupViewNoMiscFees = view.GetView("NoMiscFees");
			if (groupViewNoMiscFees is null) return;
			
			var countNoMiscFees = groupViewNoMiscFees.GroupCount();
			instance.NoMiscFees = new AllocationNoAllocsNoMiscFees[countNoMiscFees];
			for (var i = 0; i < countNoMiscFees; ++i)
			{
				instance.NoMiscFees[i] = new();
				instance.NoMiscFees[i].Parse(groupViewNoMiscFees[i]);
			}
		}
	}
}
