using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public sealed partial class PartiesComponent : IFixComponent
	{
		[Group(NoOfTag = 453, Offset = 0, Required = false)]
		public NoPartyIDs[]? NoPartyIDs {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoPartyIDs is not null && NoPartyIDs.Length != 0)
			{
				writer.WriteWholeNumber(453, NoPartyIDs.Length);
				for (int i = 0; i < NoPartyIDs.Length; i++)
				{
					((IFixEncoder)NoPartyIDs[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoPartyIDs") is IMessageView viewNoPartyIDs)
			{
				var count = viewNoPartyIDs.GroupCount();
				NoPartyIDs = new NoPartyIDs[count];
				for (int i = 0; i < count; i++)
				{
					NoPartyIDs[i] = new();
					((IFixParser)NoPartyIDs[i]).Parse(viewNoPartyIDs.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoPartyIDs":
					value = NoPartyIDs;
					break;
				default: return false;
			}
			return true;
		}
	}
}
