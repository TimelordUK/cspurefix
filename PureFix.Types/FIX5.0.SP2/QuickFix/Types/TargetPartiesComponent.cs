using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class TargetPartiesComponent : IFixComponent
	{
		[Group(NoOfTag = 1461, Offset = 0, Required = false)]
		public NoTargetPartyIDs[]? NoTargetPartyIDs {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoTargetPartyIDs is not null && NoTargetPartyIDs.Length != 0)
			{
				writer.WriteWholeNumber(1461, NoTargetPartyIDs.Length);
				for (int i = 0; i < NoTargetPartyIDs.Length; i++)
				{
					((IFixEncoder)NoTargetPartyIDs[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoTargetPartyIDs") is IMessageView viewNoTargetPartyIDs)
			{
				var count = viewNoTargetPartyIDs.GroupCount();
				NoTargetPartyIDs = new NoTargetPartyIDs[count];
				for (int i = 0; i < count; i++)
				{
					NoTargetPartyIDs[i] = new();
					((IFixParser)NoTargetPartyIDs[i]).Parse(viewNoTargetPartyIDs.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoTargetPartyIDs":
					value = NoTargetPartyIDs;
					break;
				default: return false;
			}
			return true;
		}
	}
}
