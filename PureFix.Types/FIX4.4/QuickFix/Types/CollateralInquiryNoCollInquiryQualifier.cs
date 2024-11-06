using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class CollateralInquiryNoCollInquiryQualifier : IFixGroup
	{
		[TagDetails(Tag = 896, Type = TagType.Int, Offset = 0, Required = false)]
		public int? CollInquiryQualifier {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (CollInquiryQualifier is not null) writer.WriteWholeNumber(896, CollInquiryQualifier.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			CollInquiryQualifier = view.GetInt32(896);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "CollInquiryQualifier":
					value = CollInquiryQualifier;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			CollInquiryQualifier = null;
		}
	}
}
