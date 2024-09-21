using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegPaymentStubGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40418, Offset = 0, Required = false)]
		public NoLegPaymentStubs[]? NoLegPaymentStubs {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegPaymentStubs is not null && NoLegPaymentStubs.Length != 0)
			{
				writer.WriteWholeNumber(40418, NoLegPaymentStubs.Length);
				for (int i = 0; i < NoLegPaymentStubs.Length; i++)
				{
					((IFixEncoder)NoLegPaymentStubs[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegPaymentStubs") is IMessageView viewNoLegPaymentStubs)
			{
				var count = viewNoLegPaymentStubs.GroupCount();
				NoLegPaymentStubs = new NoLegPaymentStubs[count];
				for (int i = 0; i < count; i++)
				{
					NoLegPaymentStubs[i] = new();
					((IFixParser)NoLegPaymentStubs[i]).Parse(viewNoLegPaymentStubs.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegPaymentStubs":
					value = NoLegPaymentStubs;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegPaymentStubs = null;
		}
	}
}
