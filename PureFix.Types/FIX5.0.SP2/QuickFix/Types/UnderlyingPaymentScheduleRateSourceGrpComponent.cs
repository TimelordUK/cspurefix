using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingPaymentScheduleRateSourceGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40704, Offset = 0, Required = false)]
		public NoUnderlyingPaymentScheduleRateSources[]? NoUnderlyingPaymentScheduleRateSources {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingPaymentScheduleRateSources is not null && NoUnderlyingPaymentScheduleRateSources.Length != 0)
			{
				writer.WriteWholeNumber(40704, NoUnderlyingPaymentScheduleRateSources.Length);
				for (int i = 0; i < NoUnderlyingPaymentScheduleRateSources.Length; i++)
				{
					((IFixEncoder)NoUnderlyingPaymentScheduleRateSources[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingPaymentScheduleRateSources") is IMessageView viewNoUnderlyingPaymentScheduleRateSources)
			{
				var count = viewNoUnderlyingPaymentScheduleRateSources.GroupCount();
				NoUnderlyingPaymentScheduleRateSources = new NoUnderlyingPaymentScheduleRateSources[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingPaymentScheduleRateSources[i] = new();
					((IFixParser)NoUnderlyingPaymentScheduleRateSources[i]).Parse(viewNoUnderlyingPaymentScheduleRateSources.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingPaymentScheduleRateSources":
					value = NoUnderlyingPaymentScheduleRateSources;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingPaymentScheduleRateSources = null;
		}
	}
}
