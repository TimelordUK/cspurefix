using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoStreamCommodityAltIDs : IFixGroup
	{
		[TagDetails(Tag = 41278, Type = TagType.String, Offset = 0, Required = false)]
		public string? StreamCommodityAltID {get; set;}
		
		[TagDetails(Tag = 41279, Type = TagType.String, Offset = 1, Required = false)]
		public string? StreamCommodityAltIDSource {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StreamCommodityAltID is not null) writer.WriteString(41278, StreamCommodityAltID);
			if (StreamCommodityAltIDSource is not null) writer.WriteString(41279, StreamCommodityAltIDSource);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			StreamCommodityAltID = view.GetString(41278);
			StreamCommodityAltIDSource = view.GetString(41279);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "StreamCommodityAltID":
					value = StreamCommodityAltID;
					break;
				case "StreamCommodityAltIDSource":
					value = StreamCommodityAltIDSource;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			StreamCommodityAltID = null;
			StreamCommodityAltIDSource = null;
		}
	}
}
