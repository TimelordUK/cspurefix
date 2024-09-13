using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AT", FixVersion.FIX44)]
	public static class AllocationReportAckExt
	{
		public static void Parse(this AllocationReportAck instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.StandardHeader = new StandardHeader();
			instance.StandardHeader?.Parse(view.GetView("StandardHeader"));
			instance.AllocReportID = view.GetString(755);
			instance.AllocID = view.GetString(70);
			if (view.GetView("Parties") is MsgView groupViewParties)
			{
				instance.Parties = new Parties();
				instance.Parties!.Parse(groupViewParties);
			}
			instance.Parties = new Parties();
			instance.Parties?.Parse(view.GetView("Parties"));
			instance.SecondaryAllocID = view.GetString(793);
			instance.TradeDate = view.GetDateTime(75);
			instance.TransactTime = view.GetDateTime(60);
			instance.AllocStatus = view.GetInt32(87);
			instance.AllocRejCode = view.GetInt32(88);
			instance.AllocReportType = view.GetInt32(794);
			instance.AllocIntermedReqType = view.GetInt32(808);
			instance.MatchStatus = view.GetString(573);
			instance.Product = view.GetInt32(460);
			instance.SecurityType = view.GetString(167);
			instance.Text = view.GetString(58);
			instance.EncodedTextLen = view.GetInt32(354);
			instance.EncodedText = view.GetByteArray(355);
			if (view.GetView("AllocAckGrp") is MsgView groupViewAllocAckGrp)
			{
				instance.AllocAckGrp = new AllocAckGrp();
				instance.AllocAckGrp!.Parse(groupViewAllocAckGrp);
			}
			instance.AllocAckGrp = new AllocAckGrp();
			instance.AllocAckGrp?.Parse(view.GetView("AllocAckGrp"));
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
			instance.StandardTrailer = new StandardTrailer();
			instance.StandardTrailer?.Parse(view.GetView("StandardTrailer"));
		}
	}
}
