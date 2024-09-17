using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class SettlPtysSubGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 801, Offset = 0, Required = false)]
		public NoSettlPartySubIDs[]? NoSettlPartySubIDs {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoSettlPartySubIDs is not null && NoSettlPartySubIDs.Length != 0)
			{
				writer.WriteWholeNumber(801, NoSettlPartySubIDs.Length);
				for (int i = 0; i < NoSettlPartySubIDs.Length; i++)
				{
					((IFixEncoder)NoSettlPartySubIDs[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoSettlPartySubIDs") is IMessageView viewNoSettlPartySubIDs)
			{
				var count = viewNoSettlPartySubIDs.GroupCount();
				NoSettlPartySubIDs = new NoSettlPartySubIDs[count];
				for (int i = 0; i < count; i++)
				{
					NoSettlPartySubIDs[i] = new();
					((IFixParser)NoSettlPartySubIDs[i]).Parse(viewNoSettlPartySubIDs.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoSettlPartySubIDs":
					value = NoSettlPartySubIDs;
					break;
				default: return false;
			}
			return true;
		}
	}
}
