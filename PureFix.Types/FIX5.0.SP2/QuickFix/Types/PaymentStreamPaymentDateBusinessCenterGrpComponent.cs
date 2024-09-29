using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class PaymentStreamPaymentDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40947, Offset = 0, Required = false)]
		public NoPaymentStreamPaymentDateBusinessCenters[]? NoPaymentStreamPaymentDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoPaymentStreamPaymentDateBusinessCenters is not null && NoPaymentStreamPaymentDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(40947, NoPaymentStreamPaymentDateBusinessCenters.Length);
				for (int i = 0; i < NoPaymentStreamPaymentDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoPaymentStreamPaymentDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoPaymentStreamPaymentDateBusinessCenters") is IMessageView viewNoPaymentStreamPaymentDateBusinessCenters)
			{
				var count = viewNoPaymentStreamPaymentDateBusinessCenters.GroupCount();
				NoPaymentStreamPaymentDateBusinessCenters = new NoPaymentStreamPaymentDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoPaymentStreamPaymentDateBusinessCenters[i] = new();
					((IFixParser)NoPaymentStreamPaymentDateBusinessCenters[i]).Parse(viewNoPaymentStreamPaymentDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoPaymentStreamPaymentDateBusinessCenters":
					value = NoPaymentStreamPaymentDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoPaymentStreamPaymentDateBusinessCenters = null;
		}
	}
}
