using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegProvisionPtysSubGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40537, Offset = 0, Required = false)]
		public NoLegProvisionPartySubIDs[]? NoLegProvisionPartySubIDs {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegProvisionPartySubIDs is not null && NoLegProvisionPartySubIDs.Length != 0)
			{
				writer.WriteWholeNumber(40537, NoLegProvisionPartySubIDs.Length);
				for (int i = 0; i < NoLegProvisionPartySubIDs.Length; i++)
				{
					((IFixEncoder)NoLegProvisionPartySubIDs[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegProvisionPartySubIDs") is IMessageView viewNoLegProvisionPartySubIDs)
			{
				var count = viewNoLegProvisionPartySubIDs.GroupCount();
				NoLegProvisionPartySubIDs = new NoLegProvisionPartySubIDs[count];
				for (int i = 0; i < count; i++)
				{
					NoLegProvisionPartySubIDs[i] = new();
					((IFixParser)NoLegProvisionPartySubIDs[i]).Parse(viewNoLegProvisionPartySubIDs.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegProvisionPartySubIDs":
					value = NoLegProvisionPartySubIDs;
					break;
				default: return false;
			}
			return true;
		}
	}
}
