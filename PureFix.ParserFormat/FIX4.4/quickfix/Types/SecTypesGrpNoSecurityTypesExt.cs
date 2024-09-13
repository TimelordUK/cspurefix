using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class SecTypesGrpNoSecurityTypesExt
	{
		public static void Parse(this SecTypesGrpNoSecurityTypes instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.SecurityType = view.GetString(167);
			instance.SecuritySubType = view.GetString(762);
			instance.Product = view.GetInt32(460);
			instance.CFICode = view.GetString(461);
		}
	}
}
