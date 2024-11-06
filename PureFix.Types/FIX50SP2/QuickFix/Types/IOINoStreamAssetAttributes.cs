using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoStreamAssetAttributes : IFixGroup
	{
		[TagDetails(Tag = 41238, Type = TagType.String, Offset = 0, Required = false)]
		public string? StreamAssetAttributeType {get; set;}
		
		[TagDetails(Tag = 41239, Type = TagType.String, Offset = 1, Required = false)]
		public string? StreamAssetAttributeValue {get; set;}
		
		[TagDetails(Tag = 41240, Type = TagType.String, Offset = 2, Required = false)]
		public string? StreamAssetAttributeLimit {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StreamAssetAttributeType is not null) writer.WriteString(41238, StreamAssetAttributeType);
			if (StreamAssetAttributeValue is not null) writer.WriteString(41239, StreamAssetAttributeValue);
			if (StreamAssetAttributeLimit is not null) writer.WriteString(41240, StreamAssetAttributeLimit);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			StreamAssetAttributeType = view.GetString(41238);
			StreamAssetAttributeValue = view.GetString(41239);
			StreamAssetAttributeLimit = view.GetString(41240);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "StreamAssetAttributeType":
					value = StreamAssetAttributeType;
					break;
				case "StreamAssetAttributeValue":
					value = StreamAssetAttributeValue;
					break;
				case "StreamAssetAttributeLimit":
					value = StreamAssetAttributeLimit;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			StreamAssetAttributeType = null;
			StreamAssetAttributeValue = null;
			StreamAssetAttributeLimit = null;
		}
	}
}
