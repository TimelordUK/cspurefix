using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoRelatedSym : IFixGroup
	{
		[Component(Offset = 0, Required = false)]
		public InstrumentComponent? Instrument {get; set;}
		
		[Component(Offset = 1, Required = false)]
		public InstrumentExtensionComponent? InstrumentExtension {get; set;}
		
		[Component(Offset = 2, Required = false)]
		public FinancingDetailsComponent? FinancingDetails {get; set;}
		
		[Component(Offset = 3, Required = false)]
		public RelatedInstrumentGrpComponent? RelatedInstrumentGrp {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (InstrumentExtension is not null) ((IFixEncoder)InstrumentExtension).Encode(writer);
			if (FinancingDetails is not null) ((IFixEncoder)FinancingDetails).Encode(writer);
			if (RelatedInstrumentGrp is not null) ((IFixEncoder)RelatedInstrumentGrp).Encode(writer);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("Instrument") is IMessageView viewInstrument)
			{
				Instrument = new();
				((IFixParser)Instrument).Parse(viewInstrument);
			}
			if (view.GetView("InstrumentExtension") is IMessageView viewInstrumentExtension)
			{
				InstrumentExtension = new();
				((IFixParser)InstrumentExtension).Parse(viewInstrumentExtension);
			}
			if (view.GetView("FinancingDetails") is IMessageView viewFinancingDetails)
			{
				FinancingDetails = new();
				((IFixParser)FinancingDetails).Parse(viewFinancingDetails);
			}
			if (view.GetView("RelatedInstrumentGrp") is IMessageView viewRelatedInstrumentGrp)
			{
				RelatedInstrumentGrp = new();
				((IFixParser)RelatedInstrumentGrp).Parse(viewRelatedInstrumentGrp);
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "Instrument":
					value = Instrument;
					break;
				case "InstrumentExtension":
					value = InstrumentExtension;
					break;
				case "FinancingDetails":
					value = FinancingDetails;
					break;
				case "RelatedInstrumentGrp":
					value = RelatedInstrumentGrp;
					break;
				default: return false;
			}
			return true;
		}
	}
}
