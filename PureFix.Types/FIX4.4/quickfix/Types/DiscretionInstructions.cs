using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class DiscretionInstructions : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 388, Type = TagType.String, Offset = 0, Required = false)]
		public string? DiscretionInst { get; set; }
		
		[TagDetails(Tag = 389, Type = TagType.Float, Offset = 1, Required = false)]
		public double? DiscretionOffsetValue { get; set; }
		
		[TagDetails(Tag = 841, Type = TagType.Int, Offset = 2, Required = false)]
		public int? DiscretionMoveType { get; set; }
		
		[TagDetails(Tag = 842, Type = TagType.Int, Offset = 3, Required = false)]
		public int? DiscretionOffsetType { get; set; }
		
		[TagDetails(Tag = 843, Type = TagType.Int, Offset = 4, Required = false)]
		public int? DiscretionLimitType { get; set; }
		
		[TagDetails(Tag = 844, Type = TagType.Int, Offset = 5, Required = false)]
		public int? DiscretionRoundDirection { get; set; }
		
		[TagDetails(Tag = 846, Type = TagType.Int, Offset = 6, Required = false)]
		public int? DiscretionScope { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (DiscretionInst is not null) writer.WriteString(388, DiscretionInst);
			if (DiscretionOffsetValue is not null) writer.WriteNumber(389, DiscretionOffsetValue.Value);
			if (DiscretionMoveType is not null) writer.WriteWholeNumber(841, DiscretionMoveType.Value);
			if (DiscretionOffsetType is not null) writer.WriteWholeNumber(842, DiscretionOffsetType.Value);
			if (DiscretionLimitType is not null) writer.WriteWholeNumber(843, DiscretionLimitType.Value);
			if (DiscretionRoundDirection is not null) writer.WriteWholeNumber(844, DiscretionRoundDirection.Value);
			if (DiscretionScope is not null) writer.WriteWholeNumber(846, DiscretionScope.Value);
		}
	}
}
