using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class DerivativeInstrumentPartySubIDsGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1296, Offset = 0, Required = false)]
		public DerivativeSecurityListRequestNoDerivativeInstrumentPartySubIDs[]? NoDerivativeInstrumentPartySubIDs {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoDerivativeInstrumentPartySubIDs is not null && NoDerivativeInstrumentPartySubIDs.Length != 0)
			{
				writer.WriteWholeNumber(1296, NoDerivativeInstrumentPartySubIDs.Length);
				for (int i = 0; i < NoDerivativeInstrumentPartySubIDs.Length; i++)
				{
					((IFixEncoder)NoDerivativeInstrumentPartySubIDs[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoDerivativeInstrumentPartySubIDs") is IMessageView viewNoDerivativeInstrumentPartySubIDs)
			{
				var count = viewNoDerivativeInstrumentPartySubIDs.GroupCount();
				NoDerivativeInstrumentPartySubIDs = new DerivativeSecurityListRequestNoDerivativeInstrumentPartySubIDs[count];
				for (int i = 0; i < count; i++)
				{
					NoDerivativeInstrumentPartySubIDs[i] = new();
					((IFixParser)NoDerivativeInstrumentPartySubIDs[i]).Parse(viewNoDerivativeInstrumentPartySubIDs.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoDerivativeInstrumentPartySubIDs":
					value = NoDerivativeInstrumentPartySubIDs;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoDerivativeInstrumentPartySubIDs = null;
		}
	}
}
