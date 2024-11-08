using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoStreamCommodityDataSources : IFixGroup
	{
		[TagDetails(Tag = 41281, Type = TagType.String, Offset = 0, Required = false)]
		public string? StreamCommodityDataSourceID {get; set;}
		
		[TagDetails(Tag = 41282, Type = TagType.Int, Offset = 1, Required = false)]
		public int? StreamCommodityDataSourceIDType {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StreamCommodityDataSourceID is not null) writer.WriteString(41281, StreamCommodityDataSourceID);
			if (StreamCommodityDataSourceIDType is not null) writer.WriteWholeNumber(41282, StreamCommodityDataSourceIDType.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			StreamCommodityDataSourceID = view.GetString(41281);
			StreamCommodityDataSourceIDType = view.GetInt32(41282);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "StreamCommodityDataSourceID":
					value = StreamCommodityDataSourceID;
					break;
				case "StreamCommodityDataSourceIDType":
					value = StreamCommodityDataSourceIDType;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			StreamCommodityDataSourceID = null;
			StreamCommodityDataSourceIDType = null;
		}
	}
}
