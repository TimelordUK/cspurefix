using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class QuotSetAckGrpExt
	{
		public static void Parse(this QuotSetAckGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupViewNoQuoteSets = view.GetView("NoQuoteSets");
			if (groupViewNoQuoteSets is null) return;
			
			var countNoQuoteSets = groupViewNoQuoteSets.GroupCount();
			instance.NoQuoteSets = new QuotSetAckGrpNoQuoteSets[countNoQuoteSets];
			for (var i = 0; i < countNoQuoteSets; ++i)
			{
				instance.NoQuoteSets[i] = new();
				instance.NoQuoteSets[i].Parse(groupViewNoQuoteSets[i]);
			}
		}
	}
}
