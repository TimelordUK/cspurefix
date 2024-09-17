using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class AttachmentKeywordGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 2113, Offset = 0, Required = false)]
		public NoAttachmentKeywords[]? NoAttachmentKeywords {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoAttachmentKeywords is not null && NoAttachmentKeywords.Length != 0)
			{
				writer.WriteWholeNumber(2113, NoAttachmentKeywords.Length);
				for (int i = 0; i < NoAttachmentKeywords.Length; i++)
				{
					((IFixEncoder)NoAttachmentKeywords[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoAttachmentKeywords") is IMessageView viewNoAttachmentKeywords)
			{
				var count = viewNoAttachmentKeywords.GroupCount();
				NoAttachmentKeywords = new NoAttachmentKeywords[count];
				for (int i = 0; i < count; i++)
				{
					NoAttachmentKeywords[i] = new();
					((IFixParser)NoAttachmentKeywords[i]).Parse(viewNoAttachmentKeywords.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoAttachmentKeywords":
					value = NoAttachmentKeywords;
					break;
				default: return false;
			}
			return true;
		}
	}
}
