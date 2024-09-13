using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("D", FixVersion.FIX44)]
	public static class NewOrderSingleExt
	{
		public static void Parse(this NewOrderSingle instance, MsgView? view)
		{
			instance.StandardHeader = new StandardHeader();
			instance.StandardHeader?.Parse(view?.GetView("StandardHeader"));
			instance.ClOrdID = view?.GetString(11);
			instance.SecondaryClOrdID = view?.GetString(526);
			instance.ClOrdLinkID = view?.GetString(583);
			instance.Parties = new Parties();
			instance.Parties?.Parse(view?.GetView("Parties"));
			instance.TradeOriginationDate = view?.GetDateTime(229);
			instance.TradeDate = view?.GetDateTime(75);
			instance.Account = view?.GetString(1);
			instance.AcctIDSource = view?.GetInt32(660);
			instance.AccountType = view?.GetInt32(581);
			instance.DayBookingInst = view?.GetString(589);
			instance.BookingUnit = view?.GetString(590);
			instance.PreallocMethod = view?.GetString(591);
			instance.AllocID = view?.GetString(70);
			instance.PreAllocGrp = new PreAllocGrp();
			instance.PreAllocGrp?.Parse(view?.GetView("PreAllocGrp"));
			instance.SettlType = view?.GetString(63);
			instance.SettlDate = view?.GetDateTime(64);
			instance.CashMargin = view?.GetString(544);
			instance.ClearingFeeIndicator = view?.GetString(635);
			instance.HandlInst = view?.GetString(21);
			instance.ExecInst = view?.GetString(18);
			instance.MinQty = view?.GetDouble(110);
			instance.MaxFloor = view?.GetDouble(111);
			instance.ExDestination = view?.GetString(100);
			instance.TrdgSesGrp = new TrdgSesGrp();
			instance.TrdgSesGrp?.Parse(view?.GetView("TrdgSesGrp"));
			instance.ProcessCode = view?.GetString(81);
			instance.Instrument = new Instrument();
			instance.Instrument?.Parse(view?.GetView("Instrument"));
			instance.FinancingDetails = new FinancingDetails();
			instance.FinancingDetails?.Parse(view?.GetView("FinancingDetails"));
			instance.UndInstrmtGrp = new UndInstrmtGrp();
			instance.UndInstrmtGrp?.Parse(view?.GetView("UndInstrmtGrp"));
			instance.PrevClosePx = view?.GetDouble(140);
			instance.Side = view?.GetString(54);
			instance.LocateReqd = view?.GetBool(114);
			instance.TransactTime = view?.GetDateTime(60);
			instance.Stipulations = new Stipulations();
			instance.Stipulations?.Parse(view?.GetView("Stipulations"));
			instance.QtyType = view?.GetInt32(854);
			instance.OrderQtyData = new OrderQtyData();
			instance.OrderQtyData?.Parse(view?.GetView("OrderQtyData"));
			instance.OrdType = view?.GetString(40);
			instance.PriceType = view?.GetInt32(423);
			instance.Price = view?.GetDouble(44);
			instance.StopPx = view?.GetDouble(99);
			instance.SpreadOrBenchmarkCurveData = new SpreadOrBenchmarkCurveData();
			instance.SpreadOrBenchmarkCurveData?.Parse(view?.GetView("SpreadOrBenchmarkCurveData"));
			instance.YieldData = new YieldData();
			instance.YieldData?.Parse(view?.GetView("YieldData"));
			instance.Currency = view?.GetString(15);
			instance.ComplianceID = view?.GetString(376);
			instance.SolicitedFlag = view?.GetBool(377);
			instance.IOIID = view?.GetString(23);
			instance.QuoteID = view?.GetString(117);
			instance.TimeInForce = view?.GetString(59);
			instance.EffectiveTime = view?.GetDateTime(168);
			instance.ExpireDate = view?.GetDateTime(432);
			instance.ExpireTime = view?.GetDateTime(126);
			instance.GTBookingInst = view?.GetInt32(427);
			instance.CommissionData = new CommissionData();
			instance.CommissionData?.Parse(view?.GetView("CommissionData"));
			instance.OrderCapacity = view?.GetString(528);
			instance.OrderRestrictions = view?.GetString(529);
			instance.CustOrderCapacity = view?.GetInt32(582);
			instance.ForexReq = view?.GetBool(121);
			instance.SettlCurrency = view?.GetString(120);
			instance.BookingType = view?.GetInt32(775);
			instance.Text = view?.GetString(58);
			instance.EncodedTextLen = view?.GetInt32(354);
			instance.EncodedText = view?.GetByteArray(355);
			instance.SettlDate2 = view?.GetDateTime(193);
			instance.OrderQty2 = view?.GetDouble(192);
			instance.Price2 = view?.GetDouble(640);
			instance.PositionEffect = view?.GetString(77);
			instance.CoveredOrUncovered = view?.GetInt32(203);
			instance.MaxShow = view?.GetDouble(210);
			instance.PegInstructions = new PegInstructions();
			instance.PegInstructions?.Parse(view?.GetView("PegInstructions"));
			instance.DiscretionInstructions = new DiscretionInstructions();
			instance.DiscretionInstructions?.Parse(view?.GetView("DiscretionInstructions"));
			instance.TargetStrategy = view?.GetInt32(847);
			instance.TargetStrategyParameters = view?.GetString(848);
			instance.ParticipationRate = view?.GetDouble(849);
			instance.CancellationRights = view?.GetString(480);
			instance.MoneyLaunderingStatus = view?.GetString(481);
			instance.RegistID = view?.GetString(513);
			instance.Designation = view?.GetString(494);
			instance.StandardTrailer = new StandardTrailer();
			instance.StandardTrailer?.Parse(view?.GetView("StandardTrailer"));
		}
	}
}
