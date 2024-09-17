using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class ComplexEventRateSourceGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41013, Offset = 0, Required = false)]
		public NoComplexEventRateSources[]? NoComplexEventRateSources {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoComplexEventRateSources is not null && NoComplexEventRateSources.Length != 0)
			{
				writer.WriteWholeNumber(41013, NoComplexEventRateSources.Length);
				for (int i = 0; i < NoComplexEventRateSources.Length; i++)
				{
					((IFixEncoder)NoComplexEventRateSources[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoComplexEventRateSources") is IMessageView viewNoComplexEventRateSources)
			{
				var count = viewNoComplexEventRateSources.GroupCount();
				NoComplexEventRateSources = new NoComplexEventRateSources[count];
				for (int i = 0; i < count; i++)
				{
					NoComplexEventRateSources[i] = new();
					((IFixParser)NoComplexEventRateSources[i]).Parse(viewNoComplexEventRateSources.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoComplexEventRateSources":
					value = NoComplexEventRateSources;
					break;
				default: return false;
			}
			return true;
		}
	}
}
