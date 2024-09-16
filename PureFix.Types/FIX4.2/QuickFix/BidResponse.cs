using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;

namespace PureFix.Types.FIX42.QuickFix
{
	[MessageType("l", FixVersion.FIX42)]
	public sealed partial class BidResponse : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 390, Type = TagType.String, Offset = 1, Required = false)]
		public string? BidID { get; set; }
		
		[TagDetails(Tag = 391, Type = TagType.String, Offset = 2, Required = false)]
		public string? ClientBidID { get; set; }
		
		[Group(NoOfTag = 420, Offset = 3, Required = true)]
		public BidResponseNoBidComponents[]? NoBidComponents { get; set; }
		
		[Component(Offset = 4, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& NoBidComponents is not null && FixValidator.IsValid(NoBidComponents, in config)
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (BidID is not null) writer.WriteString(390, BidID);
			if (ClientBidID is not null) writer.WriteString(391, ClientBidID);
			if (NoBidComponents is not null && NoBidComponents.Length != 0)
			{
				writer.WriteWholeNumber(420, NoBidComponents.Length);
				for (int i = 0; i < NoBidComponents.Length; i++)
				{
					((IFixEncoder)NoBidComponents[i]).Encode(writer);
				}
			}
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
		}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
