using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class CollInqQualGrpNoCollInquiryQualifierExt
	{
		public static void Parse(this CollInqQualGrpNoCollInquiryQualifier instance, MsgView? view)
		{
			instance.CollInquiryQualifier = view?.GetInt32(896);
		}
	}
}
