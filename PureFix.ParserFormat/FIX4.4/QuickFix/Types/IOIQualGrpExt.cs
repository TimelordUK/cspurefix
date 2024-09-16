using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class IOIQualGrpExt
	{
		public static void Parse(this IOIQualGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupViewNoIOIQualifiers = view.GetView("NoIOIQualifiers");
			if (groupViewNoIOIQualifiers is null) return;
			
			var countNoIOIQualifiers = groupViewNoIOIQualifiers.GroupCount();
			instance.NoIOIQualifiers = new IOIQualGrpNoIOIQualifiers[countNoIOIQualifiers];
			for (var i = 0; i < countNoIOIQualifiers; ++i)
			{
				instance.NoIOIQualifiers[i] = new();
				instance.NoIOIQualifiers[i].Parse(groupViewNoIOIQualifiers[i]);
			}
		}
	}
}
