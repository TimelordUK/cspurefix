using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class InstrumentPtysSubGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1052, Offset = 0, Required = false)]
		public IOINoInstrumentPartySubIDs[]? NoInstrumentPartySubIDs {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoInstrumentPartySubIDs is not null && NoInstrumentPartySubIDs.Length != 0)
			{
				writer.WriteWholeNumber(1052, NoInstrumentPartySubIDs.Length);
				for (int i = 0; i < NoInstrumentPartySubIDs.Length; i++)
				{
					((IFixEncoder)NoInstrumentPartySubIDs[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoInstrumentPartySubIDs") is IMessageView viewNoInstrumentPartySubIDs)
			{
				var count = viewNoInstrumentPartySubIDs.GroupCount();
				NoInstrumentPartySubIDs = new IOINoInstrumentPartySubIDs[count];
				for (int i = 0; i < count; i++)
				{
					NoInstrumentPartySubIDs[i] = new();
					((IFixParser)NoInstrumentPartySubIDs[i]).Parse(viewNoInstrumentPartySubIDs.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoInstrumentPartySubIDs":
					value = NoInstrumentPartySubIDs;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoInstrumentPartySubIDs = null;
		}
	}
}
