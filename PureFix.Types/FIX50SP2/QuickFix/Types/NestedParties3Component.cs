using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NestedParties3Component : IFixComponent
	{
		[Group(NoOfTag = 948, Offset = 0, Required = false)]
		public NoNested3PartyIDs[]? NoNested3PartyIDs {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoNested3PartyIDs is not null && NoNested3PartyIDs.Length != 0)
			{
				writer.WriteWholeNumber(948, NoNested3PartyIDs.Length);
				for (int i = 0; i < NoNested3PartyIDs.Length; i++)
				{
					((IFixEncoder)NoNested3PartyIDs[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoNested3PartyIDs") is IMessageView viewNoNested3PartyIDs)
			{
				var count = viewNoNested3PartyIDs.GroupCount();
				NoNested3PartyIDs = new NoNested3PartyIDs[count];
				for (int i = 0; i < count; i++)
				{
					NoNested3PartyIDs[i] = new();
					((IFixParser)NoNested3PartyIDs[i]).Parse(viewNoNested3PartyIDs.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoNested3PartyIDs":
					value = NoNested3PartyIDs;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoNested3PartyIDs = null;
		}
	}
}
