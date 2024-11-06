using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class MarketDataFeedTypesComponent : IFixComponent
	{
		[Group(NoOfTag = 1141, Offset = 0, Required = false)]
		public SecurityDefinitionNoMDFeedTypes[]? NoMDFeedTypes {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoMDFeedTypes is not null && NoMDFeedTypes.Length != 0)
			{
				writer.WriteWholeNumber(1141, NoMDFeedTypes.Length);
				for (int i = 0; i < NoMDFeedTypes.Length; i++)
				{
					((IFixEncoder)NoMDFeedTypes[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoMDFeedTypes") is IMessageView viewNoMDFeedTypes)
			{
				var count = viewNoMDFeedTypes.GroupCount();
				NoMDFeedTypes = new SecurityDefinitionNoMDFeedTypes[count];
				for (int i = 0; i < count; i++)
				{
					NoMDFeedTypes[i] = new();
					((IFixParser)NoMDFeedTypes[i]).Parse(viewNoMDFeedTypes.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoMDFeedTypes":
					value = NoMDFeedTypes;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoMDFeedTypes = null;
		}
	}
}
