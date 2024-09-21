using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoUnderlyingStreamCommodityDataSources : IFixGroup
	{
		[TagDetails(Tag = 41994, Type = TagType.String, Offset = 0, Required = false)]
		public string? UnderlyingStreamCommodityDataSourceID {get; set;}
		
		[TagDetails(Tag = 41995, Type = TagType.Int, Offset = 1, Required = false)]
		public int? UnderlyingStreamCommodityDataSourceIDType {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingStreamCommodityDataSourceID is not null) writer.WriteString(41994, UnderlyingStreamCommodityDataSourceID);
			if (UnderlyingStreamCommodityDataSourceIDType is not null) writer.WriteWholeNumber(41995, UnderlyingStreamCommodityDataSourceIDType.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingStreamCommodityDataSourceID = view.GetString(41994);
			UnderlyingStreamCommodityDataSourceIDType = view.GetInt32(41995);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingStreamCommodityDataSourceID":
					value = UnderlyingStreamCommodityDataSourceID;
					break;
				case "UnderlyingStreamCommodityDataSourceIDType":
					value = UnderlyingStreamCommodityDataSourceIDType;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			UnderlyingStreamCommodityDataSourceID = null;
			UnderlyingStreamCommodityDataSourceIDType = null;
		}
	}
}
