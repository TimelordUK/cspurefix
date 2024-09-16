using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class NstdPtysSubGrpExt
	{
		public static void Parse(this NstdPtysSubGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupViewNoNestedPartySubIDs = view.GetView("NoNestedPartySubIDs");
			if (groupViewNoNestedPartySubIDs is null) return;
			
			var countNoNestedPartySubIDs = groupViewNoNestedPartySubIDs.GroupCount();
			instance.NoNestedPartySubIDs = new NstdPtysSubGrpNoNestedPartySubIDs[countNoNestedPartySubIDs];
			for (var i = 0; i < countNoNestedPartySubIDs; ++i)
			{
				instance.NoNestedPartySubIDs[i] = new();
				instance.NoNestedPartySubIDs[i].Parse(groupViewNoNestedPartySubIDs[i]);
			}
		}
	}
}
