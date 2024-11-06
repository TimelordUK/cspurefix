using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class RateSourceComponent : IFixComponent
	{
		[Group(NoOfTag = 1445, Offset = 0, Required = false)]
		public ExecutionReportNoRateSources[]? NoRateSources {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoRateSources is not null && NoRateSources.Length != 0)
			{
				writer.WriteWholeNumber(1445, NoRateSources.Length);
				for (int i = 0; i < NoRateSources.Length; i++)
				{
					((IFixEncoder)NoRateSources[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoRateSources") is IMessageView viewNoRateSources)
			{
				var count = viewNoRateSources.GroupCount();
				NoRateSources = new ExecutionReportNoRateSources[count];
				for (int i = 0; i < count; i++)
				{
					NoRateSources[i] = new();
					((IFixParser)NoRateSources[i]).Parse(viewNoRateSources.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoRateSources":
					value = NoRateSources;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoRateSources = null;
		}
	}
}
