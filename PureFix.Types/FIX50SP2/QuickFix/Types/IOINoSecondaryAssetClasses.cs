using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoSecondaryAssetClasses : IFixGroup
	{
		[TagDetails(Tag = 1977, Type = TagType.Int, Offset = 0, Required = false)]
		public int? SecondaryAssetClass {get; set;}
		
		[TagDetails(Tag = 1978, Type = TagType.Int, Offset = 1, Required = false)]
		public int? SecondaryAssetSubClass {get; set;}
		
		[TagDetails(Tag = 1979, Type = TagType.String, Offset = 2, Required = false)]
		public string? SecondaryAssetType {get; set;}
		
		[TagDetails(Tag = 2741, Type = TagType.String, Offset = 3, Required = false)]
		public string? SecondaryAssetSubType {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (SecondaryAssetClass is not null) writer.WriteWholeNumber(1977, SecondaryAssetClass.Value);
			if (SecondaryAssetSubClass is not null) writer.WriteWholeNumber(1978, SecondaryAssetSubClass.Value);
			if (SecondaryAssetType is not null) writer.WriteString(1979, SecondaryAssetType);
			if (SecondaryAssetSubType is not null) writer.WriteString(2741, SecondaryAssetSubType);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			SecondaryAssetClass = view.GetInt32(1977);
			SecondaryAssetSubClass = view.GetInt32(1978);
			SecondaryAssetType = view.GetString(1979);
			SecondaryAssetSubType = view.GetString(2741);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "SecondaryAssetClass":
					value = SecondaryAssetClass;
					break;
				case "SecondaryAssetSubClass":
					value = SecondaryAssetSubClass;
					break;
				case "SecondaryAssetType":
					value = SecondaryAssetType;
					break;
				case "SecondaryAssetSubType":
					value = SecondaryAssetSubType;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			SecondaryAssetClass = null;
			SecondaryAssetSubClass = null;
			SecondaryAssetType = null;
			SecondaryAssetSubType = null;
		}
	}
}
