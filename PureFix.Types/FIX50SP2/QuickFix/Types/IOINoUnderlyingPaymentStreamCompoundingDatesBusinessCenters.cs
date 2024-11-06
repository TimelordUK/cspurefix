using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoUnderlyingPaymentStreamCompoundingDatesBusinessCenters : IFixGroup
	{
		[TagDetails(Tag = 42916, Type = TagType.String, Offset = 0, Required = false)]
		public string? UnderlyingPaymentStreamCompoundingDatesBusinessCenter {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingPaymentStreamCompoundingDatesBusinessCenter is not null) writer.WriteString(42916, UnderlyingPaymentStreamCompoundingDatesBusinessCenter);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingPaymentStreamCompoundingDatesBusinessCenter = view.GetString(42916);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingPaymentStreamCompoundingDatesBusinessCenter":
					value = UnderlyingPaymentStreamCompoundingDatesBusinessCenter;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			UnderlyingPaymentStreamCompoundingDatesBusinessCenter = null;
		}
	}
}
