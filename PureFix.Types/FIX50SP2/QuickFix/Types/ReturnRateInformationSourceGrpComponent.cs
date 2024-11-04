using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class ReturnRateInformationSourceGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42761, Offset = 0, Required = false)]
		public NoReturnRateInformationSources[]? NoReturnRateInformationSources {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoReturnRateInformationSources is not null && NoReturnRateInformationSources.Length != 0)
			{
				writer.WriteWholeNumber(42761, NoReturnRateInformationSources.Length);
				for (int i = 0; i < NoReturnRateInformationSources.Length; i++)
				{
					((IFixEncoder)NoReturnRateInformationSources[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoReturnRateInformationSources") is IMessageView viewNoReturnRateInformationSources)
			{
				var count = viewNoReturnRateInformationSources.GroupCount();
				NoReturnRateInformationSources = new NoReturnRateInformationSources[count];
				for (int i = 0; i < count; i++)
				{
					NoReturnRateInformationSources[i] = new();
					((IFixParser)NoReturnRateInformationSources[i]).Parse(viewNoReturnRateInformationSources.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoReturnRateInformationSources":
					value = NoReturnRateInformationSources;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoReturnRateInformationSources = null;
		}
	}
}
