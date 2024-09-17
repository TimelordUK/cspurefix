using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoUnderlyingStreamCommodityAltIDs : IFixGroup
	{
		[TagDetails(Tag = 41991, Type = TagType.String, Offset = 0, Required = false)]
		public string? UnderlyingStreamCommodityAltID {get; set;}
		
		[TagDetails(Tag = 41992, Type = TagType.String, Offset = 1, Required = false)]
		public string? UnderlyingStreamCommodityAltIDSource {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingStreamCommodityAltID is not null) writer.WriteString(41991, UnderlyingStreamCommodityAltID);
			if (UnderlyingStreamCommodityAltIDSource is not null) writer.WriteString(41992, UnderlyingStreamCommodityAltIDSource);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingStreamCommodityAltID = view.GetString(41991);
			UnderlyingStreamCommodityAltIDSource = view.GetString(41992);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingStreamCommodityAltID":
					value = UnderlyingStreamCommodityAltID;
					break;
				case "UnderlyingStreamCommodityAltIDSource":
					value = UnderlyingStreamCommodityAltIDSource;
					break;
				default: return false;
			}
			return true;
		}
	}
}
