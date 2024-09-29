using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class ComplexEventAveragingObservationGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40994, Offset = 0, Required = false)]
		public NoComplexEventAveragingObservations[]? NoComplexEventAveragingObservations {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoComplexEventAveragingObservations is not null && NoComplexEventAveragingObservations.Length != 0)
			{
				writer.WriteWholeNumber(40994, NoComplexEventAveragingObservations.Length);
				for (int i = 0; i < NoComplexEventAveragingObservations.Length; i++)
				{
					((IFixEncoder)NoComplexEventAveragingObservations[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoComplexEventAveragingObservations") is IMessageView viewNoComplexEventAveragingObservations)
			{
				var count = viewNoComplexEventAveragingObservations.GroupCount();
				NoComplexEventAveragingObservations = new NoComplexEventAveragingObservations[count];
				for (int i = 0; i < count; i++)
				{
					NoComplexEventAveragingObservations[i] = new();
					((IFixParser)NoComplexEventAveragingObservations[i]).Parse(viewNoComplexEventAveragingObservations.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoComplexEventAveragingObservations":
					value = NoComplexEventAveragingObservations;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoComplexEventAveragingObservations = null;
		}
	}
}
