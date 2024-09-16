using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class CollInqQualGrpExt
	{
		public static void Parse(this CollInqQualGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupViewNoCollInquiryQualifier = view.GetView("NoCollInquiryQualifier");
			if (groupViewNoCollInquiryQualifier is null) return;
			
			var countNoCollInquiryQualifier = groupViewNoCollInquiryQualifier.GroupCount();
			instance.NoCollInquiryQualifier = new CollInqQualGrpNoCollInquiryQualifier[countNoCollInquiryQualifier];
			for (var i = 0; i < countNoCollInquiryQualifier; ++i)
			{
				instance.NoCollInquiryQualifier[i] = new();
				instance.NoCollInquiryQualifier[i].Parse(groupViewNoCollInquiryQualifier[i]);
			}
		}
	}
}
