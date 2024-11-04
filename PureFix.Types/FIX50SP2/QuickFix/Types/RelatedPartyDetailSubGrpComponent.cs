using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class RelatedPartyDetailSubGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1566, Offset = 0, Required = false)]
		public NoRelatedPartyDetailSubIDs[]? NoRelatedPartyDetailSubIDs {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoRelatedPartyDetailSubIDs is not null && NoRelatedPartyDetailSubIDs.Length != 0)
			{
				writer.WriteWholeNumber(1566, NoRelatedPartyDetailSubIDs.Length);
				for (int i = 0; i < NoRelatedPartyDetailSubIDs.Length; i++)
				{
					((IFixEncoder)NoRelatedPartyDetailSubIDs[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoRelatedPartyDetailSubIDs") is IMessageView viewNoRelatedPartyDetailSubIDs)
			{
				var count = viewNoRelatedPartyDetailSubIDs.GroupCount();
				NoRelatedPartyDetailSubIDs = new NoRelatedPartyDetailSubIDs[count];
				for (int i = 0; i < count; i++)
				{
					NoRelatedPartyDetailSubIDs[i] = new();
					((IFixParser)NoRelatedPartyDetailSubIDs[i]).Parse(viewNoRelatedPartyDetailSubIDs.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoRelatedPartyDetailSubIDs":
					value = NoRelatedPartyDetailSubIDs;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoRelatedPartyDetailSubIDs = null;
		}
	}
}
