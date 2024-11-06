using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoUnderlyingStreamAssetAttributes : IFixGroup
	{
		[TagDetails(Tag = 41801, Type = TagType.String, Offset = 0, Required = false)]
		public string? UnderlyingStreamAssetAttributeType {get; set;}
		
		[TagDetails(Tag = 41802, Type = TagType.String, Offset = 1, Required = false)]
		public string? UnderlyingStreamAssetAttributeValue {get; set;}
		
		[TagDetails(Tag = 41803, Type = TagType.String, Offset = 2, Required = false)]
		public string? UnderlyingStreamAssetAttributeLimit {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingStreamAssetAttributeType is not null) writer.WriteString(41801, UnderlyingStreamAssetAttributeType);
			if (UnderlyingStreamAssetAttributeValue is not null) writer.WriteString(41802, UnderlyingStreamAssetAttributeValue);
			if (UnderlyingStreamAssetAttributeLimit is not null) writer.WriteString(41803, UnderlyingStreamAssetAttributeLimit);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingStreamAssetAttributeType = view.GetString(41801);
			UnderlyingStreamAssetAttributeValue = view.GetString(41802);
			UnderlyingStreamAssetAttributeLimit = view.GetString(41803);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingStreamAssetAttributeType":
					value = UnderlyingStreamAssetAttributeType;
					break;
				case "UnderlyingStreamAssetAttributeValue":
					value = UnderlyingStreamAssetAttributeValue;
					break;
				case "UnderlyingStreamAssetAttributeLimit":
					value = UnderlyingStreamAssetAttributeLimit;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			UnderlyingStreamAssetAttributeType = null;
			UnderlyingStreamAssetAttributeValue = null;
			UnderlyingStreamAssetAttributeLimit = null;
		}
	}
}
