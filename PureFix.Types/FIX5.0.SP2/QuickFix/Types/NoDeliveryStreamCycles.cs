using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoDeliveryStreamCycles : IFixGroup
	{
		[TagDetails(Tag = 41082, Type = TagType.String, Offset = 0, Required = false)]
		public string? DeliveryStreamCycleDesc {get; set;}
		
		[TagDetails(Tag = 41083, Type = TagType.Length, Offset = 1, Required = false, LinksToTag = 41084)]
		public int? EncodedDeliveryStreamCycleDescLen {get; set;}
		
		[TagDetails(Tag = 41084, Type = TagType.RawData, Offset = 2, Required = false, LinksToTag = 41083)]
		public byte[]? EncodedDeliveryStreamCycleDesc {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (DeliveryStreamCycleDesc is not null) writer.WriteString(41082, DeliveryStreamCycleDesc);
			if (EncodedDeliveryStreamCycleDesc is not null)
			{
				writer.WriteWholeNumber(41083, EncodedDeliveryStreamCycleDesc.Length);
				writer.WriteBuffer(41084, EncodedDeliveryStreamCycleDesc);
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			DeliveryStreamCycleDesc = view.GetString(41082);
			EncodedDeliveryStreamCycleDescLen = view.GetInt32(41083);
			EncodedDeliveryStreamCycleDesc = view.GetByteArray(41084);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "DeliveryStreamCycleDesc":
					value = DeliveryStreamCycleDesc;
					break;
				case "EncodedDeliveryStreamCycleDescLen":
					value = EncodedDeliveryStreamCycleDescLen;
					break;
				case "EncodedDeliveryStreamCycleDesc":
					value = EncodedDeliveryStreamCycleDesc;
					break;
				default: return false;
			}
			return true;
		}
	}
}
