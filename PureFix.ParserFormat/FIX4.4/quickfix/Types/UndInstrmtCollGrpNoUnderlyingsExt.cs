using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class UndInstrmtCollGrpNoUnderlyingsExt
	{
		public static void Parse(this UndInstrmtCollGrpNoUnderlyings instance, MsgView? view)
		{
			instance.UnderlyingInstrument = new UnderlyingInstrument();
			instance.UnderlyingInstrument?.Parse(view?.GetView("UnderlyingInstrument"));
			instance.CollAction = view?.GetInt32(944);
		}
	}
}
