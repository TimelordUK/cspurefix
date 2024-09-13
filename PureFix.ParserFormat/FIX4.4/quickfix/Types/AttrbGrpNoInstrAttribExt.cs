using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class AttrbGrpNoInstrAttribExt
	{
		public static void Parse(this AttrbGrpNoInstrAttrib instance, MsgView? view)
		{
			instance.InstrAttribType = view?.GetInt32(871);
			instance.InstrAttribValue = view?.GetString(872);
		}
	}
}
