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
			
			var groupView = view.GetView("NoIOIQualifiers");
			if (groupView is null) return;
			
			var count = groupView.GroupCount();
			instance.NoIOIQualifiers = new IOIQualGrpNoIOIQualifiers[count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoIOIQualifiers[i] = new();
				instance.NoIOIQualifiers[i].Parse(groupView[i]);
			}
		}
	}
}
