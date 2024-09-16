using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class SecTypesGrpExt
	{
		public static void Parse(this SecTypesGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupViewNoSecurityTypes = view.GetView("NoSecurityTypes");
			if (groupViewNoSecurityTypes is null) return;
			
			var countNoSecurityTypes = groupViewNoSecurityTypes.GroupCount();
			instance.NoSecurityTypes = new SecTypesGrpNoSecurityTypes[countNoSecurityTypes];
			for (var i = 0; i < countNoSecurityTypes; ++i)
			{
				instance.NoSecurityTypes[i] = new();
				instance.NoSecurityTypes[i].Parse(groupViewNoSecurityTypes[i]);
			}
		}
	}
}
