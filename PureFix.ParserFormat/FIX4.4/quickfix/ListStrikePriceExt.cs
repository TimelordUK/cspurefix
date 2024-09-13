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
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.StandardHeader = new StandardHeader();
			instance.StandardHeader?.Parse(view.GetView("StandardHeader"));
			instance.ListID = view.GetString(66);
			instance.TotNoStrikes = view.GetInt32(422);
			instance.LastFragment = view.GetBool(893);
			if (view.GetView("InstrmtStrkPxGrp") is MsgView groupViewInstrmtStrkPxGrp)
			{
				instance.InstrmtStrkPxGrp = new InstrmtStrkPxGrp();
				instance.InstrmtStrkPxGrp!.Parse(groupViewInstrmtStrkPxGrp);
			}
			instance.InstrmtStrkPxGrp = new InstrmtStrkPxGrp();
			instance.InstrmtStrkPxGrp?.Parse(view.GetView("InstrmtStrkPxGrp"));
			if (view.GetView("UndInstrmtStrkPxGrp") is MsgView groupViewUndInstrmtStrkPxGrp)
			{
				instance.UndInstrmtStrkPxGrp = new UndInstrmtStrkPxGrp();
				instance.UndInstrmtStrkPxGrp!.Parse(groupViewUndInstrmtStrkPxGrp);
			}
			instance.UndInstrmtStrkPxGrp = new UndInstrmtStrkPxGrp();
			instance.UndInstrmtStrkPxGrp?.Parse(view.GetView("UndInstrmtStrkPxGrp"));
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
