using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class ClearingPriceParametersGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 2580, Offset = 0, Required = false)]
		public NoClearingPriceParameters[]? NoClearingPriceParameters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoClearingPriceParameters is not null && NoClearingPriceParameters.Length != 0)
			{
				writer.WriteWholeNumber(2580, NoClearingPriceParameters.Length);
				for (int i = 0; i < NoClearingPriceParameters.Length; i++)
				{
					((IFixEncoder)NoClearingPriceParameters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoClearingPriceParameters") is IMessageView viewNoClearingPriceParameters)
			{
				var count = viewNoClearingPriceParameters.GroupCount();
				NoClearingPriceParameters = new NoClearingPriceParameters[count];
				for (int i = 0; i < count; i++)
				{
					NoClearingPriceParameters[i] = new();
					((IFixParser)NoClearingPriceParameters[i]).Parse(viewNoClearingPriceParameters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoClearingPriceParameters":
					value = NoClearingPriceParameters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoClearingPriceParameters = null;
		}
	}
}
