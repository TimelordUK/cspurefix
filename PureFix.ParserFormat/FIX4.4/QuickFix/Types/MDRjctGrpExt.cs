using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class MDRjctGrpExt
	{
		public static void Parse(this MDRjctGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupViewNoAltMDSource = view.GetView("NoAltMDSource");
			if (groupViewNoAltMDSource is null) return;
			
			var countNoAltMDSource = groupViewNoAltMDSource.GroupCount();
			instance.NoAltMDSource = new MDRjctGrpNoAltMDSource[countNoAltMDSource];
			for (var i = 0; i < countNoAltMDSource; ++i)
			{
				instance.NoAltMDSource[i] = new();
				instance.NoAltMDSource[i].Parse(groupViewNoAltMDSource[i]);
			}
		}
	}
}
