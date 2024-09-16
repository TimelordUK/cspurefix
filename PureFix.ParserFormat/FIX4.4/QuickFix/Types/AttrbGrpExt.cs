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
			
			var groupViewNoInstrAttrib = view.GetView("NoInstrAttrib");
			if (groupViewNoInstrAttrib is null) return;
			
			var countNoInstrAttrib = groupViewNoInstrAttrib.GroupCount();
			instance.NoInstrAttrib = new AttrbGrpNoInstrAttrib[countNoInstrAttrib];
			for (var i = 0; i < countNoInstrAttrib; ++i)
			{
				instance.NoInstrAttrib[i] = new();
				instance.NoInstrAttrib[i].Parse(groupViewNoInstrAttrib[i]);
			}
		}
	}
}
