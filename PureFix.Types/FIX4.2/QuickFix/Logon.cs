using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;

namespace PureFix.Types.FIX42.QuickFix
{
	[MessageType("A", FixVersion.FIX42)]
	public sealed partial class Logon : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 98, Type = TagType.Int, Offset = 1, Required = true)]
		public int? EncryptMethod { get; set; }
		
		[TagDetails(Tag = 108, Type = TagType.Int, Offset = 2, Required = true)]
		public int? HeartBtInt { get; set; }
		
		[TagDetails(Tag = 95, Type = TagType.Length, Offset = 3, Required = false, LinksToTag = 96)]
		public int? RawDataLength { get; set; }
		
		[TagDetails(Tag = 96, Type = TagType.RawData, Offset = 4, Required = false, LinksToTag = 95)]
		public byte[]? RawData { get; set; }
		
		[TagDetails(Tag = 141, Type = TagType.Boolean, Offset = 5, Required = false)]
		public bool? ResetSeqNumFlag { get; set; }
		
		[TagDetails(Tag = 383, Type = TagType.Int, Offset = 6, Required = false)]
		public int? MaxMessageSize { get; set; }
		
		[Group(NoOfTag = 384, Offset = 7, Required = false)]
		public LogonNoMsgTypes[]? NoMsgTypes { get; set; }
		
		[Component(Offset = 8, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& EncryptMethod is not null
				&& HeartBtInt is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (EncryptMethod is not null) writer.WriteWholeNumber(98, EncryptMethod.Value);
			if (HeartBtInt is not null) writer.WriteWholeNumber(108, HeartBtInt.Value);
			if (RawData is not null)
			{
				writer.WriteWholeNumber(95, RawData.Length);
				writer.WriteBuffer(96, RawData);
			}
			if (ResetSeqNumFlag is not null) writer.WriteBoolean(141, ResetSeqNumFlag.Value);
			if (MaxMessageSize is not null) writer.WriteWholeNumber(383, MaxMessageSize.Value);
			if (NoMsgTypes is not null && NoMsgTypes.Length != 0)
			{
				writer.WriteWholeNumber(384, NoMsgTypes.Length);
				for (int i = 0; i < NoMsgTypes.Length; i++)
				{
					((IFixEncoder)NoMsgTypes[i]).Encode(writer);
				}
			}
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
		}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
