using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public static class NewOrderMultilegNoAllocsExt
	{
		public static void Parse(this NewOrderMultilegNoAllocs instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.AllocAccount = view.GetString(79);
			instance.IndividualAllocID = view.GetString(467);
			instance.AllocQty = view.GetDouble(80);
		}
	}
}
