using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class UndInstrmtStrkPxGrpNoUnderlyingsExt
	{
		public static void Parse(this UndInstrmtStrkPxGrpNoUnderlyings instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("UnderlyingInstrument") is MsgView groupViewUnderlyingInstrument)
			{
				instance.UnderlyingInstrument = new UnderlyingInstrument();
				instance.UnderlyingInstrument!.Parse(groupViewUnderlyingInstrument);
			}
			instance.UnderlyingInstrument = new UnderlyingInstrument();
			instance.UnderlyingInstrument?.Parse(view.GetView("UnderlyingInstrument"));
			instance.PrevClosePx = view.GetDouble(140);
			instance.ClOrdID = view.GetString(11);
			instance.SecondaryClOrdID = view.GetString(526);
			instance.Side = view.GetString(54);
			instance.Price = view.GetDouble(44);
			instance.Currency = view.GetString(15);
			instance.Text = view.GetString(58);
			instance.EncodedTextLen = view.GetInt32(354);
			instance.EncodedText = view.GetByteArray(355);
		}
	}
}
