using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class UndInstrmtCollGrpNoUnderlyings : IFixValidator, IFixEncoder
	{
		[Component(Offset = 0, Required = false)]
		public UnderlyingInstrument? UnderlyingInstrument { get; set; }
		
		[TagDetails(Tag = 944, Type = TagType.Int, Offset = 1, Required = false)]
		public int? CollAction { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingInstrument is not null) ((IFixEncoder)UnderlyingInstrument).Encode(writer);
			if (CollAction is not null) writer.WriteWholeNumber(944, CollAction.Value);
		}
	}
}
