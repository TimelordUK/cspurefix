using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingPaymentStreamResetDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40970, Offset = 0, Required = false)]
		public NoUnderlyingPaymentStreamResetDateBusinessCenters[]? NoUnderlyingPaymentStreamResetDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingPaymentStreamResetDateBusinessCenters is not null && NoUnderlyingPaymentStreamResetDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(40970, NoUnderlyingPaymentStreamResetDateBusinessCenters.Length);
				for (int i = 0; i < NoUnderlyingPaymentStreamResetDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoUnderlyingPaymentStreamResetDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingPaymentStreamResetDateBusinessCenters") is IMessageView viewNoUnderlyingPaymentStreamResetDateBusinessCenters)
			{
				var count = viewNoUnderlyingPaymentStreamResetDateBusinessCenters.GroupCount();
				NoUnderlyingPaymentStreamResetDateBusinessCenters = new NoUnderlyingPaymentStreamResetDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingPaymentStreamResetDateBusinessCenters[i] = new();
					((IFixParser)NoUnderlyingPaymentStreamResetDateBusinessCenters[i]).Parse(viewNoUnderlyingPaymentStreamResetDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingPaymentStreamResetDateBusinessCenters":
					value = NoUnderlyingPaymentStreamResetDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingPaymentStreamResetDateBusinessCenters = null;
		}
	}
}
