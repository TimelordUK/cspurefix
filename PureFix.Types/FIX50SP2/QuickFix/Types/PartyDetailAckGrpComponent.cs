using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class PartyDetailAckGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1676, Offset = 0, Required = false)]
		public NoPartyUpdates[]? NoPartyUpdates {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoPartyUpdates is not null && NoPartyUpdates.Length != 0)
			{
				writer.WriteWholeNumber(1676, NoPartyUpdates.Length);
				for (int i = 0; i < NoPartyUpdates.Length; i++)
				{
					((IFixEncoder)NoPartyUpdates[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoPartyUpdates") is IMessageView viewNoPartyUpdates)
			{
				var count = viewNoPartyUpdates.GroupCount();
				NoPartyUpdates = new NoPartyUpdates[count];
				for (int i = 0; i < count; i++)
				{
					NoPartyUpdates[i] = new();
					((IFixParser)NoPartyUpdates[i]).Parse(viewNoPartyUpdates.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoPartyUpdates":
					value = NoPartyUpdates;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoPartyUpdates = null;
		}
	}
}
