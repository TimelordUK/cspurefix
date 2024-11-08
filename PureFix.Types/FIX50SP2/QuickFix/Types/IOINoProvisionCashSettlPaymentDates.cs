using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoProvisionCashSettlPaymentDates : IFixGroup
	{
		[TagDetails(Tag = 40172, Type = TagType.LocalDate, Offset = 0, Required = false)]
		public DateOnly? ProvisionCashSettlPaymentDate {get; set;}
		
		[TagDetails(Tag = 40173, Type = TagType.Int, Offset = 1, Required = false)]
		public int? ProvisionCashSettlPaymentDateType {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (ProvisionCashSettlPaymentDate is not null) writer.WriteLocalDateOnly(40172, ProvisionCashSettlPaymentDate.Value);
			if (ProvisionCashSettlPaymentDateType is not null) writer.WriteWholeNumber(40173, ProvisionCashSettlPaymentDateType.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			ProvisionCashSettlPaymentDate = view.GetDateOnly(40172);
			ProvisionCashSettlPaymentDateType = view.GetInt32(40173);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "ProvisionCashSettlPaymentDate":
					value = ProvisionCashSettlPaymentDate;
					break;
				case "ProvisionCashSettlPaymentDateType":
					value = ProvisionCashSettlPaymentDateType;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			ProvisionCashSettlPaymentDate = null;
			ProvisionCashSettlPaymentDateType = null;
		}
	}
}
