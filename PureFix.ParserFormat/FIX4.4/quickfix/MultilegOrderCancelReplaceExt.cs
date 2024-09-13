using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AC", FixVersion.FIX44)]
	public static class MultilegOrderCancelReplaceExt
	{
		public static void Parse(this MultilegOrderCancelReplace instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.OrderID = view.GetString(37);
			instance.OrigClOrdID = view.GetString(41);
			instance.ClOrdID = view.GetString(11);
			instance.SecondaryClOrdID = view.GetString(526);
			instance.ClOrdLinkID = view.GetString(583);
			instance.OrigOrdModTime = view.GetDateTime(586);
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
			instance.PreallocMethod = view.GetString(591);
			instance.AllocID = view.GetString(70);
			if (view.GetView("PreAllocMlegGrp") is MsgView groupViewPreAllocMlegGrp)
			{
				instance.PreAllocMlegGrp = new PreAllocMlegGrp();
				instance.PreAllocMlegGrp!.Parse(groupViewPreAllocMlegGrp);
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
			instance.Side = view.GetString(54);
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
			if (view.GetView("LegOrdGrp") is MsgView groupViewLegOrdGrp)
			{
				instance.LegOrdGrp = new LegOrdGrp();
				instance.LegOrdGrp!.Parse(groupViewLegOrdGrp);
			}
			instance.LocateReqd = view.GetBool(114);
			instance.TransactTime = view.GetDateTime(60);
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
			instance.CancellationRights = view.GetString(480);
			instance.MoneyLaunderingStatus = view.GetString(481);
			instance.RegistID = view.GetString(513);
			instance.Designation = view.GetString(494);
			instance.MultiLegRptTypeReq = view.GetInt32(563);
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
		}
	}
}
