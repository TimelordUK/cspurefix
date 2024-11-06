using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoLegStreamAssetAttributes : IFixGroup
	{
		[TagDetails(Tag = 41453, Type = TagType.String, Offset = 0, Required = false)]
		public string? LegStreamAssetAttributeType {get; set;}
		
		[TagDetails(Tag = 41454, Type = TagType.String, Offset = 1, Required = false)]
		public string? LegStreamAssetAttributeValue {get; set;}
		
		[TagDetails(Tag = 41455, Type = TagType.String, Offset = 2, Required = false)]
		public string? LegStreamAssetAttributeLimit {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegStreamAssetAttributeType is not null) writer.WriteString(41453, LegStreamAssetAttributeType);
			if (LegStreamAssetAttributeValue is not null) writer.WriteString(41454, LegStreamAssetAttributeValue);
			if (LegStreamAssetAttributeLimit is not null) writer.WriteString(41455, LegStreamAssetAttributeLimit);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegStreamAssetAttributeType = view.GetString(41453);
			LegStreamAssetAttributeValue = view.GetString(41454);
			LegStreamAssetAttributeLimit = view.GetString(41455);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegStreamAssetAttributeType":
					value = LegStreamAssetAttributeType;
					break;
				case "LegStreamAssetAttributeValue":
					value = LegStreamAssetAttributeValue;
					break;
				case "LegStreamAssetAttributeLimit":
					value = LegStreamAssetAttributeLimit;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			LegStreamAssetAttributeType = null;
			LegStreamAssetAttributeValue = null;
			LegStreamAssetAttributeLimit = null;
		}
	}
}
