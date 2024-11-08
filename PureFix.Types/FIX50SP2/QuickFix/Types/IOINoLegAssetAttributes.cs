using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoLegAssetAttributes : IFixGroup
	{
		[TagDetails(Tag = 2309, Type = TagType.String, Offset = 0, Required = false)]
		public string? LegAssetAttributeType {get; set;}
		
		[TagDetails(Tag = 2310, Type = TagType.String, Offset = 1, Required = false)]
		public string? LegAssetAttributeValue {get; set;}
		
		[TagDetails(Tag = 2311, Type = TagType.String, Offset = 2, Required = false)]
		public string? LegAssetAttributeLimit {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegAssetAttributeType is not null) writer.WriteString(2309, LegAssetAttributeType);
			if (LegAssetAttributeValue is not null) writer.WriteString(2310, LegAssetAttributeValue);
			if (LegAssetAttributeLimit is not null) writer.WriteString(2311, LegAssetAttributeLimit);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegAssetAttributeType = view.GetString(2309);
			LegAssetAttributeValue = view.GetString(2310);
			LegAssetAttributeLimit = view.GetString(2311);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegAssetAttributeType":
					value = LegAssetAttributeType;
					break;
				case "LegAssetAttributeValue":
					value = LegAssetAttributeValue;
					break;
				case "LegAssetAttributeLimit":
					value = LegAssetAttributeLimit;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			LegAssetAttributeType = null;
			LegAssetAttributeValue = null;
			LegAssetAttributeLimit = null;
		}
	}
}
