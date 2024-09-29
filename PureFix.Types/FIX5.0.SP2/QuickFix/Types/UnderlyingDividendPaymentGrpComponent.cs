using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingDividendPaymentGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42855, Offset = 0, Required = false)]
		public NoUnderlyingDividendPayments[]? NoUnderlyingDividendPayments {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingDividendPayments is not null && NoUnderlyingDividendPayments.Length != 0)
			{
				writer.WriteWholeNumber(42855, NoUnderlyingDividendPayments.Length);
				for (int i = 0; i < NoUnderlyingDividendPayments.Length; i++)
				{
					((IFixEncoder)NoUnderlyingDividendPayments[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingDividendPayments") is IMessageView viewNoUnderlyingDividendPayments)
			{
				var count = viewNoUnderlyingDividendPayments.GroupCount();
				NoUnderlyingDividendPayments = new NoUnderlyingDividendPayments[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingDividendPayments[i] = new();
					((IFixParser)NoUnderlyingDividendPayments[i]).Parse(viewNoUnderlyingDividendPayments.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingDividendPayments":
					value = NoUnderlyingDividendPayments;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingDividendPayments = null;
		}
	}
}
