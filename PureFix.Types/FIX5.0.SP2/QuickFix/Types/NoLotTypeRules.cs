using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoLotTypeRules : IFixGroup
	{
		[TagDetails(Tag = 1093, Type = TagType.String, Offset = 0, Required = false)]
		public string? LotType {get; set;}
		
		[TagDetails(Tag = 1231, Type = TagType.Float, Offset = 1, Required = false)]
		public double? MinLotSize {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LotType is not null) writer.WriteString(1093, LotType);
			if (MinLotSize is not null) writer.WriteNumber(1231, MinLotSize.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LotType = view.GetString(1093);
			MinLotSize = view.GetDouble(1231);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LotType":
					value = LotType;
					break;
				case "MinLotSize":
					value = MinLotSize;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			LotType = null;
			MinLotSize = null;
		}
	}
}
