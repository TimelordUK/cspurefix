using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoAssetAttributes : IFixGroup
	{
		[TagDetails(Tag = 2305, Type = TagType.String, Offset = 0, Required = false)]
		public string? AssetAttributeType {get; set;}
		
		[TagDetails(Tag = 2306, Type = TagType.String, Offset = 1, Required = false)]
		public string? AssetAttributeValue {get; set;}
		
		[TagDetails(Tag = 2307, Type = TagType.String, Offset = 2, Required = false)]
		public string? AssetAttributeLimit {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (AssetAttributeType is not null) writer.WriteString(2305, AssetAttributeType);
			if (AssetAttributeValue is not null) writer.WriteString(2306, AssetAttributeValue);
			if (AssetAttributeLimit is not null) writer.WriteString(2307, AssetAttributeLimit);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			AssetAttributeType = view.GetString(2305);
			AssetAttributeValue = view.GetString(2306);
			AssetAttributeLimit = view.GetString(2307);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "AssetAttributeType":
					value = AssetAttributeType;
					break;
				case "AssetAttributeValue":
					value = AssetAttributeValue;
					break;
				case "AssetAttributeLimit":
					value = AssetAttributeLimit;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			AssetAttributeType = null;
			AssetAttributeValue = null;
			AssetAttributeLimit = null;
		}
	}
}
