using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingPaymentStreamFormulaImageComponent : IFixComponent
	{
		[TagDetails(Tag = 42947, Type = TagType.Length, Offset = 0, Required = false, LinksToTag = 42948)]
		public int? UnderlyingPaymentStreamFormulaImageLength {get; set;}
		
		[TagDetails(Tag = 42948, Type = TagType.RawData, Offset = 1, Required = false, LinksToTag = 42947)]
		public byte[]? UnderlyingPaymentStreamFormulaImage {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingPaymentStreamFormulaImage is not null)
			{
				writer.WriteWholeNumber(42947, UnderlyingPaymentStreamFormulaImage.Length);
				writer.WriteBuffer(42948, UnderlyingPaymentStreamFormulaImage);
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingPaymentStreamFormulaImageLength = view.GetInt32(42947);
			UnderlyingPaymentStreamFormulaImage = view.GetByteArray(42948);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingPaymentStreamFormulaImageLength":
					value = UnderlyingPaymentStreamFormulaImageLength;
					break;
				case "UnderlyingPaymentStreamFormulaImage":
					value = UnderlyingPaymentStreamFormulaImage;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			UnderlyingPaymentStreamFormulaImageLength = null;
			UnderlyingPaymentStreamFormulaImage = null;
		}
	}
}
