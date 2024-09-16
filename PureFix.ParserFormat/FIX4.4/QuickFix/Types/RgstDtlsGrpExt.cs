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
			
			var groupViewNoRegistDtls = view.GetView("NoRegistDtls");
			if (groupViewNoRegistDtls is null) return;
			
			var countNoRegistDtls = groupViewNoRegistDtls.GroupCount();
			instance.NoRegistDtls = new RgstDtlsGrpNoRegistDtls[countNoRegistDtls];
			for (var i = 0; i < countNoRegistDtls; ++i)
			{
				instance.NoRegistDtls[i] = new();
				instance.NoRegistDtls[i].Parse(groupViewNoRegistDtls[i]);
			}
		}
	}
}
