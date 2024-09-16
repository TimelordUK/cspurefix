using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class MDRjctGrpNoAltMDSourceExt
	{
		public static void Parse(this MDRjctGrpNoAltMDSource instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.AltMDSourceID = view.GetString(817);
		}
	}
}
