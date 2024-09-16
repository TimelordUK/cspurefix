using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingReturnRateInformationSourceGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 43060, Offset = 0, Required = false)]
		public NoUnderlyingReturnRateInformationSources[]? NoUnderlyingReturnRateInformationSources {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingReturnRateInformationSources is not null && NoUnderlyingReturnRateInformationSources.Length != 0)
			{
				writer.WriteWholeNumber(43060, NoUnderlyingReturnRateInformationSources.Length);
				for (int i = 0; i < NoUnderlyingReturnRateInformationSources.Length; i++)
				{
					((IFixEncoder)NoUnderlyingReturnRateInformationSources[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingReturnRateInformationSources") is IMessageView viewNoUnderlyingReturnRateInformationSources)
			{
				var count = viewNoUnderlyingReturnRateInformationSources.GroupCount();
				NoUnderlyingReturnRateInformationSources = new NoUnderlyingReturnRateInformationSources[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingReturnRateInformationSources[i] = new();
					((IFixParser)NoUnderlyingReturnRateInformationSources[i]).Parse(viewNoUnderlyingReturnRateInformationSources.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingReturnRateInformationSources":
					value = NoUnderlyingReturnRateInformationSources;
					break;
				default: return false;
			}
			return true;
		}
	}
}
