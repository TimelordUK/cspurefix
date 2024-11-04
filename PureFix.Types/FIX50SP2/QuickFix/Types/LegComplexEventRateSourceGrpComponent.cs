using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegComplexEventRateSourceGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41382, Offset = 0, Required = false)]
		public NoLegComplexEventRateSources[]? NoLegComplexEventRateSources {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegComplexEventRateSources is not null && NoLegComplexEventRateSources.Length != 0)
			{
				writer.WriteWholeNumber(41382, NoLegComplexEventRateSources.Length);
				for (int i = 0; i < NoLegComplexEventRateSources.Length; i++)
				{
					((IFixEncoder)NoLegComplexEventRateSources[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegComplexEventRateSources") is IMessageView viewNoLegComplexEventRateSources)
			{
				var count = viewNoLegComplexEventRateSources.GroupCount();
				NoLegComplexEventRateSources = new NoLegComplexEventRateSources[count];
				for (int i = 0; i < count; i++)
				{
					NoLegComplexEventRateSources[i] = new();
					((IFixParser)NoLegComplexEventRateSources[i]).Parse(viewNoLegComplexEventRateSources.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegComplexEventRateSources":
					value = NoLegComplexEventRateSources;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegComplexEventRateSources = null;
		}
	}
}
