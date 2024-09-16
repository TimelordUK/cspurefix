using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class InstrmtMDReqGrpNoRelatedSym : IFixValidator, IFixEncoder
	{
		[Component(Offset = 0, Required = true)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 1, Required = false)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[Component(Offset = 2, Required = false)]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				Instrument is not null && ((IFixValidator)Instrument).IsValid(in config);
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (UndInstrmtGrp is not null) ((IFixEncoder)UndInstrmtGrp).Encode(writer);
			if (InstrmtLegGrp is not null) ((IFixEncoder)InstrmtLegGrp).Encode(writer);
		}
	}
}
