using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class PaymentStreamFixingDateGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42660, Offset = 0, Required = false)]
		public IOINoPaymentStreamFixingDates[]? NoPaymentStreamFixingDates {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoPaymentStreamFixingDates is not null && NoPaymentStreamFixingDates.Length != 0)
			{
				writer.WriteWholeNumber(42660, NoPaymentStreamFixingDates.Length);
				for (int i = 0; i < NoPaymentStreamFixingDates.Length; i++)
				{
					((IFixEncoder)NoPaymentStreamFixingDates[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoPaymentStreamFixingDates") is IMessageView viewNoPaymentStreamFixingDates)
			{
				var count = viewNoPaymentStreamFixingDates.GroupCount();
				NoPaymentStreamFixingDates = new IOINoPaymentStreamFixingDates[count];
				for (int i = 0; i < count; i++)
				{
					NoPaymentStreamFixingDates[i] = new();
					((IFixParser)NoPaymentStreamFixingDates[i]).Parse(viewNoPaymentStreamFixingDates.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoPaymentStreamFixingDates":
					value = NoPaymentStreamFixingDates;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoPaymentStreamFixingDates = null;
		}
	}
}
