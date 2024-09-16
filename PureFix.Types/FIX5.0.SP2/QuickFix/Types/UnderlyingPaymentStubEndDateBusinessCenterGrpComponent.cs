using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingPaymentStubEndDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42991, Offset = 0, Required = false)]
		public NoUnderlyingPaymentStubEndDateBusinessCenters[]? NoUnderlyingPaymentStubEndDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingPaymentStubEndDateBusinessCenters is not null && NoUnderlyingPaymentStubEndDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(42991, NoUnderlyingPaymentStubEndDateBusinessCenters.Length);
				for (int i = 0; i < NoUnderlyingPaymentStubEndDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoUnderlyingPaymentStubEndDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingPaymentStubEndDateBusinessCenters") is IMessageView viewNoUnderlyingPaymentStubEndDateBusinessCenters)
			{
				var count = viewNoUnderlyingPaymentStubEndDateBusinessCenters.GroupCount();
				NoUnderlyingPaymentStubEndDateBusinessCenters = new NoUnderlyingPaymentStubEndDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingPaymentStubEndDateBusinessCenters[i] = new();
					((IFixParser)NoUnderlyingPaymentStubEndDateBusinessCenters[i]).Parse(viewNoUnderlyingPaymentStubEndDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingPaymentStubEndDateBusinessCenters":
					value = NoUnderlyingPaymentStubEndDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
	}
}
