using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class RequestingPartySubGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1661, Offset = 0, Required = false)]
		public NoRequestingPartySubIDs[]? NoRequestingPartySubIDs {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoRequestingPartySubIDs is not null && NoRequestingPartySubIDs.Length != 0)
			{
				writer.WriteWholeNumber(1661, NoRequestingPartySubIDs.Length);
				for (int i = 0; i < NoRequestingPartySubIDs.Length; i++)
				{
					((IFixEncoder)NoRequestingPartySubIDs[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoRequestingPartySubIDs") is IMessageView viewNoRequestingPartySubIDs)
			{
				var count = viewNoRequestingPartySubIDs.GroupCount();
				NoRequestingPartySubIDs = new NoRequestingPartySubIDs[count];
				for (int i = 0; i < count; i++)
				{
					NoRequestingPartySubIDs[i] = new();
					((IFixParser)NoRequestingPartySubIDs[i]).Parse(viewNoRequestingPartySubIDs.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoRequestingPartySubIDs":
					value = NoRequestingPartySubIDs;
					break;
				default: return false;
			}
			return true;
		}
	}
}
