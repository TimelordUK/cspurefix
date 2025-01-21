using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.FixRepo.Types;

namespace PureFix.Types.FIX50SP2.FixRepo.Types
{
	public sealed partial class InstrumentExtensionComponent : IFixComponent
	{
		[TagDetails(Tag = 668, Type = TagType.Int, Offset = 0, Required = false)]
		public int? DeliveryForm {get; set;}
		
		[TagDetails(Tag = 869, Type = TagType.Float, Offset = 1, Required = false)]
		public double? PctAtRisk {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (DeliveryForm is not null) writer.WriteWholeNumber(668, DeliveryForm.Value);
			if (PctAtRisk is not null) writer.WriteNumber(869, PctAtRisk.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			DeliveryForm = view.GetInt32(668);
			PctAtRisk = view.GetDouble(869);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "DeliveryForm":
					value = DeliveryForm;
					break;
				case "PctAtRisk":
					value = PctAtRisk;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			DeliveryForm = null;
			PctAtRisk = null;
		}
	}
}
