using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class NestedPartiesComponent : IFixComponent
	{
		[Group(NoOfTag = 539, Offset = 0, Required = false)]
		public NoNestedPartyIDs[]? NoNestedPartyIDs {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoNestedPartyIDs is not null && NoNestedPartyIDs.Length != 0)
			{
				writer.WriteWholeNumber(539, NoNestedPartyIDs.Length);
				for (int i = 0; i < NoNestedPartyIDs.Length; i++)
				{
					((IFixEncoder)NoNestedPartyIDs[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoNestedPartyIDs") is IMessageView viewNoNestedPartyIDs)
			{
				var count = viewNoNestedPartyIDs.GroupCount();
				NoNestedPartyIDs = new NoNestedPartyIDs[count];
				for (int i = 0; i < count; i++)
				{
					NoNestedPartyIDs[i] = new();
					((IFixParser)NoNestedPartyIDs[i]).Parse(viewNoNestedPartyIDs.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoNestedPartyIDs":
					value = NoNestedPartyIDs;
					break;
				default: return false;
			}
			return true;
		}
	}
}
