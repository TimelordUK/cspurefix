using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class IOIQualGrpNoIOIQualifiersExt
	{
		public static void Parse(this IOIQualGrpNoIOIQualifiers instance, MsgView? view)
		{
			instance.IOIQualifier = view?.GetString(104);
		}
	}
}
