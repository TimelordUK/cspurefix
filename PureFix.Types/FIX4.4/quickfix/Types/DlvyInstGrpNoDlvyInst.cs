using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class DlvyInstGrpNoDlvyInst : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 165, Type = TagType.String, Offset = 0, Required = false)]
		public string? SettlInstSource { get; set; }
		
		[TagDetails(Tag = 787, Type = TagType.String, Offset = 1, Required = false)]
		public string? DlvyInstType { get; set; }
		
		[Component(Offset = 2, Required = false)]
		public SettlParties? SettlParties { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (SettlInstSource is not null) writer.WriteString(165, SettlInstSource);
			if (DlvyInstType is not null) writer.WriteString(787, DlvyInstType);
			if (SettlParties is not null) ((IFixEncoder)SettlParties).Encode(writer);
		}
	}
}
