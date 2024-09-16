using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public static class SecurityTypesNoSecurityTypesExt
	{
		public static void Parse(this SecurityTypesNoSecurityTypes instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.SecurityType = view.GetString(167);
			instance.Product = view.GetInt32(460);
			instance.CFICode = view.GetString(461);
		}
	}
}
