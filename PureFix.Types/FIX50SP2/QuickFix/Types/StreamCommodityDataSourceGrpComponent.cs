using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class StreamCommodityDataSourceGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41280, Offset = 0, Required = false)]
		public NoStreamCommodityDataSources[]? NoStreamCommodityDataSources {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoStreamCommodityDataSources is not null && NoStreamCommodityDataSources.Length != 0)
			{
				writer.WriteWholeNumber(41280, NoStreamCommodityDataSources.Length);
				for (int i = 0; i < NoStreamCommodityDataSources.Length; i++)
				{
					((IFixEncoder)NoStreamCommodityDataSources[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoStreamCommodityDataSources") is IMessageView viewNoStreamCommodityDataSources)
			{
				var count = viewNoStreamCommodityDataSources.GroupCount();
				NoStreamCommodityDataSources = new NoStreamCommodityDataSources[count];
				for (int i = 0; i < count; i++)
				{
					NoStreamCommodityDataSources[i] = new();
					((IFixParser)NoStreamCommodityDataSources[i]).Parse(viewNoStreamCommodityDataSources.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoStreamCommodityDataSources":
					value = NoStreamCommodityDataSources;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoStreamCommodityDataSources = null;
		}
	}
}
