using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegPaymentStreamNonDeliverableFixingDateGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40367, Offset = 0, Required = false)]
		public NoLegNonDeliverableFixingDates[]? NoLegNonDeliverableFixingDates {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegNonDeliverableFixingDates is not null && NoLegNonDeliverableFixingDates.Length != 0)
			{
				writer.WriteWholeNumber(40367, NoLegNonDeliverableFixingDates.Length);
				for (int i = 0; i < NoLegNonDeliverableFixingDates.Length; i++)
				{
					((IFixEncoder)NoLegNonDeliverableFixingDates[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegNonDeliverableFixingDates") is IMessageView viewNoLegNonDeliverableFixingDates)
			{
				var count = viewNoLegNonDeliverableFixingDates.GroupCount();
				NoLegNonDeliverableFixingDates = new NoLegNonDeliverableFixingDates[count];
				for (int i = 0; i < count; i++)
				{
					NoLegNonDeliverableFixingDates[i] = new();
					((IFixParser)NoLegNonDeliverableFixingDates[i]).Parse(viewNoLegNonDeliverableFixingDates.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegNonDeliverableFixingDates":
					value = NoLegNonDeliverableFixingDates;
					break;
				default: return false;
			}
			return true;
		}
	}
}
