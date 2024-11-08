using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingPaymentStreamPaymentDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40969, Offset = 0, Required = false)]
		public IOINoUnderlyingPaymentStreamPaymentDateBusinessCenters[]? NoUnderlyingPaymentStreamPaymentDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingPaymentStreamPaymentDateBusinessCenters is not null && NoUnderlyingPaymentStreamPaymentDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(40969, NoUnderlyingPaymentStreamPaymentDateBusinessCenters.Length);
				for (int i = 0; i < NoUnderlyingPaymentStreamPaymentDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoUnderlyingPaymentStreamPaymentDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingPaymentStreamPaymentDateBusinessCenters") is IMessageView viewNoUnderlyingPaymentStreamPaymentDateBusinessCenters)
			{
				var count = viewNoUnderlyingPaymentStreamPaymentDateBusinessCenters.GroupCount();
				NoUnderlyingPaymentStreamPaymentDateBusinessCenters = new IOINoUnderlyingPaymentStreamPaymentDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingPaymentStreamPaymentDateBusinessCenters[i] = new();
					((IFixParser)NoUnderlyingPaymentStreamPaymentDateBusinessCenters[i]).Parse(viewNoUnderlyingPaymentStreamPaymentDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingPaymentStreamPaymentDateBusinessCenters":
					value = NoUnderlyingPaymentStreamPaymentDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingPaymentStreamPaymentDateBusinessCenters = null;
		}
	}
}
