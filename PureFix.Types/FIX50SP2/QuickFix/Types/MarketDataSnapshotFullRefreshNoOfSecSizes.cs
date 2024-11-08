using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class MarketDataSnapshotFullRefreshNoOfSecSizes : IFixGroup
	{
		[TagDetails(Tag = 1178, Type = TagType.Int, Offset = 0, Required = false)]
		public int? MDSecSizeType {get; set;}
		
		[TagDetails(Tag = 1179, Type = TagType.Float, Offset = 1, Required = false)]
		public double? MDSecSize {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (MDSecSizeType is not null) writer.WriteWholeNumber(1178, MDSecSizeType.Value);
			if (MDSecSize is not null) writer.WriteNumber(1179, MDSecSize.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			MDSecSizeType = view.GetInt32(1178);
			MDSecSize = view.GetDouble(1179);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "MDSecSizeType":
					value = MDSecSizeType;
					break;
				case "MDSecSize":
					value = MDSecSize;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			MDSecSizeType = null;
			MDSecSize = null;
		}
	}
}
