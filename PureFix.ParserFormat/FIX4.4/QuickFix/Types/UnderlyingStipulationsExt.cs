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
			
			var groupViewNoUnderlyingStips = view.GetView("NoUnderlyingStips");
			if (groupViewNoUnderlyingStips is null) return;
			
			var countNoUnderlyingStips = groupViewNoUnderlyingStips.GroupCount();
			instance.NoUnderlyingStips = new UnderlyingStipulationsNoUnderlyingStips[countNoUnderlyingStips];
			for (var i = 0; i < countNoUnderlyingStips; ++i)
			{
				instance.NoUnderlyingStips[i] = new();
				instance.NoUnderlyingStips[i].Parse(groupViewNoUnderlyingStips[i]);
			}
		}
	}
}
