using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class MarginReqmtInqQualGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1636, Offset = 0, Required = false)]
		public MarginRequirementInquiryNoMarginReqmtInqQualifier[]? NoMarginReqmtInqQualifier {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoMarginReqmtInqQualifier is not null && NoMarginReqmtInqQualifier.Length != 0)
			{
				writer.WriteWholeNumber(1636, NoMarginReqmtInqQualifier.Length);
				for (int i = 0; i < NoMarginReqmtInqQualifier.Length; i++)
				{
					((IFixEncoder)NoMarginReqmtInqQualifier[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoMarginReqmtInqQualifier") is IMessageView viewNoMarginReqmtInqQualifier)
			{
				var count = viewNoMarginReqmtInqQualifier.GroupCount();
				NoMarginReqmtInqQualifier = new MarginRequirementInquiryNoMarginReqmtInqQualifier[count];
				for (int i = 0; i < count; i++)
				{
					NoMarginReqmtInqQualifier[i] = new();
					((IFixParser)NoMarginReqmtInqQualifier[i]).Parse(viewNoMarginReqmtInqQualifier.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoMarginReqmtInqQualifier":
					value = NoMarginReqmtInqQualifier;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoMarginReqmtInqQualifier = null;
		}
	}
}
