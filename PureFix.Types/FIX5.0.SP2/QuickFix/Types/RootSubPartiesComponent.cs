using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class RootSubPartiesComponent : IFixComponent
	{
		[Group(NoOfTag = 1120, Offset = 0, Required = false)]
		public NoRootPartySubIDs[]? NoRootPartySubIDs {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoRootPartySubIDs is not null && NoRootPartySubIDs.Length != 0)
			{
				writer.WriteWholeNumber(1120, NoRootPartySubIDs.Length);
				for (int i = 0; i < NoRootPartySubIDs.Length; i++)
				{
					((IFixEncoder)NoRootPartySubIDs[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoRootPartySubIDs") is IMessageView viewNoRootPartySubIDs)
			{
				var count = viewNoRootPartySubIDs.GroupCount();
				NoRootPartySubIDs = new NoRootPartySubIDs[count];
				for (int i = 0; i < count; i++)
				{
					NoRootPartySubIDs[i] = new();
					((IFixParser)NoRootPartySubIDs[i]).Parse(viewNoRootPartySubIDs.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoRootPartySubIDs":
					value = NoRootPartySubIDs;
					break;
				default: return false;
			}
			return true;
		}
	}
}
