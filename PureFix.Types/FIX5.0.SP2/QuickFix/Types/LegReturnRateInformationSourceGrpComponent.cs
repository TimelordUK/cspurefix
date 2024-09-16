using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegReturnRateInformationSourceGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42560, Offset = 0, Required = false)]
		public NoLegReturnRateInformationSources[]? NoLegReturnRateInformationSources {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegReturnRateInformationSources is not null && NoLegReturnRateInformationSources.Length != 0)
			{
				writer.WriteWholeNumber(42560, NoLegReturnRateInformationSources.Length);
				for (int i = 0; i < NoLegReturnRateInformationSources.Length; i++)
				{
					((IFixEncoder)NoLegReturnRateInformationSources[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegReturnRateInformationSources") is IMessageView viewNoLegReturnRateInformationSources)
			{
				var count = viewNoLegReturnRateInformationSources.GroupCount();
				NoLegReturnRateInformationSources = new NoLegReturnRateInformationSources[count];
				for (int i = 0; i < count; i++)
				{
					NoLegReturnRateInformationSources[i] = new();
					((IFixParser)NoLegReturnRateInformationSources[i]).Parse(viewNoLegReturnRateInformationSources.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegReturnRateInformationSources":
					value = NoLegReturnRateInformationSources;
					break;
				default: return false;
			}
			return true;
		}
	}
}
