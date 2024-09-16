using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public static class IOINoIOIQualifiersExt
	{
		public static void Parse(this IOINoIOIQualifiers instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.IOIQualifier = view.GetString(104);
		}
	}
}
