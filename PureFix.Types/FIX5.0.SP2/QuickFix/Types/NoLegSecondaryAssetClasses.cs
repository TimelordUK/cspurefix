using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoLegSecondaryAssetClasses : IFixGroup
	{
		[TagDetails(Tag = 2077, Type = TagType.Int, Offset = 0, Required = false)]
		public int? LegSecondaryAssetClass {get; set;}
		
		[TagDetails(Tag = 2078, Type = TagType.Int, Offset = 1, Required = false)]
		public int? LegSecondaryAssetSubClass {get; set;}
		
		[TagDetails(Tag = 2079, Type = TagType.String, Offset = 2, Required = false)]
		public string? LegSecondaryAssetType {get; set;}
		
		[TagDetails(Tag = 2743, Type = TagType.String, Offset = 3, Required = false)]
		public string? LegSecondaryAssetSubType {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegSecondaryAssetClass is not null) writer.WriteWholeNumber(2077, LegSecondaryAssetClass.Value);
			if (LegSecondaryAssetSubClass is not null) writer.WriteWholeNumber(2078, LegSecondaryAssetSubClass.Value);
			if (LegSecondaryAssetType is not null) writer.WriteString(2079, LegSecondaryAssetType);
			if (LegSecondaryAssetSubType is not null) writer.WriteString(2743, LegSecondaryAssetSubType);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegSecondaryAssetClass = view.GetInt32(2077);
			LegSecondaryAssetSubClass = view.GetInt32(2078);
			LegSecondaryAssetType = view.GetString(2079);
			LegSecondaryAssetSubType = view.GetString(2743);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegSecondaryAssetClass":
					value = LegSecondaryAssetClass;
					break;
				case "LegSecondaryAssetSubClass":
					value = LegSecondaryAssetSubClass;
					break;
				case "LegSecondaryAssetType":
					value = LegSecondaryAssetType;
					break;
				case "LegSecondaryAssetSubType":
					value = LegSecondaryAssetSubType;
					break;
				default: return false;
			}
			return true;
		}
	}
}
