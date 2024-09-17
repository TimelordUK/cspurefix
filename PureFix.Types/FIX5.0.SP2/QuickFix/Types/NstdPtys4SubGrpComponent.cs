using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NstdPtys4SubGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1413, Offset = 0, Required = false)]
		public NoNested4PartySubIDs[]? NoNested4PartySubIDs {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoNested4PartySubIDs is not null && NoNested4PartySubIDs.Length != 0)
			{
				writer.WriteWholeNumber(1413, NoNested4PartySubIDs.Length);
				for (int i = 0; i < NoNested4PartySubIDs.Length; i++)
				{
					((IFixEncoder)NoNested4PartySubIDs[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoNested4PartySubIDs") is IMessageView viewNoNested4PartySubIDs)
			{
				var count = viewNoNested4PartySubIDs.GroupCount();
				NoNested4PartySubIDs = new NoNested4PartySubIDs[count];
				for (int i = 0; i < count; i++)
				{
					NoNested4PartySubIDs[i] = new();
					((IFixParser)NoNested4PartySubIDs[i]).Parse(viewNoNested4PartySubIDs.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoNested4PartySubIDs":
					value = NoNested4PartySubIDs;
					break;
				default: return false;
			}
			return true;
		}
	}
}
