using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoLegPaymentStreamPaymentDates : IFixGroup
	{
		[TagDetails(Tag = 41590, Type = TagType.LocalDate, Offset = 0, Required = false)]
		public DateOnly? LegPaymentStreamPaymentDate {get; set;}
		
		[TagDetails(Tag = 41591, Type = TagType.Int, Offset = 1, Required = false)]
		public int? LegPaymentStreamPaymentDateType {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegPaymentStreamPaymentDate is not null) writer.WriteLocalDateOnly(41590, LegPaymentStreamPaymentDate.Value);
			if (LegPaymentStreamPaymentDateType is not null) writer.WriteWholeNumber(41591, LegPaymentStreamPaymentDateType.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegPaymentStreamPaymentDate = view.GetDateOnly(41590);
			LegPaymentStreamPaymentDateType = view.GetInt32(41591);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegPaymentStreamPaymentDate":
					value = LegPaymentStreamPaymentDate;
					break;
				case "LegPaymentStreamPaymentDateType":
					value = LegPaymentStreamPaymentDateType;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			LegPaymentStreamPaymentDate = null;
			LegPaymentStreamPaymentDateType = null;
		}
	}
}
