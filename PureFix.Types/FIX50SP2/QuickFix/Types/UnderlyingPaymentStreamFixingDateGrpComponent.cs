using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingPaymentStreamFixingDateGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42955, Offset = 0, Required = false)]
		public NoUnderlyingPaymentStreamFixingDates[]? NoUnderlyingPaymentStreamFixingDates {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingPaymentStreamFixingDates is not null && NoUnderlyingPaymentStreamFixingDates.Length != 0)
			{
				writer.WriteWholeNumber(42955, NoUnderlyingPaymentStreamFixingDates.Length);
				for (int i = 0; i < NoUnderlyingPaymentStreamFixingDates.Length; i++)
				{
					((IFixEncoder)NoUnderlyingPaymentStreamFixingDates[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingPaymentStreamFixingDates") is IMessageView viewNoUnderlyingPaymentStreamFixingDates)
			{
				var count = viewNoUnderlyingPaymentStreamFixingDates.GroupCount();
				NoUnderlyingPaymentStreamFixingDates = new NoUnderlyingPaymentStreamFixingDates[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingPaymentStreamFixingDates[i] = new();
					((IFixParser)NoUnderlyingPaymentStreamFixingDates[i]).Parse(viewNoUnderlyingPaymentStreamFixingDates.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingPaymentStreamFixingDates":
					value = NoUnderlyingPaymentStreamFixingDates;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingPaymentStreamFixingDates = null;
		}
	}
}
