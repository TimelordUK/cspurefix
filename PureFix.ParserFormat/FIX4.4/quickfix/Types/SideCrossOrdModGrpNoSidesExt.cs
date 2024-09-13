using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class SideCrossOrdModGrpNoSidesExt
	{
		public static void Parse(this SideCrossOrdModGrpNoSides instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.Side = view.GetString(54);
			instance.ClOrdID = view.GetString(11);
			instance.SecondaryClOrdID = view.GetString(526);
			instance.ClOrdLinkID = view.GetString(583);
			instance.Parties?.Parse(view.GetView("Parties"));
			instance.TradeOriginationDate = view.GetDateTime(229);
			instance.TradeDate = view.GetDateTime(75);
			instance.Account = view.GetString(1);
			instance.AcctIDSource = view.GetInt32(660);
			instance.AccountType = view.GetInt32(581);
			instance.DayBookingInst = view.GetString(589);
			instance.BookingUnit = view.GetString(590);
			instance.PreallocMethod = view.GetString(591);
			instance.AllocID = view.GetString(70);
			instance.PreAllocGrp?.Parse(view.GetView("PreAllocGrp"));
			instance.QtyType = view.GetInt32(854);
			instance.OrderQtyData?.Parse(view.GetView("OrderQtyData"));
			instance.CommissionData?.Parse(view.GetView("CommissionData"));
			instance.OrderCapacity = view.GetString(528);
			instance.OrderRestrictions = view.GetString(529);
			instance.CustOrderCapacity = view.GetInt32(582);
			instance.ForexReq = view.GetBool(121);
			instance.SettlCurrency = view.GetString(120);
			instance.BookingType = view.GetInt32(775);
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
