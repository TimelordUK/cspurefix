using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoUnderlyingProvisionCashSettlPaymentDates : IFixGroup
	{
		[TagDetails(Tag = 42100, Type = TagType.LocalDate, Offset = 0, Required = false)]
		public DateOnly? UnderlyingProvisionCashSettlPaymentDate {get; set;}
		
		[TagDetails(Tag = 42101, Type = TagType.Int, Offset = 1, Required = false)]
		public int? UnderlyingProvisionCashSettlPaymentDateType {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingProvisionCashSettlPaymentDate is not null) writer.WriteLocalDateOnly(42100, UnderlyingProvisionCashSettlPaymentDate.Value);
			if (UnderlyingProvisionCashSettlPaymentDateType is not null) writer.WriteWholeNumber(42101, UnderlyingProvisionCashSettlPaymentDateType.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingProvisionCashSettlPaymentDate = view.GetDateOnly(42100);
			UnderlyingProvisionCashSettlPaymentDateType = view.GetInt32(42101);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingProvisionCashSettlPaymentDate":
					value = UnderlyingProvisionCashSettlPaymentDate;
					break;
				case "UnderlyingProvisionCashSettlPaymentDateType":
					value = UnderlyingProvisionCashSettlPaymentDateType;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			UnderlyingProvisionCashSettlPaymentDate = null;
			UnderlyingProvisionCashSettlPaymentDateType = null;
		}
	}
}
