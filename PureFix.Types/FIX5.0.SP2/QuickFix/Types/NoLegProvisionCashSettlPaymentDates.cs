using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoLegProvisionCashSettlPaymentDates : IFixGroup
	{
		[TagDetails(Tag = 40474, Type = TagType.LocalDate, Offset = 0, Required = false)]
		public DateOnly? LegProvisionCashSettlPaymentDate {get; set;}
		
		[TagDetails(Tag = 40475, Type = TagType.Int, Offset = 1, Required = false)]
		public int? LegProvisionCashSettlPaymentDateType {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegProvisionCashSettlPaymentDate is not null) writer.WriteLocalDateOnly(40474, LegProvisionCashSettlPaymentDate.Value);
			if (LegProvisionCashSettlPaymentDateType is not null) writer.WriteWholeNumber(40475, LegProvisionCashSettlPaymentDateType.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegProvisionCashSettlPaymentDate = view.GetDateOnly(40474);
			LegProvisionCashSettlPaymentDateType = view.GetInt32(40475);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegProvisionCashSettlPaymentDate":
					value = LegProvisionCashSettlPaymentDate;
					break;
				case "LegProvisionCashSettlPaymentDateType":
					value = LegProvisionCashSettlPaymentDateType;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			LegProvisionCashSettlPaymentDate = null;
			LegProvisionCashSettlPaymentDateType = null;
		}
	}
}
