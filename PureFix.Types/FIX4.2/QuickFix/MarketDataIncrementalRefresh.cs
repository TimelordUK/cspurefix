using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;

namespace PureFix.Types.FIX42.QuickFix
{
	[MessageType("X", FixVersion.FIX42)]
	public sealed partial class MarketDataIncrementalRefresh : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 262, Type = TagType.String, Offset = 1, Required = false)]
		public string? MDReqID { get; set; }
		
		[Group(NoOfTag = 268, Offset = 2, Required = true)]
		public MarketDataIncrementalRefreshNoMDEntries[]? NoMDEntries { get; set; }
		
		[Component(Offset = 3, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& NoMDEntries is not null && FixValidator.IsValid(NoMDEntries, in config)
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (MDReqID is not null) writer.WriteString(262, MDReqID);
			if (NoMDEntries is not null && NoMDEntries.Length != 0)
			{
				writer.WriteWholeNumber(268, NoMDEntries.Length);
				for (int i = 0; i < NoMDEntries.Length; i++)
				{
					((IFixEncoder)NoMDEntries[i]).Encode(writer);
				}
			}
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
		}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
