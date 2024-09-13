using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class ListOrdGrpNoOrdersExt
	{
		public static void Parse(this ListOrdGrpNoOrders instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.ClOrdID = view.GetString(11);
			instance.SecondaryClOrdID = view.GetString(526);
			instance.ListSeqNo = view.GetInt32(67);
			instance.ClOrdLinkID = view.GetString(583);
			instance.SettlInstMode = view.GetString(160);
			if (view.GetView("Parties") is MsgView groupViewParties)
			{
				instance.Parties = new Parties();
				instance.Parties!.Parse(groupViewParties);
			}
			instance.TradeOriginationDate = view.GetDateTime(229);
			instance.TradeDate = view.GetDateTime(75);
			instance.Account = view.GetString(1);
			instance.AcctIDSource = view.GetInt32(660);
			instance.AccountType = view.GetInt32(581);
			instance.DayBookingInst = view.GetString(589);
			instance.BookingUnit = view.GetString(590);
			instance.AllocID = view.GetString(70);
			instance.PreallocMethod = view.GetString(591);
			if (view.GetView("PreAllocGrp") is MsgView groupViewPreAllocGrp)
			{
				instance.PreAllocGrp = new PreAllocGrp();
				instance.PreAllocGrp!.Parse(groupViewPreAllocGrp);
			}
			instance.SettlType = view.GetString(63);
			instance.SettlDate = view.GetDateTime(64);
			instance.CashMargin = view.GetString(544);
			instance.ClearingFeeIndicator = view.GetString(635);
			instance.HandlInst = view.GetString(21);
			instance.ExecInst = view.GetString(18);
			instance.MinQty = view.GetDouble(110);
			instance.MaxFloor = view.GetDouble(111);
			instance.ExDestination = view.GetString(100);
			if (view.GetView("TrdgSesGrp") is MsgView groupViewTrdgSesGrp)
			{
				instance.TrdgSesGrp = new TrdgSesGrp();
				instance.TrdgSesGrp!.Parse(groupViewTrdgSesGrp);
			}
			instance.ProcessCode = view.GetString(81);
			if (view.GetView("Instrument") is MsgView groupViewInstrument)
			{
				instance.Instrument = new Instrument();
				instance.Instrument!.Parse(groupViewInstrument);
			}
			if (view.GetView("UndInstrmtGrp") is MsgView groupViewUndInstrmtGrp)
			{
				instance.UndInstrmtGrp = new UndInstrmtGrp();
				instance.UndInstrmtGrp!.Parse(groupViewUndInstrmtGrp);
			}
			instance.PrevClosePx = view.GetDouble(140);
			instance.Side = view.GetString(54);
			instance.SideValueInd = view.GetInt32(401);
			instance.LocateReqd = view.GetBool(114);
			instance.TransactTime = view.GetDateTime(60);
			if (view.GetView("Stipulations") is MsgView groupViewStipulations)
			{
				instance.Stipulations = new Stipulations();
				instance.Stipulations!.Parse(groupViewStipulations);
			}
			instance.QtyType = view.GetInt32(854);
			if (view.GetView("OrderQtyData") is MsgView groupViewOrderQtyData)
			{
				instance.OrderQtyData = new OrderQtyData();
				instance.OrderQtyData!.Parse(groupViewOrderQtyData);
			}
			instance.OrdType = view.GetString(40);
			instance.PriceType = view.GetInt32(423);
			instance.Price = view.GetDouble(44);
			instance.StopPx = view.GetDouble(99);
			if (view.GetView("SpreadOrBenchmarkCurveData") is MsgView groupViewSpreadOrBenchmarkCurveData)
			{
				instance.SpreadOrBenchmarkCurveData = new SpreadOrBenchmarkCurveData();
				instance.SpreadOrBenchmarkCurveData!.Parse(groupViewSpreadOrBenchmarkCurveData);
			}
			if (view.GetView("YieldData") is MsgView groupViewYieldData)
			{
				instance.YieldData = new YieldData();
				instance.YieldData!.Parse(groupViewYieldData);
			}
			instance.Currency = view.GetString(15);
			instance.ComplianceID = view.GetString(376);
			instance.SolicitedFlag = view.GetBool(377);
			instance.IOIID = view.GetString(23);
			instance.QuoteID = view.GetString(117);
			instance.TimeInForce = view.GetString(59);
			instance.EffectiveTime = view.GetDateTime(168);
			instance.ExpireDate = view.GetDateTime(432);
			instance.ExpireTime = view.GetDateTime(126);
			instance.GTBookingInst = view.GetInt32(427);
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
			instance.BookingType = view.GetInt32(775);
			instance.Text = view.GetString(58);
			instance.EncodedTextLen = view.GetInt32(354);
			instance.EncodedText = view.GetByteArray(355);
			instance.SettlDate2 = view.GetDateTime(193);
			instance.OrderQty2 = view.GetDouble(192);
			instance.Price2 = view.GetDouble(640);
			instance.PositionEffect = view.GetString(77);
			instance.CoveredOrUncovered = view.GetInt32(203);
			instance.MaxShow = view.GetDouble(210);
			if (view.GetView("PegInstructions") is MsgView groupViewPegInstructions)
			{
				instance.PegInstructions = new PegInstructions();
				instance.PegInstructions!.Parse(groupViewPegInstructions);
			}
			if (view.GetView("DiscretionInstructions") is MsgView groupViewDiscretionInstructions)
			{
				instance.DiscretionInstructions = new DiscretionInstructions();
				instance.DiscretionInstructions!.Parse(groupViewDiscretionInstructions);
			}
			instance.TargetStrategy = view.GetInt32(847);
			instance.TargetStrategyParameters = view.GetString(848);
			instance.ParticipationRate = view.GetDouble(849);
			instance.Designation = view.GetString(494);
		}
	}
}
