using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoLegDeliveryStreamCycles : IFixGroup
	{
		[TagDetails(Tag = 41457, Type = TagType.String, Offset = 0, Required = false)]
		public string? LegDeliveryStreamCycleDesc {get; set;}
		
		[TagDetails(Tag = 41458, Type = TagType.Length, Offset = 1, Required = false, LinksToTag = 41459)]
		public int? EncodedLegDeliveryStreamCycleDescLen {get; set;}
		
		[TagDetails(Tag = 41459, Type = TagType.RawData, Offset = 2, Required = false, LinksToTag = 41458)]
		public byte[]? EncodedLegDeliveryStreamCycleDesc {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegDeliveryStreamCycleDesc is not null) writer.WriteString(41457, LegDeliveryStreamCycleDesc);
			if (EncodedLegDeliveryStreamCycleDesc is not null)
			{
				writer.WriteWholeNumber(41458, EncodedLegDeliveryStreamCycleDesc.Length);
				writer.WriteBuffer(41459, EncodedLegDeliveryStreamCycleDesc);
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegDeliveryStreamCycleDesc = view.GetString(41457);
			EncodedLegDeliveryStreamCycleDescLen = view.GetInt32(41458);
			EncodedLegDeliveryStreamCycleDesc = view.GetByteArray(41459);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegDeliveryStreamCycleDesc":
					value = LegDeliveryStreamCycleDesc;
					break;
				case "EncodedLegDeliveryStreamCycleDescLen":
					value = EncodedLegDeliveryStreamCycleDescLen;
					break;
				case "EncodedLegDeliveryStreamCycleDesc":
					value = EncodedLegDeliveryStreamCycleDesc;
					break;
				default: return false;
			}
			return true;
		}
	}
}
