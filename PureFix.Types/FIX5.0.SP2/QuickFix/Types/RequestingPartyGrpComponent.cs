using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class RequestingPartyGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1657, Offset = 0, Required = false)]
		public NoRequestingPartyIDs[]? NoRequestingPartyIDs {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoRequestingPartyIDs is not null && NoRequestingPartyIDs.Length != 0)
			{
				writer.WriteWholeNumber(1657, NoRequestingPartyIDs.Length);
				for (int i = 0; i < NoRequestingPartyIDs.Length; i++)
				{
					((IFixEncoder)NoRequestingPartyIDs[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoRequestingPartyIDs") is IMessageView viewNoRequestingPartyIDs)
			{
				var count = viewNoRequestingPartyIDs.GroupCount();
				NoRequestingPartyIDs = new NoRequestingPartyIDs[count];
				for (int i = 0; i < count; i++)
				{
					NoRequestingPartyIDs[i] = new();
					((IFixParser)NoRequestingPartyIDs[i]).Parse(viewNoRequestingPartyIDs.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoRequestingPartyIDs":
					value = NoRequestingPartyIDs;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoRequestingPartyIDs = null;
		}
	}
}
