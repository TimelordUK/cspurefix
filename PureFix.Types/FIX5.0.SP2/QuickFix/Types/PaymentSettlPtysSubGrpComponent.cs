using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class PaymentSettlPtysSubGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40238, Offset = 0, Required = false)]
		public NoPaymentSettlPartySubIDs[]? NoPaymentSettlPartySubIDs {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoPaymentSettlPartySubIDs is not null && NoPaymentSettlPartySubIDs.Length != 0)
			{
				writer.WriteWholeNumber(40238, NoPaymentSettlPartySubIDs.Length);
				for (int i = 0; i < NoPaymentSettlPartySubIDs.Length; i++)
				{
					((IFixEncoder)NoPaymentSettlPartySubIDs[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoPaymentSettlPartySubIDs") is IMessageView viewNoPaymentSettlPartySubIDs)
			{
				var count = viewNoPaymentSettlPartySubIDs.GroupCount();
				NoPaymentSettlPartySubIDs = new NoPaymentSettlPartySubIDs[count];
				for (int i = 0; i < count; i++)
				{
					NoPaymentSettlPartySubIDs[i] = new();
					((IFixParser)NoPaymentSettlPartySubIDs[i]).Parse(viewNoPaymentSettlPartySubIDs.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoPaymentSettlPartySubIDs":
					value = NoPaymentSettlPartySubIDs;
					break;
				default: return false;
			}
			return true;
		}
	}
}
