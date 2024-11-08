using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingPaymentStubGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40708, Offset = 0, Required = false)]
		public IOINoUnderlyingPaymentStubs[]? NoUnderlyingPaymentStubs {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingPaymentStubs is not null && NoUnderlyingPaymentStubs.Length != 0)
			{
				writer.WriteWholeNumber(40708, NoUnderlyingPaymentStubs.Length);
				for (int i = 0; i < NoUnderlyingPaymentStubs.Length; i++)
				{
					((IFixEncoder)NoUnderlyingPaymentStubs[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingPaymentStubs") is IMessageView viewNoUnderlyingPaymentStubs)
			{
				var count = viewNoUnderlyingPaymentStubs.GroupCount();
				NoUnderlyingPaymentStubs = new IOINoUnderlyingPaymentStubs[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingPaymentStubs[i] = new();
					((IFixParser)NoUnderlyingPaymentStubs[i]).Parse(viewNoUnderlyingPaymentStubs.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingPaymentStubs":
					value = NoUnderlyingPaymentStubs;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingPaymentStubs = null;
		}
	}
}
