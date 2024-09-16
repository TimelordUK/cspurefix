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
			if (view is null) return;
			
			var groupViewNoTradingSessions = view.GetView("NoTradingSessions");
			if (groupViewNoTradingSessions is null) return;
			
			var countNoTradingSessions = groupViewNoTradingSessions.GroupCount();
			instance.NoTradingSessions = new TrdgSesGrpNoTradingSessions[countNoTradingSessions];
			for (var i = 0; i < countNoTradingSessions; ++i)
			{
				instance.NoTradingSessions[i] = new();
				instance.NoTradingSessions[i].Parse(groupViewNoTradingSessions[i]);
			}
		}
	}
}
