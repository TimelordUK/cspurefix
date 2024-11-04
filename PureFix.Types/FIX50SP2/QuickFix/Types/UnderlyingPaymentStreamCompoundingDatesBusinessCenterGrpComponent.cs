using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingPaymentStreamCompoundingDatesBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42915, Offset = 0, Required = false)]
		public NoUnderlyingPaymentStreamCompoundingDatesBusinessCenters[]? NoUnderlyingPaymentStreamCompoundingDatesBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingPaymentStreamCompoundingDatesBusinessCenters is not null && NoUnderlyingPaymentStreamCompoundingDatesBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(42915, NoUnderlyingPaymentStreamCompoundingDatesBusinessCenters.Length);
				for (int i = 0; i < NoUnderlyingPaymentStreamCompoundingDatesBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoUnderlyingPaymentStreamCompoundingDatesBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingPaymentStreamCompoundingDatesBusinessCenters") is IMessageView viewNoUnderlyingPaymentStreamCompoundingDatesBusinessCenters)
			{
				var count = viewNoUnderlyingPaymentStreamCompoundingDatesBusinessCenters.GroupCount();
				NoUnderlyingPaymentStreamCompoundingDatesBusinessCenters = new NoUnderlyingPaymentStreamCompoundingDatesBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingPaymentStreamCompoundingDatesBusinessCenters[i] = new();
					((IFixParser)NoUnderlyingPaymentStreamCompoundingDatesBusinessCenters[i]).Parse(viewNoUnderlyingPaymentStreamCompoundingDatesBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingPaymentStreamCompoundingDatesBusinessCenters":
					value = NoUnderlyingPaymentStreamCompoundingDatesBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingPaymentStreamCompoundingDatesBusinessCenters = null;
		}
	}
}
