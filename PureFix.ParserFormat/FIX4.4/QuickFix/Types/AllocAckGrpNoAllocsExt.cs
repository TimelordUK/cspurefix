using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class AllocAckGrpNoAllocsExt
	{
		public static void Parse(this AllocAckGrpNoAllocs instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.AllocAccount = view.GetString(79);
			instance.AllocAcctIDSource = view.GetInt32(661);
			instance.AllocPrice = view.GetDouble(366);
			instance.IndividualAllocID = view.GetString(467);
			instance.IndividualAllocRejCode = view.GetInt32(776);
			instance.AllocText = view.GetString(161);
			instance.EncodedAllocTextLen = view.GetInt32(360);
			instance.EncodedAllocText = view.GetByteArray(361);
		}
	}
}
