using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegComplexEventAveragingObservationGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41363, Offset = 0, Required = false)]
		public IOINoLegComplexEventAveragingObservations[]? NoLegComplexEventAveragingObservations {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegComplexEventAveragingObservations is not null && NoLegComplexEventAveragingObservations.Length != 0)
			{
				writer.WriteWholeNumber(41363, NoLegComplexEventAveragingObservations.Length);
				for (int i = 0; i < NoLegComplexEventAveragingObservations.Length; i++)
				{
					((IFixEncoder)NoLegComplexEventAveragingObservations[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegComplexEventAveragingObservations") is IMessageView viewNoLegComplexEventAveragingObservations)
			{
				var count = viewNoLegComplexEventAveragingObservations.GroupCount();
				NoLegComplexEventAveragingObservations = new IOINoLegComplexEventAveragingObservations[count];
				for (int i = 0; i < count; i++)
				{
					NoLegComplexEventAveragingObservations[i] = new();
					((IFixParser)NoLegComplexEventAveragingObservations[i]).Parse(viewNoLegComplexEventAveragingObservations.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegComplexEventAveragingObservations":
					value = NoLegComplexEventAveragingObservations;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegComplexEventAveragingObservations = null;
		}
	}
}
