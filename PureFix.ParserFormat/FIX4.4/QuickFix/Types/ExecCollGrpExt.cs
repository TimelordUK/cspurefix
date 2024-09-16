using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class ExecCollGrpExt
	{
		public static void Parse(this ExecCollGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupViewNoExecs = view.GetView("NoExecs");
			if (groupViewNoExecs is null) return;
			
			var countNoExecs = groupViewNoExecs.GroupCount();
			instance.NoExecs = new ExecCollGrpNoExecs[countNoExecs];
			for (var i = 0; i < countNoExecs; ++i)
			{
				instance.NoExecs[i] = new();
				instance.NoExecs[i].Parse(groupViewNoExecs[i]);
			}
		}
	}
}
