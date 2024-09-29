using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoUnderlyingDeliveryStreamCycles : IFixGroup
	{
		[TagDetails(Tag = 41805, Type = TagType.String, Offset = 0, Required = false)]
		public string? UnderlyingDeliveryStreamCycleDesc {get; set;}
		
		[TagDetails(Tag = 41806, Type = TagType.Length, Offset = 1, Required = false, LinksToTag = 41807)]
		public int? EncodedUnderlyingDeliveryStreamCycleDescLen {get; set;}
		
		[TagDetails(Tag = 41807, Type = TagType.RawData, Offset = 2, Required = false, LinksToTag = 41806)]
		public byte[]? EncodedUnderlyingDeliveryStreamCycleDesc {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingDeliveryStreamCycleDesc is not null) writer.WriteString(41805, UnderlyingDeliveryStreamCycleDesc);
			if (EncodedUnderlyingDeliveryStreamCycleDesc is not null)
			{
				writer.WriteWholeNumber(41806, EncodedUnderlyingDeliveryStreamCycleDesc.Length);
				writer.WriteBuffer(41807, EncodedUnderlyingDeliveryStreamCycleDesc);
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingDeliveryStreamCycleDesc = view.GetString(41805);
			EncodedUnderlyingDeliveryStreamCycleDescLen = view.GetInt32(41806);
			EncodedUnderlyingDeliveryStreamCycleDesc = view.GetByteArray(41807);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingDeliveryStreamCycleDesc":
					value = UnderlyingDeliveryStreamCycleDesc;
					break;
				case "EncodedUnderlyingDeliveryStreamCycleDescLen":
					value = EncodedUnderlyingDeliveryStreamCycleDescLen;
					break;
				case "EncodedUnderlyingDeliveryStreamCycleDesc":
					value = EncodedUnderlyingDeliveryStreamCycleDesc;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			UnderlyingDeliveryStreamCycleDesc = null;
			EncodedUnderlyingDeliveryStreamCycleDescLen = null;
			EncodedUnderlyingDeliveryStreamCycleDesc = null;
		}
	}
}
