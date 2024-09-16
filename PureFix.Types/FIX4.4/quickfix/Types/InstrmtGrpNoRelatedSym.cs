using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class InstrmtGrpNoRelatedSym : IFixValidator, IFixEncoder
	{
		[Component(Offset = 0, Required = false)]
		public Instrument? Instrument { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
		}
	}
}
