using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("m", FixVersion.FIX44)]
	public static class ListStrikePriceExt
	{
		public static void Parse(this ListStrikePrice instance, MsgView? view)
		{
			instance.StandardHeader?.Parse(view?.GetView("StandardHeader"));
			instance.ListID = view?.GetString(66);
			instance.TotNoStrikes = view?.GetInt32(422);
			instance.LastFragment = view?.GetBool(893);
			instance.InstrmtStrkPxGrp?.Parse(view?.GetView("InstrmtStrkPxGrp"));
			instance.UndInstrmtStrkPxGrp?.Parse(view?.GetView("UndInstrmtStrkPxGrp"));
			instance.StandardTrailer?.Parse(view?.GetView("StandardTrailer"));
		}
	}
}
