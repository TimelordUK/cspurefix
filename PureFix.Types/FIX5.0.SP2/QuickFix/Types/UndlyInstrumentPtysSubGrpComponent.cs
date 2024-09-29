using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UndlyInstrumentPtysSubGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1062, Offset = 0, Required = false)]
		public NoUndlyInstrumentPartySubIDs[]? NoUndlyInstrumentPartySubIDs {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUndlyInstrumentPartySubIDs is not null && NoUndlyInstrumentPartySubIDs.Length != 0)
			{
				writer.WriteWholeNumber(1062, NoUndlyInstrumentPartySubIDs.Length);
				for (int i = 0; i < NoUndlyInstrumentPartySubIDs.Length; i++)
				{
					((IFixEncoder)NoUndlyInstrumentPartySubIDs[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUndlyInstrumentPartySubIDs") is IMessageView viewNoUndlyInstrumentPartySubIDs)
			{
				var count = viewNoUndlyInstrumentPartySubIDs.GroupCount();
				NoUndlyInstrumentPartySubIDs = new NoUndlyInstrumentPartySubIDs[count];
				for (int i = 0; i < count; i++)
				{
					NoUndlyInstrumentPartySubIDs[i] = new();
					((IFixParser)NoUndlyInstrumentPartySubIDs[i]).Parse(viewNoUndlyInstrumentPartySubIDs.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUndlyInstrumentPartySubIDs":
					value = NoUndlyInstrumentPartySubIDs;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUndlyInstrumentPartySubIDs = null;
		}
	}
}
