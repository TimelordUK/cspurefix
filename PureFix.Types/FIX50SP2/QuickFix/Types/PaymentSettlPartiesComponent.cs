using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class PaymentSettlPartiesComponent : IFixComponent
	{
		[Group(NoOfTag = 40233, Offset = 0, Required = false)]
		public ExecutionReportNoPaymentSettlPartyIDs[]? NoPaymentSettlPartyIDs {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoPaymentSettlPartyIDs is not null && NoPaymentSettlPartyIDs.Length != 0)
			{
				writer.WriteWholeNumber(40233, NoPaymentSettlPartyIDs.Length);
				for (int i = 0; i < NoPaymentSettlPartyIDs.Length; i++)
				{
					((IFixEncoder)NoPaymentSettlPartyIDs[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoPaymentSettlPartyIDs") is IMessageView viewNoPaymentSettlPartyIDs)
			{
				var count = viewNoPaymentSettlPartyIDs.GroupCount();
				NoPaymentSettlPartyIDs = new ExecutionReportNoPaymentSettlPartyIDs[count];
				for (int i = 0; i < count; i++)
				{
					NoPaymentSettlPartyIDs[i] = new();
					((IFixParser)NoPaymentSettlPartyIDs[i]).Parse(viewNoPaymentSettlPartyIDs.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoPaymentSettlPartyIDs":
					value = NoPaymentSettlPartyIDs;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoPaymentSettlPartyIDs = null;
		}
	}
}
