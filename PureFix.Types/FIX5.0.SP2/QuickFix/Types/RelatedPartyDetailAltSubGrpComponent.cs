using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class RelatedPartyDetailAltSubGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1572, Offset = 0, Required = false)]
		public NoRelatedPartyDetailAltSubIDs[]? NoRelatedPartyDetailAltSubIDs {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoRelatedPartyDetailAltSubIDs is not null && NoRelatedPartyDetailAltSubIDs.Length != 0)
			{
				writer.WriteWholeNumber(1572, NoRelatedPartyDetailAltSubIDs.Length);
				for (int i = 0; i < NoRelatedPartyDetailAltSubIDs.Length; i++)
				{
					((IFixEncoder)NoRelatedPartyDetailAltSubIDs[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoRelatedPartyDetailAltSubIDs") is IMessageView viewNoRelatedPartyDetailAltSubIDs)
			{
				var count = viewNoRelatedPartyDetailAltSubIDs.GroupCount();
				NoRelatedPartyDetailAltSubIDs = new NoRelatedPartyDetailAltSubIDs[count];
				for (int i = 0; i < count; i++)
				{
					NoRelatedPartyDetailAltSubIDs[i] = new();
					((IFixParser)NoRelatedPartyDetailAltSubIDs[i]).Parse(viewNoRelatedPartyDetailAltSubIDs.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoRelatedPartyDetailAltSubIDs":
					value = NoRelatedPartyDetailAltSubIDs;
					break;
				default: return false;
			}
			return true;
		}
	}
}
