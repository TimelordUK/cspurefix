using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NstdPtys3SubGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 952, Offset = 0, Required = false)]
		public ExecutionReportNoNested3PartySubIDs[]? NoNested3PartySubIDs {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoNested3PartySubIDs is not null && NoNested3PartySubIDs.Length != 0)
			{
				writer.WriteWholeNumber(952, NoNested3PartySubIDs.Length);
				for (int i = 0; i < NoNested3PartySubIDs.Length; i++)
				{
					((IFixEncoder)NoNested3PartySubIDs[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoNested3PartySubIDs") is IMessageView viewNoNested3PartySubIDs)
			{
				var count = viewNoNested3PartySubIDs.GroupCount();
				NoNested3PartySubIDs = new ExecutionReportNoNested3PartySubIDs[count];
				for (int i = 0; i < count; i++)
				{
					NoNested3PartySubIDs[i] = new();
					((IFixParser)NoNested3PartySubIDs[i]).Parse(viewNoNested3PartySubIDs.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoNested3PartySubIDs":
					value = NoNested3PartySubIDs;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoNested3PartySubIDs = null;
		}
	}
}
