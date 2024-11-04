using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class PaymentScheduleRateSourceGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40868, Offset = 0, Required = false)]
		public NoPaymentScheduleRateSources[]? NoPaymentScheduleRateSources {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoPaymentScheduleRateSources is not null && NoPaymentScheduleRateSources.Length != 0)
			{
				writer.WriteWholeNumber(40868, NoPaymentScheduleRateSources.Length);
				for (int i = 0; i < NoPaymentScheduleRateSources.Length; i++)
				{
					((IFixEncoder)NoPaymentScheduleRateSources[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoPaymentScheduleRateSources") is IMessageView viewNoPaymentScheduleRateSources)
			{
				var count = viewNoPaymentScheduleRateSources.GroupCount();
				NoPaymentScheduleRateSources = new NoPaymentScheduleRateSources[count];
				for (int i = 0; i < count; i++)
				{
					NoPaymentScheduleRateSources[i] = new();
					((IFixParser)NoPaymentScheduleRateSources[i]).Parse(viewNoPaymentScheduleRateSources.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoPaymentScheduleRateSources":
					value = NoPaymentScheduleRateSources;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoPaymentScheduleRateSources = null;
		}
	}
}
