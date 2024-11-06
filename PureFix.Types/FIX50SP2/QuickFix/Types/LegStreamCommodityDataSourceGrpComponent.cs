using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegStreamCommodityDataSourceGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41677, Offset = 0, Required = false)]
		public IOINoLegStreamCommodityDataSources[]? NoLegStreamCommodityDataSources {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegStreamCommodityDataSources is not null && NoLegStreamCommodityDataSources.Length != 0)
			{
				writer.WriteWholeNumber(41677, NoLegStreamCommodityDataSources.Length);
				for (int i = 0; i < NoLegStreamCommodityDataSources.Length; i++)
				{
					((IFixEncoder)NoLegStreamCommodityDataSources[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegStreamCommodityDataSources") is IMessageView viewNoLegStreamCommodityDataSources)
			{
				var count = viewNoLegStreamCommodityDataSources.GroupCount();
				NoLegStreamCommodityDataSources = new IOINoLegStreamCommodityDataSources[count];
				for (int i = 0; i < count; i++)
				{
					NoLegStreamCommodityDataSources[i] = new();
					((IFixParser)NoLegStreamCommodityDataSources[i]).Parse(viewNoLegStreamCommodityDataSources.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegStreamCommodityDataSources":
					value = NoLegStreamCommodityDataSources;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegStreamCommodityDataSources = null;
		}
	}
}
