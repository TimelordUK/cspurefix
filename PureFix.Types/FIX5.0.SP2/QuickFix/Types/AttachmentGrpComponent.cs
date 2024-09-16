using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class AttachmentGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 2104, Offset = 0, Required = false)]
		public NoAttachments[]? NoAttachments {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoAttachments is not null && NoAttachments.Length != 0)
			{
				writer.WriteWholeNumber(2104, NoAttachments.Length);
				for (int i = 0; i < NoAttachments.Length; i++)
				{
					((IFixEncoder)NoAttachments[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoAttachments") is IMessageView viewNoAttachments)
			{
				var count = viewNoAttachments.GroupCount();
				NoAttachments = new NoAttachments[count];
				for (int i = 0; i < count; i++)
				{
					NoAttachments[i] = new();
					((IFixParser)NoAttachments[i]).Parse(viewNoAttachments.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoAttachments":
					value = NoAttachments;
					break;
				default: return false;
			}
			return true;
		}
	}
}
