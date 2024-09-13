using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("t", FixVersion.FIX44)]
	public static class CrossOrderCancelReplaceRequestExt
	{
		public static void Parse(this CrossOrderCancelReplaceRequest instance, MsgView? view)
		{
			instance.StandardHeader?.Parse(view?.GetView("StandardHeader"));
			instance.OrderID = view?.GetString(37);
			instance.CrossID = view?.GetString(548);
			instance.OrigCrossID = view?.GetString(551);
			instance.CrossType = view?.GetInt32(549);
			instance.CrossPrioritization = view?.GetInt32(550);
			instance.SideCrossOrdModGrp?.Parse(view?.GetView("SideCrossOrdModGrp"));
			instance.Instrument?.Parse(view?.GetView("Instrument"));
			instance.UndInstrmtGrp?.Parse(view?.GetView("UndInstrmtGrp"));
			instance.InstrmtLegGrp?.Parse(view?.GetView("InstrmtLegGrp"));
			instance.SettlType = view?.GetString(63);
			instance.SettlDate = view?.GetDateTime(64);
			instance.HandlInst = view?.GetString(21);
			instance.ExecInst = view?.GetString(18);
			instance.MinQty = view?.GetDouble(110);
			instance.MaxFloor = view?.GetDouble(111);
			instance.ExDestination = view?.GetString(100);
			instance.TrdgSesGrp?.Parse(view?.GetView("TrdgSesGrp"));
			instance.ProcessCode = view?.GetString(81);
			instance.PrevClosePx = view?.GetDouble(140);
			instance.LocateReqd = view?.GetBool(114);
			instance.TransactTime = view?.GetDateTime(60);
			instance.Stipulations?.Parse(view?.GetView("Stipulations"));
			instance.OrdType = view?.GetString(40);
			instance.PriceType = view?.GetInt32(423);
			instance.Price = view?.GetDouble(44);
			instance.StopPx = view?.GetDouble(99);
			instance.SpreadOrBenchmarkCurveData?.Parse(view?.GetView("SpreadOrBenchmarkCurveData"));
			instance.YieldData?.Parse(view?.GetView("YieldData"));
			instance.Currency = view?.GetString(15);
			instance.ComplianceID = view?.GetString(376);
			instance.IOIID = view?.GetString(23);
			instance.QuoteID = view?.GetString(117);
			instance.TimeInForce = view?.GetString(59);
			instance.EffectiveTime = view?.GetDateTime(168);
			instance.ExpireDate = view?.GetDateTime(432);
			instance.ExpireTime = view?.GetDateTime(126);
			instance.GTBookingInst = view?.GetInt32(427);
			instance.MaxShow = view?.GetDouble(210);
			instance.PegInstructions?.Parse(view?.GetView("PegInstructions"));
			instance.DiscretionInstructions?.Parse(view?.GetView("DiscretionInstructions"));
			instance.TargetStrategy = view?.GetInt32(847);
			instance.TargetStrategyParameters = view?.GetString(848);
			instance.ParticipationRate = view?.GetDouble(849);
			instance.CancellationRights = view?.GetString(480);
			instance.MoneyLaunderingStatus = view?.GetString(481);
			instance.RegistID = view?.GetString(513);
			instance.Designation = view?.GetString(494);
			instance.StandardTrailer?.Parse(view?.GetView("StandardTrailer"));
		}
	}
}
