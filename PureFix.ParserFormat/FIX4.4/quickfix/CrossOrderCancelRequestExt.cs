using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("u", FixVersion.FIX44)]
	public static class CrossOrderCancelRequestExt
	{
		public static void Parse(this CrossOrderCancelRequest instance, MsgView? view)
		{
			instance.StandardHeader?.Parse(view?.GetView("StandardHeader"));
			instance.OrderID = view?.GetString(37);
			instance.CrossID = view?.GetString(548);
			instance.OrigCrossID = view?.GetString(551);
			instance.CrossType = view?.GetInt32(549);
			instance.CrossPrioritization = view?.GetInt32(550);
			instance.SideCrossOrdCxlGrp?.Parse(view?.GetView("SideCrossOrdCxlGrp"));
			instance.Instrument?.Parse(view?.GetView("Instrument"));
			instance.UndInstrmtGrp?.Parse(view?.GetView("UndInstrmtGrp"));
			instance.InstrmtLegGrp?.Parse(view?.GetView("InstrmtLegGrp"));
			instance.TransactTime = view?.GetDateTime(60);
			instance.StandardTrailer?.Parse(view?.GetView("StandardTrailer"));
		}
	}
}
