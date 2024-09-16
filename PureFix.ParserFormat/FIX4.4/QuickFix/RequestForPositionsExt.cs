using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AN", FixVersion.FIX44)]
	public static class RequestForPositionsExt
	{
		public static void Parse(this RequestForPositions instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.PosReqID = view.GetString(710);
			instance.PosReqType = view.GetInt32(724);
			instance.MatchStatus = view.GetString(573);
			instance.SubscriptionRequestType = view.GetString(263);
			if (view.GetView("Parties") is MsgView groupViewParties)
			{
				instance.Parties = new Parties();
				instance.Parties!.Parse(groupViewParties);
			}
			instance.Account = view.GetString(1);
			instance.AcctIDSource = view.GetInt32(660);
			instance.AccountType = view.GetInt32(581);
			if (view.GetView("Instrument") is MsgView groupViewInstrument)
			{
				instance.Instrument = new Instrument();
				instance.Instrument!.Parse(groupViewInstrument);
			}
			instance.Currency = view.GetString(15);
			if (view.GetView("InstrmtLegGrp") is MsgView groupViewInstrmtLegGrp)
			{
				instance.InstrmtLegGrp = new InstrmtLegGrp();
				instance.InstrmtLegGrp!.Parse(groupViewInstrmtLegGrp);
			}
			if (view.GetView("UndInstrmtGrp") is MsgView groupViewUndInstrmtGrp)
			{
				instance.UndInstrmtGrp = new UndInstrmtGrp();
				instance.UndInstrmtGrp!.Parse(groupViewUndInstrmtGrp);
			}
			instance.ClearingBusinessDate = view.GetDateOnly(715);
			instance.SettlSessID = view.GetString(716);
			instance.SettlSessSubID = view.GetString(717);
			if (view.GetView("TrdgSesGrp") is MsgView groupViewTrdgSesGrp)
			{
				instance.TrdgSesGrp = new TrdgSesGrp();
				instance.TrdgSesGrp!.Parse(groupViewTrdgSesGrp);
			}
			instance.TransactTime = view.GetDateTime(60);
			instance.ResponseTransportType = view.GetInt32(725);
			instance.ResponseDestination = view.GetString(726);
			instance.Text = view.GetString(58);
			instance.EncodedTextLen = view.GetInt32(354);
			instance.EncodedText = view.GetByteArray(355);
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
		}
	}
}
