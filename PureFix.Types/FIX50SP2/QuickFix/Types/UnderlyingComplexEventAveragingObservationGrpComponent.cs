using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingComplexEventAveragingObservationGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41713, Offset = 0, Required = false)]
		public IOINoUnderlyingComplexEventAveragingObservations[]? NoUnderlyingComplexEventAveragingObservations {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingComplexEventAveragingObservations is not null && NoUnderlyingComplexEventAveragingObservations.Length != 0)
			{
				writer.WriteWholeNumber(41713, NoUnderlyingComplexEventAveragingObservations.Length);
				for (int i = 0; i < NoUnderlyingComplexEventAveragingObservations.Length; i++)
				{
					((IFixEncoder)NoUnderlyingComplexEventAveragingObservations[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingComplexEventAveragingObservations") is IMessageView viewNoUnderlyingComplexEventAveragingObservations)
			{
				var count = viewNoUnderlyingComplexEventAveragingObservations.GroupCount();
				NoUnderlyingComplexEventAveragingObservations = new IOINoUnderlyingComplexEventAveragingObservations[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingComplexEventAveragingObservations[i] = new();
					((IFixParser)NoUnderlyingComplexEventAveragingObservations[i]).Parse(viewNoUnderlyingComplexEventAveragingObservations.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingComplexEventAveragingObservations":
					value = NoUnderlyingComplexEventAveragingObservations;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingComplexEventAveragingObservations = null;
		}
	}
}
