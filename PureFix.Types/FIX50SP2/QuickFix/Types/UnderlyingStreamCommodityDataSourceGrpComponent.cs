using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingStreamCommodityDataSourceGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41993, Offset = 0, Required = false)]
		public NoUnderlyingStreamCommodityDataSources[]? NoUnderlyingStreamCommodityDataSources {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingStreamCommodityDataSources is not null && NoUnderlyingStreamCommodityDataSources.Length != 0)
			{
				writer.WriteWholeNumber(41993, NoUnderlyingStreamCommodityDataSources.Length);
				for (int i = 0; i < NoUnderlyingStreamCommodityDataSources.Length; i++)
				{
					((IFixEncoder)NoUnderlyingStreamCommodityDataSources[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingStreamCommodityDataSources") is IMessageView viewNoUnderlyingStreamCommodityDataSources)
			{
				var count = viewNoUnderlyingStreamCommodityDataSources.GroupCount();
				NoUnderlyingStreamCommodityDataSources = new NoUnderlyingStreamCommodityDataSources[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingStreamCommodityDataSources[i] = new();
					((IFixParser)NoUnderlyingStreamCommodityDataSources[i]).Parse(viewNoUnderlyingStreamCommodityDataSources.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingStreamCommodityDataSources":
					value = NoUnderlyingStreamCommodityDataSources;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingStreamCommodityDataSources = null;
		}
	}
}
