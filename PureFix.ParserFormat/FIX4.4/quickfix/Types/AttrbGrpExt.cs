using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class AttrbGrpExt
	{
		public static void Parse(this AttrbGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupView = view.GetView("NoInstrAttrib");
			if (groupView is null) return;
			
			var count = groupView.GroupCount();
			instance.NoInstrAttrib = new AttrbGrpNoInstrAttrib[count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoInstrAttrib[i] = new();
				instance.NoInstrAttrib[i].Parse(groupView[i]);
			}
		}
	}
}
