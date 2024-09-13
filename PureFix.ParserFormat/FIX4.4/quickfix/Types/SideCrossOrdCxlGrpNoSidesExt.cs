using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class SideCrossOrdCxlGrpNoSidesExt
	{
		public static void Parse(this SideCrossOrdCxlGrpNoSides instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.Side = view.GetString(54);
			instance.OrigClOrdID = view.GetString(41);
			instance.ClOrdID = view.GetString(11);
			instance.SecondaryClOrdID = view.GetString(526);
			instance.ClOrdLinkID = view.GetString(583);
			instance.OrigOrdModTime = view.GetDateTime(586);
			instance.Parties?.Parse(view.GetView("Parties"));
			instance.TradeOriginationDate = view.GetDateTime(229);
			instance.TradeDate = view.GetDateTime(75);
			instance.OrderQtyData?.Parse(view.GetView("OrderQtyData"));
			instance.ComplianceID = view.GetString(376);
			instance.Text = view.GetString(58);
			instance.EncodedTextLen = view.GetInt32(354);
			instance.EncodedText = view.GetByteArray(355);
		}
	}
}
