using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class StrategyParametersGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 957, Offset = 0, Required = false)]
		public NoStrategyParameters[]? NoStrategyParameters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoStrategyParameters is not null && NoStrategyParameters.Length != 0)
			{
				writer.WriteWholeNumber(957, NoStrategyParameters.Length);
				for (int i = 0; i < NoStrategyParameters.Length; i++)
				{
					((IFixEncoder)NoStrategyParameters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoStrategyParameters") is IMessageView viewNoStrategyParameters)
			{
				var count = viewNoStrategyParameters.GroupCount();
				NoStrategyParameters = new NoStrategyParameters[count];
				for (int i = 0; i < count; i++)
				{
					NoStrategyParameters[i] = new();
					((IFixParser)NoStrategyParameters[i]).Parse(viewNoStrategyParameters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoStrategyParameters":
					value = NoStrategyParameters;
					break;
				default: return false;
			}
			return true;
		}
	}
}
