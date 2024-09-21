using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingPaymentStreamNonDeliverableFixingDatesBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40968, Offset = 0, Required = false)]
		public NoUnderlyingPaymentStreamNonDeliverableFixingDatesBizCenters[]? NoUnderlyingPaymentStreamNonDeliverableFixingDatesBizCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingPaymentStreamNonDeliverableFixingDatesBizCenters is not null && NoUnderlyingPaymentStreamNonDeliverableFixingDatesBizCenters.Length != 0)
			{
				writer.WriteWholeNumber(40968, NoUnderlyingPaymentStreamNonDeliverableFixingDatesBizCenters.Length);
				for (int i = 0; i < NoUnderlyingPaymentStreamNonDeliverableFixingDatesBizCenters.Length; i++)
				{
					((IFixEncoder)NoUnderlyingPaymentStreamNonDeliverableFixingDatesBizCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingPaymentStreamNonDeliverableFixingDatesBizCenters") is IMessageView viewNoUnderlyingPaymentStreamNonDeliverableFixingDatesBizCenters)
			{
				var count = viewNoUnderlyingPaymentStreamNonDeliverableFixingDatesBizCenters.GroupCount();
				NoUnderlyingPaymentStreamNonDeliverableFixingDatesBizCenters = new NoUnderlyingPaymentStreamNonDeliverableFixingDatesBizCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingPaymentStreamNonDeliverableFixingDatesBizCenters[i] = new();
					((IFixParser)NoUnderlyingPaymentStreamNonDeliverableFixingDatesBizCenters[i]).Parse(viewNoUnderlyingPaymentStreamNonDeliverableFixingDatesBizCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingPaymentStreamNonDeliverableFixingDatesBizCenters":
					value = NoUnderlyingPaymentStreamNonDeliverableFixingDatesBizCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingPaymentStreamNonDeliverableFixingDatesBizCenters = null;
		}
	}
}
