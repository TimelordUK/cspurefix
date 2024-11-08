using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class TradeCaptureReportNoOfLegUnderlyings : IFixGroup
	{
		[Component(Offset = 0, Required = false)]
		public UnderlyingLegInstrumentComponent? UnderlyingLegInstrument {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingLegInstrument is not null) ((IFixEncoder)UnderlyingLegInstrument).Encode(writer);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("UnderlyingLegInstrument") is IMessageView viewUnderlyingLegInstrument)
			{
				UnderlyingLegInstrument = new();
				((IFixParser)UnderlyingLegInstrument).Parse(viewUnderlyingLegInstrument);
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingLegInstrument":
					value = UnderlyingLegInstrument;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			((IFixReset?)UnderlyingLegInstrument)?.Reset();
		}
	}
}
