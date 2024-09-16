using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class TargetPtysSubGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 2433, Offset = 0, Required = false)]
		public NoTargetPartySubIDs[]? NoTargetPartySubIDs {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoTargetPartySubIDs is not null && NoTargetPartySubIDs.Length != 0)
			{
				writer.WriteWholeNumber(2433, NoTargetPartySubIDs.Length);
				for (int i = 0; i < NoTargetPartySubIDs.Length; i++)
				{
					((IFixEncoder)NoTargetPartySubIDs[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoTargetPartySubIDs") is IMessageView viewNoTargetPartySubIDs)
			{
				var count = viewNoTargetPartySubIDs.GroupCount();
				NoTargetPartySubIDs = new NoTargetPartySubIDs[count];
				for (int i = 0; i < count; i++)
				{
					NoTargetPartySubIDs[i] = new();
					((IFixParser)NoTargetPartySubIDs[i]).Parse(viewNoTargetPartySubIDs.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoTargetPartySubIDs":
					value = NoTargetPartySubIDs;
					break;
				default: return false;
			}
			return true;
		}
	}
}
