using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("m", FixVersion.FIX44)]
	public sealed partial class ListStrikePrice : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 66, Type = TagType.String, Offset = 1, Required = true)]
		public string? ListID { get; set; }
		
		[TagDetails(Tag = 422, Type = TagType.Int, Offset = 2, Required = true)]
		public int? TotNoStrikes { get; set; }
		
		[TagDetails(Tag = 893, Type = TagType.Boolean, Offset = 3, Required = false)]
		public bool? LastFragment { get; set; }
		
		[Component(Offset = 4, Required = true)]
		public InstrmtStrkPxGrp? InstrmtStrkPxGrp { get; set; }
		
		[Component(Offset = 5, Required = false)]
		public UndInstrmtStrkPxGrp? UndInstrmtStrkPxGrp { get; set; }
		
		[Component(Offset = 6, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& ListID is not null
				&& TotNoStrikes is not null
				&& InstrmtStrkPxGrp is not null && ((IFixValidator)InstrmtStrkPxGrp).IsValid(in config)
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (ListID is not null) writer.WriteString(66, ListID);
			if (TotNoStrikes is not null) writer.WriteWholeNumber(422, TotNoStrikes.Value);
			if (LastFragment is not null) writer.WriteBoolean(893, LastFragment.Value);
			if (InstrmtStrkPxGrp is not null) ((IFixEncoder)InstrmtStrkPxGrp).Encode(writer);
			if (UndInstrmtStrkPxGrp is not null) ((IFixEncoder)UndInstrmtStrkPxGrp).Encode(writer);
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
		}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
