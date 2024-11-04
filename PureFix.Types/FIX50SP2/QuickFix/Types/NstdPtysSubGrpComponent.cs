using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NstdPtysSubGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 804, Offset = 0, Required = false)]
		public NoNestedPartySubIDs[]? NoNestedPartySubIDs {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoNestedPartySubIDs is not null && NoNestedPartySubIDs.Length != 0)
			{
				writer.WriteWholeNumber(804, NoNestedPartySubIDs.Length);
				for (int i = 0; i < NoNestedPartySubIDs.Length; i++)
				{
					((IFixEncoder)NoNestedPartySubIDs[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoNestedPartySubIDs") is IMessageView viewNoNestedPartySubIDs)
			{
				var count = viewNoNestedPartySubIDs.GroupCount();
				NoNestedPartySubIDs = new NoNestedPartySubIDs[count];
				for (int i = 0; i < count; i++)
				{
					NoNestedPartySubIDs[i] = new();
					((IFixParser)NoNestedPartySubIDs[i]).Parse(viewNoNestedPartySubIDs.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoNestedPartySubIDs":
					value = NoNestedPartySubIDs;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoNestedPartySubIDs = null;
		}
	}
}
