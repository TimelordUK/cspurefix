using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegPaymentStreamPaymentDateGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41589, Offset = 0, Required = false)]
		public NoLegPaymentStreamPaymentDates[]? NoLegPaymentStreamPaymentDates {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegPaymentStreamPaymentDates is not null && NoLegPaymentStreamPaymentDates.Length != 0)
			{
				writer.WriteWholeNumber(41589, NoLegPaymentStreamPaymentDates.Length);
				for (int i = 0; i < NoLegPaymentStreamPaymentDates.Length; i++)
				{
					((IFixEncoder)NoLegPaymentStreamPaymentDates[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegPaymentStreamPaymentDates") is IMessageView viewNoLegPaymentStreamPaymentDates)
			{
				var count = viewNoLegPaymentStreamPaymentDates.GroupCount();
				NoLegPaymentStreamPaymentDates = new NoLegPaymentStreamPaymentDates[count];
				for (int i = 0; i < count; i++)
				{
					NoLegPaymentStreamPaymentDates[i] = new();
					((IFixParser)NoLegPaymentStreamPaymentDates[i]).Parse(viewNoLegPaymentStreamPaymentDates.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegPaymentStreamPaymentDates":
					value = NoLegPaymentStreamPaymentDates;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegPaymentStreamPaymentDates = null;
		}
	}
}
