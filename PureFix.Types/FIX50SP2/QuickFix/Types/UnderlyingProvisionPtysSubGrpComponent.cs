using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingProvisionPtysSubGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42177, Offset = 0, Required = false)]
		public NoUnderlyingProvisionPartySubIDs[]? NoUnderlyingProvisionPartySubIDs {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingProvisionPartySubIDs is not null && NoUnderlyingProvisionPartySubIDs.Length != 0)
			{
				writer.WriteWholeNumber(42177, NoUnderlyingProvisionPartySubIDs.Length);
				for (int i = 0; i < NoUnderlyingProvisionPartySubIDs.Length; i++)
				{
					((IFixEncoder)NoUnderlyingProvisionPartySubIDs[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingProvisionPartySubIDs") is IMessageView viewNoUnderlyingProvisionPartySubIDs)
			{
				var count = viewNoUnderlyingProvisionPartySubIDs.GroupCount();
				NoUnderlyingProvisionPartySubIDs = new NoUnderlyingProvisionPartySubIDs[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingProvisionPartySubIDs[i] = new();
					((IFixParser)NoUnderlyingProvisionPartySubIDs[i]).Parse(viewNoUnderlyingProvisionPartySubIDs.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingProvisionPartySubIDs":
					value = NoUnderlyingProvisionPartySubIDs;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingProvisionPartySubIDs = null;
		}
	}
}
