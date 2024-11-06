using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingPaymentStubStartDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 43000, Offset = 0, Required = false)]
		public IOINoUnderlyingPaymentStubStartDateBusinessCenters[]? NoUnderlyingPaymentStubStartDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingPaymentStubStartDateBusinessCenters is not null && NoUnderlyingPaymentStubStartDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(43000, NoUnderlyingPaymentStubStartDateBusinessCenters.Length);
				for (int i = 0; i < NoUnderlyingPaymentStubStartDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoUnderlyingPaymentStubStartDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingPaymentStubStartDateBusinessCenters") is IMessageView viewNoUnderlyingPaymentStubStartDateBusinessCenters)
			{
				var count = viewNoUnderlyingPaymentStubStartDateBusinessCenters.GroupCount();
				NoUnderlyingPaymentStubStartDateBusinessCenters = new IOINoUnderlyingPaymentStubStartDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingPaymentStubStartDateBusinessCenters[i] = new();
					((IFixParser)NoUnderlyingPaymentStubStartDateBusinessCenters[i]).Parse(viewNoUnderlyingPaymentStubStartDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingPaymentStubStartDateBusinessCenters":
					value = NoUnderlyingPaymentStubStartDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingPaymentStubStartDateBusinessCenters = null;
		}
	}
}
