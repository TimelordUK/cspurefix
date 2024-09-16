using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class PegInstructionsComponent : IFixComponent
	{
		[TagDetails(Tag = 211, Type = TagType.Float, Offset = 0, Required = false)]
		public double? PegOffsetValue {get; set;}
		
		[TagDetails(Tag = 835, Type = TagType.Int, Offset = 1, Required = false)]
		public int? PegMoveType {get; set;}
		
		[TagDetails(Tag = 836, Type = TagType.Int, Offset = 2, Required = false)]
		public int? PegOffsetType {get; set;}
		
		[TagDetails(Tag = 837, Type = TagType.Int, Offset = 3, Required = false)]
		public int? PegLimitType {get; set;}
		
		[TagDetails(Tag = 838, Type = TagType.Int, Offset = 4, Required = false)]
		public int? PegRoundDirection {get; set;}
		
		[TagDetails(Tag = 840, Type = TagType.Int, Offset = 5, Required = false)]
		public int? PegScope {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (PegOffsetValue is not null) writer.WriteNumber(211, PegOffsetValue.Value);
			if (PegMoveType is not null) writer.WriteWholeNumber(835, PegMoveType.Value);
			if (PegOffsetType is not null) writer.WriteWholeNumber(836, PegOffsetType.Value);
			if (PegLimitType is not null) writer.WriteWholeNumber(837, PegLimitType.Value);
			if (PegRoundDirection is not null) writer.WriteWholeNumber(838, PegRoundDirection.Value);
			if (PegScope is not null) writer.WriteWholeNumber(840, PegScope.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			PegOffsetValue = view.GetDouble(211);
			PegMoveType = view.GetInt32(835);
			PegOffsetType = view.GetInt32(836);
			PegLimitType = view.GetInt32(837);
			PegRoundDirection = view.GetInt32(838);
			PegScope = view.GetInt32(840);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "PegOffsetValue":
					value = PegOffsetValue;
					break;
				case "PegMoveType":
					value = PegMoveType;
					break;
				case "PegOffsetType":
					value = PegOffsetType;
					break;
				case "PegLimitType":
					value = PegLimitType;
					break;
				case "PegRoundDirection":
					value = PegRoundDirection;
					break;
				case "PegScope":
					value = PegScope;
					break;
				default: return false;
			}
			return true;
		}
	}
}
