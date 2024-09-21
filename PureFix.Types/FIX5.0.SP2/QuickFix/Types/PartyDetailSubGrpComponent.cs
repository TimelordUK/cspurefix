using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class PartyDetailSubGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1694, Offset = 0, Required = false)]
		public NoPartyDetailSubIDs[]? NoPartyDetailSubIDs {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoPartyDetailSubIDs is not null && NoPartyDetailSubIDs.Length != 0)
			{
				writer.WriteWholeNumber(1694, NoPartyDetailSubIDs.Length);
				for (int i = 0; i < NoPartyDetailSubIDs.Length; i++)
				{
					((IFixEncoder)NoPartyDetailSubIDs[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoPartyDetailSubIDs") is IMessageView viewNoPartyDetailSubIDs)
			{
				var count = viewNoPartyDetailSubIDs.GroupCount();
				NoPartyDetailSubIDs = new NoPartyDetailSubIDs[count];
				for (int i = 0; i < count; i++)
				{
					NoPartyDetailSubIDs[i] = new();
					((IFixParser)NoPartyDetailSubIDs[i]).Parse(viewNoPartyDetailSubIDs.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoPartyDetailSubIDs":
					value = NoPartyDetailSubIDs;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoPartyDetailSubIDs = null;
		}
	}
}
