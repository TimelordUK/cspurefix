using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public static class NewOrderCrossNoSidesExt
	{
		public static void Parse(this NewOrderCrossNoSides instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.Side = view.GetString(54);
			instance.ClOrdID = view.GetString(11);
			instance.SecondaryClOrdID = view.GetString(526);
			instance.ClOrdLinkID = view.GetString(583);
			if (view.GetView("Parties") is MsgView groupViewParties)
			{
				instance.Parties = new Parties();
				instance.Parties!.Parse(groupViewParties);
			}
			instance.TradeOriginationDate = view.GetString(229);
			instance.Account = view.GetString(1);
			instance.AccountType = view.GetInt32(581);
			instance.DayBookingInst = view.GetString(589);
			instance.BookingUnit = view.GetString(590);
			instance.PreallocMethod = view.GetString(591);
			var groupViewNoAllocs = view.GetView("NoAllocs");
			if (groupViewNoAllocs is null) return;
			
			var countNoAllocs = groupViewNoAllocs.GroupCount();
			instance.NoAllocs = new NewOrderCrossNoSidesNoAllocs[countNoAllocs];
			for (var i = 0; i < countNoAllocs; ++i)
			{
				instance.NoAllocs[i] = new();
				instance.NoAllocs[i].Parse(groupViewNoAllocs[i]);
			}
			instance.QuantityType = view.GetInt32(465);
			if (view.GetView("OrderQtyData") is MsgView groupViewOrderQtyData)
			{
				instance.OrderQtyData = new OrderQtyData();
				instance.OrderQtyData!.Parse(groupViewOrderQtyData);
			}
			if (view.GetView("CommissionData") is MsgView groupViewCommissionData)
			{
				instance.CommissionData = new CommissionData();
				instance.CommissionData!.Parse(groupViewCommissionData);
			}
			instance.OrderCapacity = view.GetString(528);
			instance.OrderRestrictions = view.GetString(529);
			instance.CustOrderCapacity = view.GetInt32(582);
			instance.ForexReq = view.GetBool(121);
			instance.SettlCurrency = view.GetString(120);
			instance.Text = view.GetString(58);
			instance.EncodedTextLen = view.GetInt32(354);
			instance.EncodedText = view.GetByteArray(355);
			instance.PositionEffect = view.GetString(77);
			instance.CoveredOrUncovered = view.GetInt32(203);
			instance.CashMargin = view.GetString(544);
			instance.ClearingFeeIndicator = view.GetString(635);
			instance.SolicitedFlag = view.GetBool(377);
			instance.SideComplianceID = view.GetString(659);
		}
	}
}
