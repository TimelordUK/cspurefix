using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("J", FixVersion.FIX44)]
	public static class AllocationInstructionExt
	{
		public static void Parse(this AllocationInstruction instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.StandardHeader?.Parse(view.GetView("StandardHeader"));
			instance.AllocID = view.GetString(70);
			instance.AllocTransType = view.GetString(71);
			instance.AllocType = view.GetInt32(626);
			instance.SecondaryAllocID = view.GetString(793);
			instance.RefAllocID = view.GetString(72);
			instance.AllocCancReplaceReason = view.GetInt32(796);
			instance.AllocIntermedReqType = view.GetInt32(808);
			instance.AllocLinkID = view.GetString(196);
			instance.AllocLinkType = view.GetInt32(197);
			instance.BookingRefID = view.GetString(466);
			instance.AllocNoOrdersType = view.GetInt32(857);
			instance.OrdAllocGrp?.Parse(view.GetView("OrdAllocGrp"));
			instance.ExecAllocGrp?.Parse(view.GetView("ExecAllocGrp"));
			instance.PreviouslyReported = view.GetBool(570);
			instance.ReversalIndicator = view.GetBool(700);
			instance.MatchType = view.GetString(574);
			instance.Side = view.GetString(54);
			instance.Instrument?.Parse(view.GetView("Instrument"));
			instance.InstrumentExtension?.Parse(view.GetView("InstrumentExtension"));
			instance.FinancingDetails?.Parse(view.GetView("FinancingDetails"));
			instance.UndInstrmtGrp?.Parse(view.GetView("UndInstrmtGrp"));
			instance.InstrmtLegGrp?.Parse(view.GetView("InstrmtLegGrp"));
			instance.Quantity = view.GetDouble(53);
			instance.QtyType = view.GetInt32(854);
			instance.LastMkt = view.GetString(30);
			instance.TradeOriginationDate = view.GetDateTime(229);
			instance.TradingSessionID = view.GetString(336);
			instance.TradingSessionSubID = view.GetString(625);
			instance.PriceType = view.GetInt32(423);
			instance.AvgPx = view.GetDouble(6);
			instance.AvgParPx = view.GetDouble(860);
			instance.SpreadOrBenchmarkCurveData?.Parse(view.GetView("SpreadOrBenchmarkCurveData"));
			instance.Currency = view.GetString(15);
			instance.AvgPxPrecision = view.GetInt32(74);
			instance.Parties?.Parse(view.GetView("Parties"));
			instance.TradeDate = view.GetDateTime(75);
			instance.TransactTime = view.GetDateTime(60);
			instance.SettlType = view.GetString(63);
			instance.SettlDate = view.GetDateTime(64);
			instance.BookingType = view.GetInt32(775);
			instance.GrossTradeAmt = view.GetDouble(381);
			instance.Concession = view.GetDouble(238);
			instance.TotalTakedown = view.GetDouble(237);
			instance.NetMoney = view.GetDouble(118);
			instance.PositionEffect = view.GetString(77);
			instance.AutoAcceptIndicator = view.GetBool(754);
			instance.Text = view.GetString(58);
			instance.EncodedTextLen = view.GetInt32(354);
			instance.EncodedText = view.GetByteArray(355);
			instance.NumDaysInterest = view.GetInt32(157);
			instance.AccruedInterestRate = view.GetDouble(158);
			instance.AccruedInterestAmt = view.GetDouble(159);
			instance.TotalAccruedInterestAmt = view.GetDouble(540);
			instance.InterestAtMaturity = view.GetDouble(738);
			instance.EndAccruedInterestAmt = view.GetDouble(920);
			instance.StartCash = view.GetDouble(921);
			instance.EndCash = view.GetDouble(922);
			instance.LegalConfirm = view.GetBool(650);
			instance.Stipulations?.Parse(view.GetView("Stipulations"));
			instance.YieldData?.Parse(view.GetView("YieldData"));
			instance.TotNoAllocs = view.GetInt32(892);
			instance.LastFragment = view.GetBool(893);
			instance.AllocGrp?.Parse(view.GetView("AllocGrp"));
			instance.StandardTrailer?.Parse(view.GetView("StandardTrailer"));
		}
	}
}
