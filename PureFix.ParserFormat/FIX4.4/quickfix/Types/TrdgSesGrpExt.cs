using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class TrdgSesGrpExt
	{
		public static void Parse(this TrdgSesGrp instance, MsgView? view)
		{
			var count = view?.GroupCount() ?? 0;
			instance.NoTradingSessions = new TrdgSesGrpNoTradingSessions [count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoTradingSessions[i] = new();
				instance.NoTradingSessions[i].Parse(view?[i]);
			}
		}
	}
}
