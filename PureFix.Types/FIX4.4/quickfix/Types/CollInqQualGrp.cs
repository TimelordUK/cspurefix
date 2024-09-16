using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class CollInqQualGrp : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 938, Offset = 0, Required = false)]
		public CollInqQualGrpNoCollInquiryQualifier[]? NoCollInquiryQualifier { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoCollInquiryQualifier is not null && NoCollInquiryQualifier.Length != 0)
			{
				writer.WriteWholeNumber(938, NoCollInquiryQualifier.Length);
				for (int i = 0; i < NoCollInquiryQualifier.Length; i++)
				{
					((IFixEncoder)NoCollInquiryQualifier[i]).Encode(writer);
				}
			}
		}
	}
}
