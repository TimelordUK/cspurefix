using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class RgstDtlsGrpExt
	{
		public static void Parse(this RgstDtlsGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var count = view.GroupCount();
			instance.NoRegistDtls = new RgstDtlsGrpNoRegistDtls [count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoRegistDtls[i] = new();
				instance.NoRegistDtls[i].Parse(view[i]);
			}
		}
	}
}
