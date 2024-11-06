using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NestedParties4Component : IFixComponent
	{
		[Group(NoOfTag = 1414, Offset = 0, Required = false)]
		public ExecutionReportNoNested4PartyIDs[]? NoNested4PartyIDs {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoNested4PartyIDs is not null && NoNested4PartyIDs.Length != 0)
			{
				writer.WriteWholeNumber(1414, NoNested4PartyIDs.Length);
				for (int i = 0; i < NoNested4PartyIDs.Length; i++)
				{
					((IFixEncoder)NoNested4PartyIDs[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoNested4PartyIDs") is IMessageView viewNoNested4PartyIDs)
			{
				var count = viewNoNested4PartyIDs.GroupCount();
				NoNested4PartyIDs = new ExecutionReportNoNested4PartyIDs[count];
				for (int i = 0; i < count; i++)
				{
					NoNested4PartyIDs[i] = new();
					((IFixParser)NoNested4PartyIDs[i]).Parse(viewNoNested4PartyIDs.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoNested4PartyIDs":
					value = NoNested4PartyIDs;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoNested4PartyIDs = null;
		}
	}
}
