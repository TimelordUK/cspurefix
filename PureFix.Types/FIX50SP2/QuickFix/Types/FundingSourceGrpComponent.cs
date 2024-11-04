using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class FundingSourceGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 2849, Offset = 0, Required = false)]
		public NoFundingSources[]? NoFundingSources {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoFundingSources is not null && NoFundingSources.Length != 0)
			{
				writer.WriteWholeNumber(2849, NoFundingSources.Length);
				for (int i = 0; i < NoFundingSources.Length; i++)
				{
					((IFixEncoder)NoFundingSources[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoFundingSources") is IMessageView viewNoFundingSources)
			{
				var count = viewNoFundingSources.GroupCount();
				NoFundingSources = new NoFundingSources[count];
				for (int i = 0; i < count; i++)
				{
					NoFundingSources[i] = new();
					((IFixParser)NoFundingSources[i]).Parse(viewNoFundingSources.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoFundingSources":
					value = NoFundingSources;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoFundingSources = null;
		}
	}
}
