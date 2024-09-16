using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class UnderlyingStipulationsExt
	{
		public static void Parse(this UnderlyingStipulations instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupView = view.GetView("NoUnderlyingStips");
			if (groupView is null) return;
			
			var count = groupView.GroupCount();
			instance.NoUnderlyingStips = new UnderlyingStipulationsNoUnderlyingStips[count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoUnderlyingStips[i] = new();
				instance.NoUnderlyingStips[i].Parse(groupView[i]);
			}
		}
	}
}
